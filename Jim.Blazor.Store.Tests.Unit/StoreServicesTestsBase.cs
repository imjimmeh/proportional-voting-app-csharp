using Jim.Blazor.Store.Models.Options;
using Jim.Blazor.Store.Models.Tests;
using Jim.Blazor.Store.Services.Stores;
using Jim.Blazor.Store.Tests.Unit.Mocks;
using Jim.Core.Helpers.Randoms;
using Jim.Core.Store.Models.Services;
using Microsoft.JSInterop;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace Jim.Blazor.Store.Tests.Unit
{
    public class StoreServicesTestsBase
    {
        private BlazorStoreOptions _options = new BlazorStoreOptions(StoreType.Session);

        private IJSRuntime? _js;

        private IStoreReader? _storeReader;

        private IStoreWriter? _storeWriter;


        public BlazorStoreOptions Options => _options;

        public IJSRuntime JS { get { return _js ??= new IJSRuntimeMocker(); } }
        public IStoreReader Reader { get { return _storeReader ??= new StoreReader(_options, JS); } }
        public IStoreWriter Writer { get { return _storeWriter ??= new StoreWriter(_options, JS); } }

        protected internal void SetWriter(IStoreWriter writer) => _storeWriter = writer;

        protected internal virtual (string key, TestStoreModel value) GenerateRandomKeyAndValue()
        {
            var key = GenerateRandomKey();

            var model = GenerateRandomValue();

            return new(key, model);
        }

        protected internal static string GenerateRandomKey() => RNGGenerator.GenerateString(10);

        protected internal static TestStoreModel GenerateRandomValue()
            => new TestStoreModel
            {
                TestBool = true,
                TestInt = 123,
                TestNullableInt = null,
                TestString = RNGGenerator.GenerateString(20)
            };

        protected internal virtual async Task<(string key, TestStoreModel value)> GenerateAndSetRandomKeyAndValue()
        {
            try
            {
                var generated = GenerateRandomKeyAndValue();
                var result = await SetKeyValue(generated.key, generated.value);

                Assert.True(result);

                return generated;
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
                throw;
            }
        }

        protected internal virtual async Task<bool> SetKeyValue<T>(string key, T value)
            where T : class
        {
            try
            {
                var result = await Writer.WriteAsync(key, value);

                Assert.True(result);

                return result;
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
                throw;
            }
        }
    }
}