using Jim.Blazor.Store.Models;
using Jim.Blazor.Store.Models.Options;
using Jim.Blazor.Store.Services.Stores;
using Jim.Core.Store.Models.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ProportionalVotingApp.DatabaseService;
using ProportionalVotingApp.Models.DTOs;
using ProportionalVotingApp.Models.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddDbContext<IVotingRepository, VotingRepository>(options => options.UseSqlServer("Data Source=192.168.1.161;Initial Catalog=VotingApp;Persist Security Info=True;User ID=SA;Password=r1pz33z!l0l"));

builder.Services.AddSingleton(new BlazorStoreOptions(StoreType.Local));
builder.Services.AddScoped<IStoreReader, StoreReader>();
builder.Services.AddScoped<IStoreWriter, StoreWriter>();

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
