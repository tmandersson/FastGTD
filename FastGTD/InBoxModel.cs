using System.Collections.Generic;
using FastGTD.DataAccess;
using FastGTD.DataTransfer;

namespace FastGTD
{
    public class InBoxModel
    {
        // TODO: InBoxModel contains strings and repo contains InBoxItems. Fix?
        // TODO: Also: When removing items with same name one is removed in gui but all in repo.
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
            _repository.CreateNew(item);
            FireEvent(Changed);
        }

        public void RemoveItem(string item)
        {
            _items.Remove(item);
            _repository.DeleteByName(item);
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
