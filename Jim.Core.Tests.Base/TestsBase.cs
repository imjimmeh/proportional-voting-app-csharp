using NUnit.Framework;
using System;

namespace Jim.Core.Tests.Base
{
    public class TestsBase
    {
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