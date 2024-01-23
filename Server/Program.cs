using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Server.Data;
using Server.Events;
using Server.Models;
using Server.Services;
using Server.Services.Interfaces;
using System;
using System.Security.Claims;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddHttpClient();
// Add services to the container.

builder.Services.AddControllers();
// Add database

builder.Services.AddDbContext<ApplicationDbContext>(options =>
options.UseMySQL("server = localhost; user = root; database = test_db; password =admin;")
);
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAuthentication(options =>
{
    options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
})
.AddCookie(options =>
     {
         options.LoginPath = "/login";
         options.LogoutPath = "/signout";
     })
.AddSteam(options =>
{
    options.Events.OnAuthenticated = AuthenticationEvents.OnAuthenticated;
});
builder.Services.AddAuthorization();
//My services
builder.Services.AddTransient<IChatService,ChatService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
