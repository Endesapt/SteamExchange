using AspNet.Security.OpenId;
using System.Security.Claims;
using Server.Helpers;
using Server.Data;
using Server.Models;
using System.Text.Json;
using Newtonsoft.Json;
using System.Web;

namespace Server.Events
{
    public static class AuthenticationEvents
    {
        public static async Task OnAuthenticated(OpenIdAuthenticatedContext c)
        {
            var userIdString = c.Identity.FindFirst(ClaimTypes.NameIdentifier).Value;
            long userId;
            if (!ExtractIdHelper.ExtractedIdFromClaim(userIdString, out userId)) throw new InvalidOperationException("can't extract Id from Claim");

            
            var httpClientFactory = c.HttpContext.RequestServices.GetService<IHttpClientFactory>();
            var configuration = c.HttpContext.RequestServices.GetService<IConfiguration>();
            using var dbContext = c.HttpContext.RequestServices.GetService<ApplicationDbContext>();
            User user = dbContext!.Users.Find(userId);
            if (user!=null) return;

            var httpClient= httpClientFactory!.CreateClient();
            var urlBuilder = new UriBuilder("https://api.steampowered.com/ISteamUser/GetPlayerSummaries/v2/");
            var query = HttpUtility.ParseQueryString(urlBuilder.Query);
            query["format"] = "json";
            query["key"] = configuration["SteamApiKey"];
            query["steamids"] = userId.ToString();
            urlBuilder.Query = query.ToString();
            string url = urlBuilder.ToString();
            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get,url);

            var httpResponseMessage = await httpClient.SendAsync(httpRequestMessage);

            if (httpResponseMessage.IsSuccessStatusCode)
            {
                var contentString =
                    await httpResponseMessage.Content.ReadAsStringAsync();

                var response = JsonConvert.DeserializeObject<dynamic>(contentString);

                var responseUser = response.response.players[0];
                user = new User()
                {
                    AvatarHash = responseUser.avatarhash,
                    UserName = responseUser.personaname,
                    Id = responseUser.steamid,
                };
                dbContext.Add(user);
                dbContext.SaveChanges();
                dbContext.Dispose();
            }
            else
            {
                throw new InvalidOperationException("Can't get info from Steam servers");
            }
        }
       
    }
}
