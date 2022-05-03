using Jim.Core.Authentication.Database.Service;
using Jim.Core.Authentication.Models.Database;
using Jim.Core.Authentication.Models.Services;
using Jim.Core.Authentication.Service;
using Jim.Core.Authentication.Tokens.Service;
using Jim.Core.Authentication.Tokens.Service.Models;
using Jim.Core.Encryption.Models;
using Jim.Core.Encryption.Service;
using Jim.Core.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

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
builder.Services.AddSingleton<ITokenGeneratorOptions>(new TokenGeneratorOptions
{
    Audience = "www.jim.com",
    Issuer = "www.jim.com",
    Algorithm = SecurityAlgorithms.RsaSha512,
    PrivateKey = builder.Configuration["Authentication:Tokens:PrivateKey"],
    PublicKey = builder.Configuration["Authentication:Tokens:PublicKey"],
    TimeToLive = TimeSpan.FromDays(7)
});

builder.Services.AddScoped<IEncryptionService, Argon2EncryptionService>();
builder.Services.AddScoped<ILoginService, UserManagerService<User>>();
builder.Services.AddScoped<IUserManagerService, UserManagerService<User>>();

builder.Services.AddScoped<IUserTokenService, UserTokenService>();
builder.Services.AddScoped<ITokenService, TokenService>();

builder.Services.AddAuthentication("JimCustom").AddScheme<TokenAuthenticationHandlerOptions, TokenAuthenticationHandler>("JimCustom", options =>
{
    options.AuthenticationHeader = "Jimmeh";
});
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();
app.UseAuthentication();
app.MapControllers();

app.Run();
