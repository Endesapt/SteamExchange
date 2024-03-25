using Client.Services.Interfaces;
using IdentityModel.Client;
using Microsoft.Extensions.Configuration;
using ModelLibrary;
using MonkeyCache.FileStore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace Client.Services
{
    internal class RequestService:IRequestService
    {
        private readonly string BaseURL;
        private readonly IAuthorizationHandler _authorizationHandler;
        public RequestService(IConfiguration configuration, IAuthorizationHandler authorizationHandler)
        {
            //BaseURL = "http://192.168.0.105:45455/";
            BaseURL = "https://settling-maggot-finally.ngrok-free.app";
            _authorizationHandler = authorizationHandler;
        }

        public async Task<bool> CreateOfferAsync(IEnumerable<UserWeapon> weapons)
        {
            return await PostAsync("/createAuction", weapons);
        }

        public async Task<T> GetAsync<T>(string url, int minutes, bool forceRefresh)
        {
            if (!forceRefresh && !Barrel.Current.IsExpired(url))
                return Barrel.Current.Get<T>(url);
            var httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri(BaseURL);
            httpClient.SetBearerToken(
                await _authorizationHandler.GetAccessTokenAsync()
                );
            try
            {
                T result = await httpClient.GetFromJsonAsync<T>(url);
                Barrel.Current.Add(url, result, TimeSpan.FromMinutes(minutes));
                return result;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Unable to get information from server {ex}");
            }

            return default;
        }

        public async Task<bool> PostAsync(string url,object obj)
        {
            var httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri(BaseURL);
            httpClient.SetBearerToken(
                await _authorizationHandler.GetAccessTokenAsync()
                );
            var responce = await httpClient.PostAsJsonAsync(url,obj);
            return responce.IsSuccessStatusCode;
        }
    }
}
