using Jim.Blazor.Store.Models.Options;
using Jim.Blazor.Store.Models.Services;
using Jim.Blazor.Store.Services;
using Jim.Blazor.Store.Services.Stores;
using Jim.Core.Authentication.Database.Service;
using Jim.Core.Authentication.Http.Service;
using Jim.Core.Authentication.Models.Database;
using Jim.Core.Authentication.Models.Services;
using Jim.Core.Authentication.Service;
using Jim.Core.Encryption.Models;
using Jim.Core.Encryption.Service;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
var config = builder.Configuration;

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddHttpContextAccessor();

builder.Services.AddHttpClient<IAuthenticationHttpService>(client => client.BaseAddress = new Uri(config["ConnectionStrings:AuthenticationApiUrl"]));

builder.Services.AddScoped<IAuthenticationHttpService, AuthenticationHttpService>();
builder.Services.AddScoped<IUserSignInManager<User>, UserSignInManager<User>>();
builder.Services.AddScoped<IBlazorStoreReader, StoreReader>();
builder.Services.AddScoped<IBlazorStoreWriter, StoreWriter>();

builder.Services.AddSingleton(new BlazorStoreOptions(StoreType.Local));
builder.Services.AddScoped<IStoreWriterWatcherFactory, StoreWriterWatcherFactory>();

builder.Services.AddDbContext<IUserStore<User>, UsersDbContext>(options =>
{
    options.UseSqlServer(config["ConnectionStrings:SqlServer"]);
});

builder.Services.AddScoped<IUserManagerService, UserManagerService<User>>();
builder.Services.AddSingleton(new Argon2EncryptionOptions { Secret = builder.Configuration["EncryptionSecret"] });
builder.Services.AddScoped<IEncryptionService, Argon2EncryptionService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
