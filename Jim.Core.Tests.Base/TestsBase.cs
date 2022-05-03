using Microsoft.Extensions.Configuration;
using NUnit.Framework;
using System;
using System.Reflection;

namespace Jim.Core.Tests.Base
{
    public class TestsBase<TConcrete>
        where TConcrete : TestsBase<TConcrete>
    {
        protected internal readonly IConfigurationRoot Config;

        public TestsBase()
        {
            Config = BuildConfig();
        }
        
        public IConfigurationRoot BuildConfig()
        {
            var environmentName = Environment.GetEnvironmentVariable("Hosting:Environment");

            var config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", true)
                .AddJsonFile($"appsettings.{environmentName}.json", true)
                .AddEnvironmentVariables();

            config.AddUserSecrets(Assembly.GetAssembly(typeof(TConcrete)));

            return config.Build();
        }
        public void ObjectsAreEqualsAndSecondIsNotNull(object expected, object toCompare)
        {
            try
            {
                Assert.IsNotNull(toCompare);

                if(toCompare is string s)
                    Assert.IsNotEmpty(s);

                Assert.AreEqual(expected, toCompare,
                    $"Did not receive right value - expected {expected} but received {toCompare}");
            }
            catch (Exception ex)
            {
                Assert.Fail($"Unknown error - {ex.Message}");
            }
        }
    }
}