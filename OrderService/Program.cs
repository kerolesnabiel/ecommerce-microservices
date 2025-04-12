using BuildingBlocks.Extensions;
using OrderService.Models;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddMarten(options =>
{
    options.Connection(builder.Configuration.GetConnectionString("Database")!);
    options.Schema.For<Order>()
        .Identity(x => x.Id)
        .Index(x => x.UserId)
        .Index(x => x.TransactionId);
})
.UseLightweightSessions();

builder.Services.AddMassTransitService
    (builder.Configuration, Assembly.GetExecutingAssembly());

var app = builder.Build();
app.Run();
