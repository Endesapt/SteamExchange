using Microsoft.EntityFrameworkCore;
using Mysqlx.Crud;
using Server.Data;
using Server.Models;
using Server.ResponseModels;
using System.Diagnostics;
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
                    (k.Value.ClassId is not null)
                    ).DistinctBy(k=>k.Value.ClassId).ToDictionary();
                    foreach( var weapon in allWeapons)
                    {
                        Price? price = null;
                        if(weapon.Value.Prices == null) { }
                        else if (weapon.Value.Prices.TryGetValue("24_hours", out price)) { }
                        else if (weapon.Value.Prices.TryGetValue("7_days", out price)) { }
                        else if (weapon.Value.Prices.TryGetValue("30_days", out price)) { }
                        else if (weapon.Value.Prices.TryGetValue("all_time", out price)) { }
                        weapon.Value.Price=price?.Average ?? 0;
                    }
                    using var scope = _provider.CreateScope();
                    using var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
                    await db.Weapons.ForEachAsync(w =>
                    {
                        if (allWeapons.ContainsKey(w.Name))
                        {
                            w.Price = allWeapons[w.Name].Price;
                            allWeapons.Remove(w.Name);
                        }
                    }
                    );
                    db.Weapons.AddRange(allWeapons.Values.Select((wDto) => new ModelLibrary.Weapon
                    {
                        Price = wDto.Price,
                        Name = wDto.Name,
                        Type = wDto.Type,
                        IsTradable = wDto.IsTradable==1,
                        IconUrl = wDto.IconUrl,
                        ClassId=wDto.ClassId??0
                    }));
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
