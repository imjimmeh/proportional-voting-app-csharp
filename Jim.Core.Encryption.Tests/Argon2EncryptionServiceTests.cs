using Jim.Core.Encryption.Models;
using Jim.Core.Encryption.Service;
using Jim.Core.Helpers.Randoms;
using NUnit.Framework;

namespace Jim.Core.Encryption.Tests
{

    public class Argon2EncryptionServiceTests
    {
        private const string TESTING_PASSWORD = "Testing";
        private const string TESTING_SECRET = "ASDsdncjk32nr12n12,dc0asucs";
        private IEncryptionService? _encryptionService;

        [SetUp]
        public void Setup()
        {
            GenerateEncryptionService(TESTING_SECRET);
        }

        private void GenerateEncryptionService(string secret)
        {
            var options = new Argon2EncryptionOptions
            {
                Secret = secret
            };

            _encryptionService = new Argon2EncryptionService(options);
        }

        [Test, Order(1)]
        public void Argon2Service_Should_HashString()
        {
            var result = HashString();
        }

        [Test, Order(2)]
        public void Argon2Service_Should_Hash_And_Decrypt()
        {
            HashedStringWithSalt result = HashString();

            var verified = _encryptionService.VerifyHashedString(TESTING_PASSWORD, result);

            Assert.IsTrue(verified);
        }

        [Test, Order(3)]
        public void Argon2Service_Should_Not_Verify_Completely_Incorrect_Password()
        {
            HashedStringWithSalt result = HashString();

            var verified = _encryptionService.VerifyHashedString(RNGGenerator.GenerateString(15), result);

            Assert.IsFalse(verified);
        }

        [Test, Order(4)]
        public void Argon2Service_Should_Not_Verify_DifferentCaps_Password()
        {
            HashedStringWithSalt result = HashString();

            var verified = _encryptionService.VerifyHashedString(TESTING_PASSWORD.ToLower(), result);

            Assert.IsFalse(verified);
        }

        [Test, Order(5)]
        public void Argon2Service_Should_Not_Verify_Password_With_Preceding_Space()
        {
            HashedStringWithSalt result = HashString();

            var verified = _encryptionService.VerifyHashedString(" " + TESTING_PASSWORD, result);

            Assert.IsFalse(verified);
        }

        [Test, Order(6)]
        public void Argon2Service_Should_Not_Verify_Password_With_DifferentSalt()
        {
            HashedStringWithSalt result = HashString();

            result = new HashedStringWithSalt(result.HashedString, "testing");
            var verified = _encryptionService.VerifyHashedString(TESTING_PASSWORD, result);

            Assert.IsFalse(verified);
        }

        [Test, Order(7)]
        public void Argon2Service_Should_Not_Verify_With_Different_Secret()
        {
            HashedStringWithSalt result = HashString();

            var verified = _encryptionService.VerifyHashedString(RNGGenerator.GenerateString(15), result);

            Assert.IsFalse(verified);
        }


        private HashedStringWithSalt HashString(string password = TESTING_PASSWORD)
        {
            var result = _encryptionService.HashString(password);

            Assert.IsNotNull(result);
            Assert.IsNotEmpty(result.HashedString);
            Assert.IsNotEmpty(result.Salt);
            return result;
        }
    }
}