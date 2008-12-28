using System.Collections.Generic;
using FastGTD.DataAccess;
using FastGTD.DataTransfer;

namespace FastGTD
{
    public class InBoxModel
    {
        private readonly IInBoxItemRepository _repository;
        private readonly IList<string> _items = new List<string>();
        public event VoidDelegate Changed;

        public InBoxModel(IInBoxItemRepository repository)
        {
            _repository = repository;
        }

        public void Load()
        {
            IList<InBoxItem> loaded_items = _repository.GetAll();
            foreach (InBoxItem item in loaded_items)
            {
                _items.Add(item.Name);
            }
            FireEvent(Changed);
        }

        public IList<string> Items
        {
            get { return _items; }
        }

        public void AddItem(string item)
        {
            _items.Add(item);
            FireEvent(Changed);
        }

        public void RemoveItem(string item)
        {
            _items.Remove(item);
            FireEvent(Changed);
        }

        private static void FireEvent(VoidDelegate evnt)
        {
            if (evnt != null)
                evnt();
        }

        public void ClearItems()
        {
            _items.Clear();
            _repository.DeleteAll();
            FireEvent(Changed);
        }
    }

    public delegate void VoidDelegate();
}
