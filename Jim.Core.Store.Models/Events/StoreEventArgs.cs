using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jim.Core.Store.Models.Events
{
    public interface IStoreEventArgs
    {
        string Key { get; }
    }
}
