using Jim.Blazor.Store.Models.Tests;
using Jim.Blazor.Store.Tests.Unit.Mocks;
using Jim.Core.Helpers.Randoms;
using Jim.Core.Store.Models.Options;
using Jim.Core.Store.Models.Services;
using Microsoft.JSInterop;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace Jim.Blazor.Store.Tests.Unit.CoreTests
{
    public abstract class StoreServicesTestsBase<TOptions>
        where TOptions : StoreOptions
    {
        private TOptions _options;

        private IJSRuntime? _js;

        private IStoreReader<TOptions>? _storeReader;

        private IStoreWriter<TOptions>? _storeWriter;

        public StoreServicesTestsBase(TOptions options)
        {
            _options = options ?? throw new ArgumentNullException(nameof(options));
        }

        public TOptions Options => _options;
        public IJSRuntime JS => _js ??= new IJSRuntimeMocker();
        public IStoreReader<TOptions> Reader => _storeReader ??= CreateReader();
        public IStoreWriter<TOptions> Writer => _storeWriter ??= CreateWriter();

        protected internal abstract IStoreReader<TOptions> CreateReader();

        protected internal abstract IStoreWriter<TOptions> CreateWriter();

        protected internal void SetWriter(IStoreWriter<TOptions> writer)
            => _storeWriter = writer;

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