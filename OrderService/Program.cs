using BuildingBlocks.Behaviors;
using BuildingBlocks.Extensions;
using BuildingBlocks.Extensions.ServiceCollection;
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

builder.Services.AddMediatR(cfg =>
{
    cfg.RegisterServicesFromAssembly(typeof(Program).Assembly);
    cfg.AddOpenBehavior(typeof(ValidationBehavior<,>));
});

builder.Services.AddAuthenticationService(builder.Configuration);

var app = builder.Build();

app.UseAuthentication();
app.UseAuthorization();
app.Run();
