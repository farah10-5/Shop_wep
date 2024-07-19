using Microsoft.EntityFrameworkCore;
using Microsoft.FluentUI.AspNetCore.Components;
using Shop_FluentUI.Components;
using ShopPersistance;
using ShopServices;
using ShopServicesInterfaces;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContextFactory<ShopContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("DBConnection")));
// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();
builder.Services.AddFluentUIComponents();

builder.Services.AddScoped<IGiftServices, GiftServices>();
builder.Services.AddScoped<IFlowerServices, FlowerServices>();


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
