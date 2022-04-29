using Jim.Blazor.Store.Models.Events;
using Jim.Blazor.Store.Models.Options;
using Jim.Blazor.Store.Models.Services;
using Jim.Blazor.Store.Models.Tests;
using Jim.Blazor.Store.Services;
using Jim.Blazor.Store.Tests.Unit.CoreTests;
using Jim.Core.Store.Models.Events;
using NUnit.Framework;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Jim.Blazor.Store.Tests.Unit
{
    public class WatcherFactoryTests : BlazorStoreServicesTestsBase
    {
        private IStoreWriterWatcherFactory _watcherFactory;
        private ConcurrentBag<IStoreEventArgs> _receivedEventArgs;

        public WatcherFactoryTests() : base(new BlazorStoreOptions(StoreType.Local))
        {
            if (Writer is IBlazorStoreWriter writer)
                _watcherFactory = new StoreWriterWatcherFactory(writer);
            else
                throw new Exception($"{nameof(Writer)} is not type {typeof(IBlazorStoreWriter)} - is {Writer.GetType()}");

            _receivedEventArgs = new ConcurrentBag<IStoreEventArgs>();
        }

        [SetUp]
        public void Setup()
        {
            _receivedEventArgs.Clear();
        }

        [Test, Order(1)]
        public void Factory_Should_Create_Watchers()
        {
            CreateAndValidateWatcher();
        }

        private IBlazorStoreWriterWatcher<TestStoreModel?> CreateAndValidateWatcher()
        {
            var watcher = _watcherFactory.GetWatcher<TestStoreModel>();
            Assert.NotNull(watcher);
            Assert.True(watcher is StoreWriterWatcher<TestStoreModel?> concrete);

            return watcher;
        }

        [Test, Order(2)]
        public async Task Watcher_Should_Fire_Events()
        {
            var watcher = CreateAndValidateWatcher();
            SetWriter(watcher);
            watcher.OnStoreWrite += OnStoreWrite;

            (string key, TestStoreModel value) = await GenerateAndSetRandomKeyAndValue();
            Thread.Sleep(1000);

            Assert.True(_receivedEventArgs.Count > 0);

            var first = _receivedEventArgs.FirstOrDefault();

            Assert.NotNull(first);

            Assert.AreEqual(first!.Key, key);

            if (first is not BlazorStoreEntryChangedEventArgs<TestStoreModel?> rightArgs)
                Assert.Fail($"Received value is not type {typeof(BlazorStoreEntryChangedEventArgs<TestStoreModel?>)} - received {first.GetType()}");
        }

        private void OnStoreWrite(object? sender, BlazorStoreEntryChangedEventArgs<TestStoreModel?> e)
        {
            _receivedEventArgs.Add(e);
        }
    }
}
