using Jim.Blazor.Store.Models.Events;
using Jim.Blazor.Store.Models.Options;
using Jim.Blazor.Store.Models.Services;
using Jim.Blazor.Store.Models.Tests;
using Jim.Blazor.Store.Services;
using NUnit.Framework;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Jim.Blazor.Store.Tests.Unit
{
    public class StoreWriterWatcherTests : BlazorStoreServicesTestsBase
    {
        private SpinLock _lock = new SpinLock();

        private const int TIME_TO_WAIT_PER_WRITE_IN_MS = 25;
        private const int CONCURRNECY_TEST_AMOUNT = 100;
        private IBlazorStoreWriterWatcher<TestStoreModel>? _watcher;

        private ConcurrentBag<BlazorStoreEntryChangedEventArgs<TestStoreModel?>>? _returnedValues;

        public StoreWriterWatcherTests() : base(new BlazorStoreOptions(StoreType.Local))
        {
        }

        public IBlazorStoreWriterWatcher<TestStoreModel> Watcher => _watcher ?? throw new ArgumentNullException(nameof(_watcher));

        [SetUp]
        public void Setup()
        {

            _watcher = Writer is IBlazorStoreWriter blazorWriter ?
                new StoreWriterWatcher<TestStoreModel>(blazorWriter) :
                throw new InvalidCastException($"{nameof(Writer)} is not of expected type {typeof(IBlazorStoreWriter)} - is {Writer.GetType()}");

            SetWriter(Watcher);

            Watcher.OnStoreWrite += OnStoreChange;

            if (_returnedValues != null)
                _returnedValues.Clear();

            _returnedValues = new ConcurrentBag<BlazorStoreEntryChangedEventArgs<TestStoreModel?>>();
        }

        [TearDown]
        public void TearDown()
        {
            Watcher.OnStoreWrite -= OnStoreChange;
            _returnedValues?.Clear();
        }

        [Test, Order(1)]
        public async Task Watcher_Should_Fire_On_Change()
        {
            var (key, value) = await GenerateAndSetRandomKeyAndValue();

            Thread.Sleep(TIME_TO_WAIT_PER_WRITE_IN_MS);

            var firstInBag = _returnedValues?.FirstOrDefault();
            ValidateResult(key, value, firstInBag);
        }


        [Test, Order(2)]
        public async Task Watcher_Should_Fire_On_Multiple_Changes()
        {
            var keyValuePairs = GenerateKeysAndValues();
            var tasks = keyValuePairs.AsParallel().Select(async key => await SetKeyValue(key.Key, key.Value)).ToArray();

            var complete = Task.WaitAll(tasks, TimeSpan.FromSeconds(10));

            Thread.Sleep(1000);

            Assert.True(_returnedValues.Count == keyValuePairs.Count, $"Expected {keyValuePairs.Count} returned values but received {_returnedValues.Count}");

            foreach ((string key, TestStoreModel value) in keyValuePairs)
            {
                var matchingValue = _returnedValues.Where(value => value.Key == key).FirstOrDefault();

                Assert.NotNull(matchingValue);
                ValidateResult(key, value, matchingValue);
            }
        }

        private void ValidateResult(string? key, TestStoreModel expectedValue, BlazorStoreEntryChangedEventArgs<TestStoreModel?> result)
        {
            Assert.NotNull(result);

            Assert.AreEqual(result!.Key, key);
            Assert.AreEqual(result.Method, JsStoreMethod.Set);
            Assert.AreEqual(result.StoreType, Watcher.Options.StoreToUse.ToStoreType());
            Assert.AreEqual(expectedValue, result.NewValue);
        }

        private static Dictionary<string, TestStoreModel> GenerateKeysAndValues()
        {
            var keyValuePairs = new Dictionary<string, TestStoreModel>();

            for (var x = 0; x < CONCURRNECY_TEST_AMOUNT; x++)
            {
                keyValuePairs.Add(GenerateRandomKey(), GenerateRandomValue());
            }

            return keyValuePairs;
        }

        private void OnStoreChange(object? sender, BlazorStoreEntryChangedEventArgs<TestStoreModel> args)
        {
            bool haveLock = false;

            try
            {
                _lock.Enter(ref haveLock);
                _returnedValues?.Add(args);
            }
            catch(Exception ex)
            {
                Assert.Fail(ex.ToString());
            }
            finally
            {
                _lock.Exit();
            }
        }
    }
}

