﻿using System.Threading.Tasks;

namespace Jim.Blazor.Store.Tests.Unit
{
    public interface IStoreMocker
    {
        Task<string> GetItem(string key);
        Task<bool> SetItem(string key, string value);
    }
}