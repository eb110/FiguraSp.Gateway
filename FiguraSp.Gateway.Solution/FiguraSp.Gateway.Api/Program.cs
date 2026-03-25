using eCommerce.SharedLibrary.DependencyInjection;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;
using Ocelot.Cache.CacheManager;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.AddJsonFile("ocelot.json", optional: false, reloadOnChange: true);
JwtAutchenticationScheme.AddJwtAutchenticationScheme(builder.Services, builder.Configuration);

builder.Services.AddOcelot().AddCacheManager(x => x.WithDictionaryHandle());
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(builder =>
    {
        builder.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin();
    });
});


var app = builder.Build();

app.UseCors();
app.UseHttpsRedirection();
//app.UseMiddleware<AttachSignatureToRequest>();
app.UseOcelot().Wait();
app.Run();