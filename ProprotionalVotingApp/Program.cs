using Jim.Blazor.Store.Models.Options;
using Jim.Blazor.Store.Models.Services;
using Jim.Blazor.Store.Services.Stores;
using Microsoft.EntityFrameworkCore;
using ProportionalVotingApp.DatabaseService;
using ProportionalVotingApp.Models.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddDbContext<IVotingRepository, VotingRepository>(options => options.UseSqlServer(builder.Configuration["ConnectionStrings:SqlServer"]));

builder.Services.AddSingleton(new BlazorStoreOptions(StoreType.Local));
builder.Services.AddScoped<IBlazorStoreReader, StoreReader>();
builder.Services.AddScoped<IBlazorStoreWriter, StoreWriter>();

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
