using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jim.Blazor.Store.Models.Tests
{
    public record TestStoreModel
    {
        public TestStoreModel()
        {
        }

        public TestStoreModel(string testString, int testInt, bool testBool, int? testNullableInt)
        {
            TestString = testString;
            TestInt = testInt;
            TestBool = testBool;
            TestNullableInt = testNullableInt;
        }

        public string? TestString { get; init; }

        public int TestInt { get; init; }

        public bool TestBool { get; init; }

        public int? TestNullableInt { get; init; }
    }
}
