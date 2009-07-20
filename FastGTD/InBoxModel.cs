using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using FastGTD.DataAccess;
using FastGTD.DataTransfer;

namespace FastGTD
{
    public class InBoxModel : IInBoxModel
    {
        private readonly IList<InBoxItem> _items = new List<InBoxItem>();

        private readonly IInBoxPersistenceProvider _persistence;
        public event Action Changed;

        public InBoxModel(IInBoxPersistenceProvider persistence)
        {
            _persistence = persistence;
        }

        public void Load()
        {
            IList<InBoxItem> loaded_items = _persistence.GetAll();
            foreach (InBoxItem item in loaded_items)
            {
                _items.Add(item);
            }
            FireEvent(Changed);
        }

        public IList<InBoxItem> Items
        {
            get { return new ReadOnlyCollection<InBoxItem>(_items); }
        }

        public InBoxItem Add(string name)
        {
            var item = new InBoxItem(name);
            _persistence.Save(item);
            _items.Add(item);
            FireEvent(Changed);

            return item;
        }

        public void Remove(InBoxItem item)
        {
            _items.Remove(item);
            _persistence.Delete(item);
            FireEvent(Changed);
        }

        private static void FireEvent(Action evnt)
        {
            if (evnt != null)
                evnt();
        }

        public void ClearItems()
        {
            _items.Clear();
            _persistence.DeleteAll();
            FireEvent(Changed);
        }
    }
}
