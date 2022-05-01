using Jim.Core.Helpers.Randoms;
using NUnit.Framework;
using System.Collections.Generic;

namespace Jim.Core.Shared.Tests
{
    public class RandomExtensions_Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Should_Generate_Random_String()
        {
            const int stringLength = 100;

            GenerateString(stringLength);
        }

        [Test]
        public void Should_Generate_String_With_Correct_Length()
        {
            const int stringLength = 100;

            var generatedString = GenerateString(stringLength);

            Assert.IsTrue(generatedString.Length == stringLength);
        }

        [Test]
        public void Should_Generate_DifferentStrings()
        {
            const int stringLength = 100;
            const int stringCountToCreate = 10;

            var strings = new HashSet<string>();

            for(var x = 0; x < stringCountToCreate; x++)
            {
                strings.Add(GenerateString(stringLength));
            }

            Assert.IsTrue(strings.Count == stringCountToCreate);
        }

        private string GenerateString(int length)
        {
            var randomString = RNGGenerator.GenerateString(length);


            Assert.IsNotNull(randomString);
            Assert.IsNotEmpty(randomString);

            return randomString;
        }
    }
}