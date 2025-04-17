using Microsoft.EntityFrameworkCore;
using NotificationService.Data;
using NotificationService.Data.Seeders;
using NotificationService.Hubs;
using BuildingBlocks.Extensions.ServiceCollection;

var builder = WebApplication.CreateBuilder(args);
var config = builder.Configuration;

builder.Services.AddDbContext<NotificationDbContext>(options =>
    options.UseNpgsql(config.GetConnectionString("Database")));

builder.Services.AddScoped<Seeder>();
builder.Services.AddSignalR();
builder.Services.AddAuthenticationService(config);

var app = builder.Build();

var scope = app.Services.CreateScope();
var seeder = scope.ServiceProvider.GetRequiredService<Seeder>();
await seeder.Seed();

app.MapHub<NotificationHub>("/api/notification/hub");

app.Run();
