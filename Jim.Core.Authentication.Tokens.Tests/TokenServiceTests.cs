using Jim.Core.Authentication.Models.Database;
using Jim.Core.Authentication.Models.DTOs;
using Jim.Core.Authentication.Models.Interfaces;
using Jim.Core.Authentication.Models.Services;
using Jim.Core.Authentication.Tokens.Service;
using Jim.Core.Authentication.Tokens.Service.Models;
using Jim.Core.Tests.Base;
using Microsoft.IdentityModel.Tokens;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Jim.Core.Authentication.Tokens.Tests
{
    public class TokenServiceTests : TestsBase<TokenServiceTests>
    {
        private const string AUDIENCE = "www.jimtesting.com";
        private const string ISSUER = "www.jimtesting.com";

        private ITokenService? _tokenService;

        private string _privateKey => Config["Authentication:Tokens:PrivateKey"];
        private string _publicKey => Config["Authentication:Tokens:PublicKey"];

        public TokenServiceTests()
        {
        }

        [SetUp]
        public void Setup()
        {
            var options = new TokenGeneratorOptions
            {
                Algorithm = SecurityAlgorithms.RsaSha512,
                Audience = AUDIENCE,
                Issuer = ISSUER,
                PrivateKey = _privateKey,
                PublicKey = _publicKey,
                TimeToLive = TimeSpan.FromSeconds(30)
            };

            _tokenService = new TokenService(options);
        }

        [Test]
        public async Task Service_Should_Generate_Token()
        {
            var testUser = new BasicUserForTest
            {
                Claims = new List<IClaim>()
                {
                    new ClaimDTO{Type = "Role", Value="Testing"}
                },
                CreatedAt = DateTime.UtcNow,
                LastModified = DateTime.UtcNow,
                Id = 1,
                Username = "JimTesting"
            };

            var token = await _tokenService!.GenerateToken(testUser);
            Assert.IsNotNull(token);
            Assert.IsNotEmpty(token.GeneratedToken);
        }

        [Test]
        public async Task Service_Should_Generate_And_Validate_Token()
        {
            var testUser = new BasicUserForTest
            {
                Claims = new List<IClaim>()
                {
                    new ClaimDTO{Type = "Role", Value="Testing"}
                },
                CreatedAt = DateTime.UtcNow,
                LastModified = DateTime.UtcNow,
                Id = 1,
                Username = "JimTesting"
            };

            var token = await _tokenService!.GenerateToken(testUser);
            Assert.IsNotNull(token);
            Assert.IsNotEmpty(token.GeneratedToken);

            var validResult = await _tokenService!.ValidateTokenResult(token.GeneratedToken!);

            Assert.IsTrue(validResult.IsSuccess);
        }
    }
}