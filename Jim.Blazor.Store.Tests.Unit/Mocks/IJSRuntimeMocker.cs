using Jim.Blazor.Store.Models.Options;
using Microsoft.JSInterop;
using NUnit.Framework;
using System;
using System.Diagnostics.CodeAnalysis;
using System.Threading;
using System.Threading.Tasks;

namespace Jim.Blazor.Store.Tests.Unit
{
    public class IJSRuntimeMocker : IJSRuntime
    {
        private IStoreMocker _storeMocker;

        public IJSRuntimeMocker(IStoreMocker? storeMocker = null)
        {
            _storeMocker = storeMocker ?? new StoreMocker();
        }

        public ValueTask<TValue> InvokeAsync<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicConstructors | DynamicallyAccessedMemberTypes.PublicFields | DynamicallyAccessedMemberTypes.PublicProperties)] TValue>(string identifier, object?[]? args)
        {
            (string storeType, string action) = GetValuesFromIdentifier(identifier);

            var method = GetStoreMethod(storeType, action);

            switch (method)
            {
                case JsStoreMethod.Get:
                    return GetFromStore<TValue>(args);
                case JsStoreMethod.Set:
                    {
                        SetStore(args);
                        return new ValueTask<TValue>();
                    }
                case JsStoreMethod.None:
                    throw new Exception("Did not receive a valid JsStoreMethod");
                default:
                    throw new Exception("Method not mapped");
            }
        }

        private static JsStoreMethod GetStoreMethod(string storeType, string action)
        {
            var asEnum = storeType.ToStoreType();

            if (asEnum == StoreType.None)
                throw new Exception("Did not receive a valid storetype");

            var method = action.ToJsStoreMethod();
            return method;
        }

        private async ValueTask<TValue> GetFromStore<TValue>(object?[]? args)
        {
            string key = GetKey(args, 1);

            var value = await _storeMocker.GetItem(key);

            if (value is TValue tvalue)
                return tvalue;

            throw new Exception($"Value is not expected type - expected {typeof(TValue)} but received {value.GetType()}");
        }

        private static string GetKey(object?[]? args, int argumentCount)
        {
            ValidateArguments(args, argumentCount);

            var key = ObjectToString(args![0], "key", false);
            return key;
        }

        private static string ObjectToString(object? obj, string parameterName, bool allowNull)
        {
            if (obj == null && !allowNull)
                throw new ArgumentNullException(parameterName);
            else if (obj is string s)
            {
                if (!allowNull && string.IsNullOrEmpty(s))
                    throw new Exception(parameterName);

                return s;
            }

            throw new Exception($"Expected string key but type is {obj.GetType()}");
        }
        private static void ValidateArguments(object?[]? args, int expectedArgumentCount)
        {
            if (args == null)
                throw new ArgumentNullException("Received no arguments");

            if (args.Length != expectedArgumentCount)
                throw new ArgumentException($"Expected {expectedArgumentCount} argument{(expectedArgumentCount > 1 ? "s" : "") } (key) - received {args.Length}");
        }

        private ValueTask SetStore(object?[]? args)
        {
            var key = GetKey(args, 2);
            var value = ObjectToString(args![1], "value", true);

            _storeMocker.SetItem(key, value);

            return new ValueTask();
        }

        public ValueTask<TValue> InvokeAsync<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicConstructors | DynamicallyAccessedMemberTypes.PublicFields | DynamicallyAccessedMemberTypes.PublicProperties)] TValue>(string identifier, CancellationToken cancellationToken, object?[]? args)
        {
            throw new NotImplementedException();
        }

        private (string storeType, string action) GetValuesFromIdentifier(string? identifier)
        {
            if (string.IsNullOrEmpty(identifier))
                throw new ArgumentNullException(nameof(identifier));

            var indexOfSeperator = identifier.IndexOf('.');

            if (indexOfSeperator == -1)
                throw new Exception("Invalid identitifer - did not have seperator");
            try
            {
                int end = indexOfSeperator - 1;
                int start = indexOfSeperator + 1;

                return new(identifier[..indexOfSeperator], identifier[start..]);
            }
            catch (Exception ex)
            {
                throw new Exception("Error splitting string", ex);
            }
        }
    }
}