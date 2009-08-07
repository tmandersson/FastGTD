using System;
using System.Collections.Generic;

namespace FastGTD.Domain
{
    public interface IGTDItemModel<T>
    {
        void Load();
        void ClearItems();
        IList<T> Items { get; }
        T Add(string item);
        void Remove(T item);
        event Action Changed;
    }
}