using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jim.Core.Store.Models.Services
{
    public interface IStoreReader
    {
        public Task<T?> GetAsync<T>(string key) where T : class;
    }
}
