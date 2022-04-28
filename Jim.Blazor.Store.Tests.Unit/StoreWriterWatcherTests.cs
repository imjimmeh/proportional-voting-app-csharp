using Jim.Blazor.Store.Models.Events;
using Jim.Blazor.Store.Models.Options;
using Jim.Blazor.Store.Models.Services;
using Jim.Blazor.Store.Services;
using Jim.Core.Helpers.Randoms;
using NUnit.Framework;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Jim.Blazor.Store.Tests.Unit
{
    public class StoreWriterWatcherTests : StoreServicesTestsBase
    {
        private SpinLock _lock = new SpinLock();

        private const int TIME_TO_WAIT_PER_WRITE_IN_MS = 25;
        private const int CONCURRNECY_TEST_AMOUNT = 100;
        private IStoreWriterWatcher? _watcher;

        private ConcurrentBag<BlazorStoreEntryChangedEventArgs>? _returnedValues;

        public IStoreWriterWatcher Watcher => _watcher ?? throw new ArgumentNullException(nameof(_watcher));

        [SetUp]
        public void Setup()
        {
            if(Writer is not IBlazorStoreWriter writer)
                throw new ArgumentNullException($"{nameof(Writer)} is not expected type of {typeof(IBlazorStoreWriter)}");

            _watcher = new StoreWriterWatcher(Options, JS);
            SetWriter(Watcher);

            Watcher.OnStoreWrite += OnStoreChange;

            _returnedValues = new ConcurrentBag<BlazorStoreEntryChangedEventArgs>();
        }

        [Test, Order(1)]
        public async Task Watcher_Should_Fire_On_Change()
        {
            var (key, value) = await GenerateAndSetRandomKeyAndValue();

            Thread.Sleep(TIME_TO_WAIT_PER_WRITE_IN_MS);

            var firstInBag = _returnedValues?.FirstOrDefault();
            ValidateResult(key, firstInBag);
        }


        [Test, Order(2)]
        public async Task Watcher_Should_Fire_On_Multiple_Changes()
        {
            var keys = GenerateKeys();

            var tasks = keys.AsParallel().Select(async key => await SetKeyValue(key, GenerateRandomValue())).ToArray();

            var complete = Task.WaitAll(tasks, TimeSpan.FromSeconds(10));

            Thread.Sleep(1000);

            Assert.True(_returnedValues.Count == keys.Count, $"Expected {keys.Count} returned values but received {_returnedValues.Count}");

            foreach (var key in keys)
            {
                var matchingValue = _returnedValues.Where(value => value.Key == key).FirstOrDefault();

                Assert.NotNull(matchingValue);
                ValidateResult(key, matchingValue);
            }
        }

        private void ValidateResult(string? key, BlazorStoreEntryChangedEventArgs? result)
        {
            Assert.NotNull(result);

            Assert.AreEqual(result!.Key, key);
            Assert.AreEqual(result.Method, JsStoreMethod.Get);
            Assert.AreEqual(result.StoreType, Watcher.Options.StoreToUse.ToStoreType());
        }

        private static HashSet<string> GenerateKeys()
        {
            var keys = new HashSet<string>(CONCURRNECY_TEST_AMOUNT);

            for (var x = 0; x < CONCURRNECY_TEST_AMOUNT; x++)
            {
                keys.Add(GenerateRandomKey());
            }

            return keys;
        }

        private void OnStoreChange(object? sender, BlazorStoreEntryChangedEventArgs args)
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

