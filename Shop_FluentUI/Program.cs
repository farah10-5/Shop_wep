using System.Net;
using Microsoft.EntityFrameworkCore;
using Microsoft.FluentUI.AspNetCore.Components;
using Shop_FluentUI.Components;
using ShopPersistance;
using ShopServices;
using ShopServicesInterfaces;

var builder = WebApplication.CreateBuilder(args);
// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();
builder.Services.AddFluentUIComponents();

builder.Services.AddDbContextFactory<ShopContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("DBConnection"), opt =>

 opt.EnableRetryOnFailure(
     maxRetryCount: 5,
     maxRetryDelay: System.TimeSpan.FromSeconds(30),
     errorNumbersToAdd: null)
     ));


builder.Services.AddScoped<IGiftServices, GiftServices>();
builder.Services.AddScoped<IFlowerServices, FlowerServices>();
builder.WebHost.UseSetting(WebHostDefaults.DetailedErrorsKey, "true");
builder.WebHost.ConfigureKestrel((context, serverOptions) =>
{
    serverOptions.Listen(IPAddress.Loopback, 5000);
}
);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
}

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
