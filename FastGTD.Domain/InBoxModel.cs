using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using FastGTD.DataTransfer;

namespace FastGTD.Domain
{
    public class InBoxModel : IGTDItemModel<InBoxItem>
    {
        private IList<InBoxItem> _items = new List<InBoxItem>();
        private readonly IGTDItemPersistence<InBoxItem> _persistence;

        public event Action Changed;

        public InBoxModel(IGTDItemPersistence<InBoxItem> persistence)
        {
            _persistence = persistence;
        }

        public void Load()
        {
            _items = _persistence.GetAll();
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

        public void ClearItems()
        {
            _items.Clear();
            _persistence.DeleteAll();
            FireEvent(Changed);
        }

        private static void FireEvent(Action evnt)
        {
            if (evnt != null)
                evnt();
        }
    }
}