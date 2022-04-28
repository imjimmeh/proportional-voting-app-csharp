using Jim.Blazor.Store.Models.Options;
using Jim.Blazor.Store.Models.Tests;
using Jim.Blazor.Store.Services;
using Jim.Core.Helpers.Randoms;
using Jim.Core.Store.Models.Services;
using Microsoft.JSInterop;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jim.Blazor.Store.Tests.Unit
{
    public class StoreServicesTests
    {
        private IJSRuntime? _js;

        private IStoreReader? _storeReader;

        private IStoreWriter? _storeWriter;

        private BlazorStoreOptions _options = new BlazorStoreOptions(StoreType.Session);

        public IJSRuntime JS { get { return _js ??= new IJSRuntimeMocker(); } }

        public IStoreReader Reader { get { return _storeReader ??= new StoreReader(_options, JS); } }
        public IStoreWriter Writer { get { return _storeWriter ??= new StoreWriter(_options, JS); } }

        [Test, Order(1)]
        public async Task Writer_Should_WriteObject()
        {
            await SetRandomValue();
        }


        [Test, Order(2)]
        public async Task Reader_Should_GetObject()
        {
            try
            {
                var generated = await SetRandomValue();

                var readerResult = await Reader.GetAsync<TestStoreModel>(generated.key);

                if (readerResult == null)
                    throw new Exception("Retrieved null result back from store, but should have stored value");

                Assert.AreEqual(generated.value.TestNullableInt, readerResult.TestNullableInt);
                Assert.AreEqual(generated.value.TestInt, readerResult.TestInt);
                Assert.AreEqual(generated.value.TestBool, readerResult.TestBool);
                Assert.AreEqual(generated.value.TestString, readerResult.TestString);

            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }

        private async Task<(string key, TestStoreModel value)> SetRandomValue()
        {
            try
            {
                var generated = GenerateRandomKeyAndValue();
                var result = await Writer.WriteAsync(generated.key, generated.value);

                Assert.True(result);

                return generated;
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
                throw;
            }
        }

        private static (string key, TestStoreModel value) GenerateRandomKeyAndValue()
        {
            var value = new TestStoreModel
            {
                TestBool = true,
                TestInt = 123,
                TestNullableInt = null,
                TestString = RNGGenerator.GenerateString(20)
            };
            var key = RNGGenerator.GenerateString(10);

            return new(key, value);
        }
    }
}
