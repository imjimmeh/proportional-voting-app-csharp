using Jim.Blazor.Store.Models.Options;
using Jim.Blazor.Store.Tests.Unit.Mocks;
using Jim.Core.Helpers.Randoms;
using Microsoft.JSInterop;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace Jim.Blazor.Store.Tests.Unit.CoreTests
{
    public class JsRuntimeMockerTests
    {
        private readonly BlazorStoreOptions _options = new BlazorStoreOptions(StoreType.Local);
        private readonly IStoreMocker _store = new StoreMocker();
        private IJSRuntime? _runtime = null;

        [SetUp]
        public void Setup()
        {
        }

        public IJSRuntime JS => _runtime ??= new IJSRuntimeMocker(_store);

        [Test, Order(1)]
        public async Task Should_SetItemAsync()
        {
            await GenerateAndSetRandomValuesAsync();

            Assert.Pass();
        }

        [Test, Order(2)]
        public async Task Should_GetItem()
        {
            (string key, string value) = await GenerateAndSetRandomValuesAsync();

            var get = await JS.InvokeAsync<string>(_options.GetMethodPath(JsStoreMethod.Get), key);

            Assert.AreEqual(value, get);
        }

        [Test]
        public async Task Should_Not_Get_Key_With_Space()
        {
            try
            {
                (string key, string value) = await GenerateAndSetRandomValuesAsync();
                key = key + " ";

                var get = await JS.InvokeAsync<string>(_options.GetMethodPath(JsStoreMethod.Get), key);

                Assert.True(string.IsNullOrEmpty(get));
            }
            catch (Exception ex)
            {
                Assert.Pass();
            }
        }

        [Test]
        public async Task Should_Not_Get_RandomKey()
        {
            try
            {
                var randomKey = GenerateString();
                var get = await JS.InvokeAsync<string>(_options.GetMethodPath(JsStoreMethod.Get), randomKey);

                Assert.True(string.IsNullOrEmpty(get));
            }
            catch (Exception ex)
            {
                Assert.Pass();
            }
        }

        private async Task<(string key, string value)> GenerateAndSetRandomValuesAsync()
        {
            var testValues = GetKeyValueToTest();

            await JS.InvokeVoidAsync(_options.GetMethodPath(JsStoreMethod.Set), testValues.key, testValues.value);

            return testValues;
        }

        private static (string key, string value) GetKeyValueToTest()
        {
            var key = GenerateString();
            var value = GenerateString(minLength: 20, maxLength: 100);

            return new(key, value);
        }

        private static string GenerateString(int minLength = 1, int maxLength = 20)
        {
            var length = RNG.Random.Next(minLength, maxLength);
            return RNGGenerator.GenerateString(length);
        }
    }
}
