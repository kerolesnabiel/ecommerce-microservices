using BuildingBlocks.Behaviors;
using BuildingBlocks.Middlewares;
using FluentValidation;
using PaymentService.gRPC.Services;
using Stripe;

var builder = WebApplication.CreateBuilder(args);
var assembly = typeof(Program).Assembly;

builder.Services.AddMediatR(cfg =>
{
    cfg.RegisterServicesFromAssembly(assembly);
    cfg.AddOpenBehavior(typeof(ValidationBehavior<,>));
});

builder.Services.AddValidatorsFromAssembly(assembly);

builder.Services.AddGrpc();

var config = builder.Configuration;
StripeConfiguration.ApiKey = config["Stripe:SecretKey"];

var app = builder.Build();

app.MapGet("api/payments/key", () => config["Stripe:PublishableKey"]);

app.MapGrpcService<PaymentServiceGrpc>();

app.Run();
