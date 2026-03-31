using Ocelot.DependencyInjection;
using Ocelot.Middleware;
using Ocelot.Cache.CacheManager;
using FiguraSp.SharedLibrary.DependencyInjection;
using FiguraSp.Gateway.Api.Middleware;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.AddJsonFile("ocelot.json", optional: false, reloadOnChange: true);
SharedService.AddJwtSharedService(builder.Services, builder.Configuration);

builder.Services.AddOcelot().AddCacheManager(x => x.WithDictionaryHandle());
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(builder =>
    {
        builder.AllowAnyHeader().AllowAnyMethod().WithOrigins("http://localhost:3000");
        //builder.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin();
    });
});

var app = builder.Build();

app.UseCors();
app.UseHttpsRedirection();
app.UseMiddleware<RequestMiddleware>();
app.UseOcelot().Wait();
app.Run();