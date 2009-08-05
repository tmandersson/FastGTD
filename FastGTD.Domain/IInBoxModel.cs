using System;
using System.Collections.Generic;
using FastGTD.DataTransfer;

namespace FastGTD.Domain
{
    public interface IInBoxModel
    {
        void Load();
        InBoxItem Add(string item);
        void Remove(InBoxItem item);
        IList<InBoxItem> Items { get; }
        event Action Changed;
    }
}