using Jim.Blazor.Store.Models.Options;
using Jim.Blazor.Store.Models.Services;
using Jim.Blazor.Store.Services;
using Jim.Blazor.Store.Services.Stores;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();

builder.Services.AddScoped<IBlazorStoreReader, StoreReader>();
builder.Services.AddScoped<IBlazorStoreWriter, StoreWriter>();

builder.Services.AddSingleton(new BlazorStoreOptions(StoreType.Local));
builder.Services.AddScoped<IStoreWriterWatcherFactory, StoreWriterWatcherFactory>();

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
