using Newtonsoft.Json;
using Server.Data;
using Server.Dto;
using Server.Helpers;
using Server.Models;
using Server.ResponseModels;
using Server.Services.Interfaces;
using System.Data.Entity;
using System.Net.Http;
using System.Web;

namespace Server.Services
{
    public class UserService : IUserService
    {
        private readonly ApplicationDbContext _context;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;
        public UserService(ApplicationDbContext context,IHttpClientFactory httpClientFactory,IConfiguration configuration)
        {
            _configuration= configuration;
            _context=context;
            _httpClientFactory=httpClientFactory;
        }
        public async Task EnsureUserCreated(long userId)
        {
            var user = _context.Users.Find(userId);
            if (user != null) return;
            await CreateUser(userId);
            await UpdateInventory(userId);


        }

        public ProfileResponseModel? GetProfile(long userId)
        {
            var user = _context.Users.Find(userId);
            if (user == null) return null;
            var weapons = _context.UsersWeapons
                .Include((uw) => uw.Weapon)
                .Where((uw) => uw.UserId == userId)
                .Select((uw)=>new WeaponDto(){ 
                    ClassId = uw.WeaponClassId,
                    AssetId=uw.AssetId,
                    Price=uw.Weapon.Price,
                    Name=uw.Weapon.Name,
                    IconUrl=uw.Weapon.IconUrl,
                    Count=uw.Count
                });
            return new ProfileResponseModel
            {
                User = user,
                Weapons = weapons.ToList()
            };
        }

        public async Task<bool> UpdateInventory(long userId)
        {
            var user = _context.Users.Find(userId);
            if (user == null) return false;
            var httpClient = _httpClientFactory!.CreateClient();
            var urlBuilder = new UriBuilder("https://api.steampowered.com/IEconService/GetInventoryItemsWithDescriptions/v1/");
            var query = HttpUtility.ParseQueryString(urlBuilder.Query);
            query["contextid"] = "2";
            query["key"] = _configuration["SteamApiKey"];
            query["steamid"] = userId.ToString();
            query["get_descriptions"] = "true";
            query["appid"] = "730";
            urlBuilder.Query = query.ToString();
            string url = urlBuilder.ToString();
            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, url);

            var httpResponseMessage = await httpClient.SendAsync(httpRequestMessage);
            if (httpResponseMessage.IsSuccessStatusCode)
            {
                var contentString =
                    await httpResponseMessage.Content.ReadAsStringAsync();

                var response = JsonConvert.DeserializeObject<dynamic>(contentString);
                if(response?.response?.assets == null) return false;
                var assets = response.response.assets;
                IEnumerable<dynamic> descriptions = response.response.descriptions;
                foreach ( var item in assets)
                {
                    UserWeapon userWeapon = new()
                    {
                        UserId = userId,
                        WeaponClassId = item.classid,
                        Count = item.amount,
                        AssetId = item.assetid,
                    };
                    var isTradeable = descriptions
                        .FirstOrDefault((d)=>d.classid==item.classid)?
                        .tradable??false;
                    if (!(bool)isTradeable) continue;
                    _context.Update(userWeapon);
                }
                await _context.SaveChangesAsync();
                return true;
            }
            return false;

        }
        private async Task CreateUser(long userId)
        {
            



            var httpClient = _httpClientFactory!.CreateClient();
            var urlBuilder = new UriBuilder("https://api.steampowered.com/ISteamUser/GetPlayerSummaries/v2/");
            var query = HttpUtility.ParseQueryString(urlBuilder.Query);
            query["format"] = "json";
            query["key"] = _configuration["SteamApiKey"];
            query["steamids"] = userId.ToString();
            urlBuilder.Query = query.ToString();
            string url = urlBuilder.ToString();
            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, url);

            var httpResponseMessage = await httpClient.SendAsync(httpRequestMessage);

            if (httpResponseMessage.IsSuccessStatusCode)
            {
                var contentString =
                    await httpResponseMessage.Content.ReadAsStringAsync();

                var response = JsonConvert.DeserializeObject<dynamic>(contentString);

                var responseUser = response.response.players[0];
                var user = new User()
                {
                    AvatarHash = responseUser.avatarhash,
                    UserName = responseUser.personaname,
                    Id = responseUser.steamid,
                };
                _context.Add(user);
                _context.SaveChanges();
            }
            else
            {
                throw new InvalidOperationException("Can't get info from Steam servers");
            }
        }

    }
}

