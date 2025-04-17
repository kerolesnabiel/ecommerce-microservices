using Microsoft.EntityFrameworkCore;
using NotificationService.Data;
using NotificationService.Data.Seeders;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<NotificationDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("Database")));
builder.Services.AddScoped<Seeder>();

var app = builder.Build();
var scope = app.Services.CreateScope();
var seeder = scope.ServiceProvider.GetRequiredService<Seeder>();
await seeder.Seed();
app.Run();
