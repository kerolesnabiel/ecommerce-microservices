using ProductService.Behaviors;
using ProductService.Middlewares;
using ProductService.Extensions;

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
builder.Services.AddScoped<ErrorHandlingMiddleware>();

builder.Services.AddAuthorization();
builder.Services.AddAuthenticationService(builder.Configuration);

var app = builder.Build();

app.UseAuthentication();
app.UseAuthorization();

app.UseMiddleware<ErrorHandlingMiddleware>();
app.MapCarter();
app.Run();
