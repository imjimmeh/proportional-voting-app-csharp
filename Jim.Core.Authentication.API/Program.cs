using Jim.Core.Authentication.Database.Service;
using Jim.Core.Authentication.Models.Database;
using Jim.Core.Authentication.Models.Services;
using Jim.Core.Authentication.Service;
using Jim.Core.Encryption.Models;
using Jim.Core.Encryption.Service;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<IUserStore<User>, UsersDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration["ConnectionStrings:SqlServer"]);
});

builder.Services.AddSingleton(new Argon2EncryptionOptions { Secret = builder.Configuration["EncryptionSecret"] });
builder.Services.AddScoped<IEncryptionService, Argon2EncryptionService>();
builder.Services.AddScoped<IUserManagerService, UserManagerService<User>>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
