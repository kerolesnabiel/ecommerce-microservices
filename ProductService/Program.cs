using ProductService.Behaviors;

var builder = WebApplication.CreateBuilder(args);
var assembly = typeof(Program).Assembly;

builder.Services.AddMediatR(cfg =>
{
    cfg.RegisterServicesFromAssembly(assembly);
    cfg.AddOpenBehavior(typeof(ValidationBehavior<,>));
});

builder.Services.AddValidatorsFromAssembly(assembly);

builder.Services.AddMarten(cfg => cfg.Connection
    (builder.Configuration.GetConnectionString("DefaultConnection")!))
    .UseLightweightSessions();

builder.Services.AddCarter();

var app = builder.Build();

app.MapCarter();
app.Run();
