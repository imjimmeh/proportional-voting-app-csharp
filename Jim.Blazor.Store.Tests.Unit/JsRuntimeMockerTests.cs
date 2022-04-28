using Jim.Blazor.Store.Models.Options;
using Jim.Core.Helpers.Randoms;
using Microsoft.JSInterop;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace Jim.Blazor.Store.Tests.Unit
{
    public class JsRuntimeMockerTests
    {
        private readonly BlazorStoreOptions _options = new BlazorStoreOptions(StoreType.Local);

        private IJSRuntime? _runtime;

        [SetUp]
        public void Setup()
        {
            var storeMocker = new StoreMocker();
            _runtime = new IJSRuntimeMocker(storeMocker);
        }

        [Test, Order(1)]
        public async Task Should_SetItemAsync()
        {
            if (_runtime == null)
                Assert.Fail("JsRuntimeMocker is null");

            await GenerateAndSetRandomValuesAsync();

            Assert.Pass();
        }

        [Test, Order(2)]
        public async Task Should_GetItem()
        {
            (string key, string value) = await GenerateAndSetRandomValuesAsync();

            var get = await _runtime.InvokeAsync<string>(_options.GetMethodPath(JsStoreMethod.Get), key);
            Assert.AreEqual(value, get);
        }

        [Test]
        public async Task Should_Not_Get_Key_With_Space()
        {
            Assert.NotNull(_runtime);

            try
            {
                (string key, string value) = await GenerateAndSetRandomValuesAsync();
                key = key + " ";

                var get = await _runtime.InvokeAsync<string>(_options.GetMethodPath(JsStoreMethod.Get), key);

                Assert.True(string.IsNullOrEmpty(get));
            }
            catch(Exception ex)
            {
                Assert.Pass();
            }
        }

        [Test]
        public async Task Should_Not_Get_RandomKey()
        {
            Assert.NotNull(_runtime);

            try
            {
                var randomKey = GenerateString();
                var get = await _runtime.InvokeAsync<string>(_options.GetMethodPath(JsStoreMethod.Get), randomKey);

                Assert.True(string.IsNullOrEmpty(get));
            }
            catch (Exception ex)
            {
                Assert.Pass();
            }
        }

        private async Task<(string key, string value)> GenerateAndSetRandomValuesAsync()
        {
            if(_runtime == null)
                throw new ArgumentNullException(nameof(_runtime));

            var testValues = GetKeyValueToTest();

            await _runtime.InvokeVoidAsync(_options.GetMethodPath(JsStoreMethod.Set), testValues.key, testValues.value);

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
