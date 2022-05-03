using Jim.Blazor.Store.Models.Options;
using Jim.Core.Tests.Base;
using NUnit.Framework;
using System;

namespace Jim.Blazor.Store.Tests.Unit.CoreTests
{
    public class JsStoreMethodTests : TestsBase<JsStoreMethodTests>
    {
        [Test]
        public void Should_Return_GetItem()
        {
            try
            {
                ObjectsAreEqualsAndSecondIsNotNull(JsStoreMethod.Get.ToMethodName(), JsStoreMethods.GET_METHOD);
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }

        [Test]
        public void Should_Return_SetItem()
        {
            try
            {
                ObjectsAreEqualsAndSecondIsNotNull(JsStoreMethod.Set.ToMethodName(), JsStoreMethods.SET_METHOD);
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }

        [Test]
        public void Should_Not_ReturnItem()
        {
            try
            {
                var methodName = JsStoreMethod.None.ToMethodName();

                Assert.IsTrue(string.IsNullOrEmpty(methodName), $"Received {methodName} but should not have received value");
            }
            catch (Exception ex)
            {
                Assert.True(true);
            }

        }
    }
}
