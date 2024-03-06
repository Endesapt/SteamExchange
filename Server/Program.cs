using Amazon.Runtime;
using Amazon.S3;
using Keycloak.AuthServices.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Server.Data;
using Server.Events;
using Server.Hubs;
using Server.Models;
using Server.ResponseModels;
using Server.Services;
using Server.Services.Interfaces;
using Server.Workers;
using System;
using System.Security.Claims;
using System.Text.Json;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddHttpClient();
builder.Services.AddSingleton<IUserIdProvider, CustomUserIdProvider>();
builder.Services.AddSignalR();
builder.Services.AddControllers();
// Add database

builder.Services.AddDbContextPool<ApplicationDbContext>(options =>
{
    options.UseMySQL("server = localhost; user = root; database = test_db; password =admin;");
}
);
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDefaultAWSOptions(builder.Configuration.GetAWSOptions());

builder.Services.AddAuthorization();
builder.Services.AddKeycloakAuthentication(builder.Configuration, o =>
{
    o.RequireHttpsMetadata = false;
});
builder.Services.AddAuthorization();
//Services
builder.Services.AddTransient<IChatService,ChatService>();
builder.Services.AddAWSService<IAmazonS3>();
//Workers
builder.Services.AddHostedService<WeaponUpdateWorker>();

var app = builder.Build();

app.UseDefaultFiles();
app.UseStaticFiles();
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
app.MapHub<ChatHub>("/chat");

app.Run();
