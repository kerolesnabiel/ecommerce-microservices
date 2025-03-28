using CartService.Extensions;
using CartService.Middlewares;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddServiceExtensions(builder.Configuration);

var app = builder.Build();
app.UseMiddleware<ErrorHandlingMiddleware>();
app.MapCarter();
app.Run();
