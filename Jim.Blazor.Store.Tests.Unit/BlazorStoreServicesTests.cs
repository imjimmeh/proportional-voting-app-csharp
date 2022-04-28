using Jim.Blazor.Store.Models.Options;
using Jim.Blazor.Store.Models.Tests;
using Jim.Blazor.Store.Services;
using Jim.Blazor.Store.Services.Stores;
using Jim.Blazor.Store.Tests.Unit.Mocks;
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
    public class BlazorStoreServicesTests : BlazorStoreServicesTestsBase
    {
        public BlazorStoreServicesTests() : base(new BlazorStoreOptions(StoreType.Local))
        {
        }

        [Test, Order(1)]
        public async Task Writer_Should_WriteObject()
        {
            await GenerateAndSetRandomKeyAndValue();
        }

        [Test, Order(2)]
        public async Task Reader_Should_GetObject()
        {
            try
            {
                var generated = await GenerateAndSetRandomKeyAndValue();

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
    }
}
