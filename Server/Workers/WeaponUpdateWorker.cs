using Microsoft.EntityFrameworkCore;
using Mysqlx.Crud;
using Server.Data;
using Server.Models;
using Server.ResponseModels;
using System.Text.Json;

namespace Server.Workers
{
    public class WeaponUpdateWorker:BackgroundService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IServiceProvider _provider;
        public WeaponUpdateWorker(IHttpClientFactory factory,
            IServiceProvider provider)
        {
            _httpClientFactory = factory;
            _provider = provider;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            
            
           
            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    
                    using HttpClient client = _httpClientFactory.CreateClient();
                    var weaponseResponce = await client.GetFromJsonAsync<WeaponsInfo>(
                                           $"https://csgobackpack.net/api/GetItemsList/v2/",
                                           new JsonSerializerOptions(JsonSerializerDefaults.Web));
                    var allWeapons = weaponseResponce.ItemsList.Where(k =>
                    ((k.Value.ClassId is not null) && (k.Value.IsTradable > 0)&&(k.Value.Prices is not null)
                    )).DistinctBy(k=>k.Value.ClassId).ToDictionary();
                    foreach( var weapon in allWeapons)
                    {
                        Price? price = null;
                        if (weapon.Value.Prices.TryGetValue("24_hours", out price)) { }
                        else if (weapon.Value.Prices.TryGetValue("7_days", out price)) { }
                        else if (weapon.Value.Prices.TryGetValue("30_days", out price)) { }
                        else if (weapon.Value.Prices.TryGetValue("all_time", out price)) { }
                        weapon.Value.Price=price?.Average ?? 0;
                    }
                    using var scope = _provider.CreateScope();
                    using var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
                    await db.Weapons.ForEachAsync(w =>
                    {
                        w.Price = allWeapons[w.Name].Price;
                        allWeapons.Remove(w.Name);
                    }
                           );
                    db.Weapons.AddRange(allWeapons.Values);
                    db.SaveChanges();
                    db.ChangeTracker.Clear();

                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error while refreshing weapons db: "+ex.ToString());
                }
                //8 hours
                await Task.Delay(1000*3600*8, stoppingToken);
            }
        }
    }
}
