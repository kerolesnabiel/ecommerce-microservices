var builder = WebApplication.CreateBuilder(args);

var assembly = typeof(Program).Assembly;

builder.Services.AddMediatR(cfg =>
    cfg.RegisterServicesFromAssembly(assembly));

builder.Services.AddMarten(cfg => cfg.Connection
    (builder.Configuration.GetConnectionString("DefaultConnection")!))
    .UseLightweightSessions();

builder.Services.AddCarter();

var app = builder.Build();

app.MapCarter();
app.Run();
