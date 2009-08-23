using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace FastGTD.Domain
{
    public class ItemModel<T> : IItemModel<T> where T : GTDItem, new()
    {
        private IList<T> _items = new List<T>();
        private readonly IItemPersistence<T> _persistence;

        public event Action Changed;

        public ItemModel(IItemPersistence<T> persistence)
        {
            _persistence = persistence;
        }

        public void Load()
        {
            _items = _persistence.GetAll();
            FireEvent(Changed);
        }

        public IList<T> Items
        {
            get { return new ReadOnlyCollection<T>(_items); }
        }

        public T Add(string name)
        {
            var item = new T {Name = name};
            _persistence.Save(item);
            _items.Add(item);
            FireEvent(Changed);
            return item;
        }

        public void Remove(T item)
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