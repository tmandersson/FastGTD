using System.Collections;
using System.Collections.Generic;
using FastGTD.DataAccess;
using FastGTD.DataTransfer;

namespace FastGTD
{
    public interface IInBoxModel
    {
        void Load();
        InBoxItem CreateItem(string item);
        IList<InBoxItem> Items { get; }
        void RemoveItem(InBoxItem item);
    }

    public class InBoxModel : IInBoxModel
    {
        // TODO: InBoxModel contains strings and repo contains InBoxItems. Fix?
        // TODO: Also: When removing items with same name one is removed in gui but all in repo.
        // TODO: Maybe some other object (repon?) should do the persistance job? By listening to the Changed event or some more notification events.
        private readonly IInBoxItemRepository _repository;
        private readonly IList<InBoxItem> _items = new List<InBoxItem>();
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
                _items.Add(item);
            }
            FireEvent(Changed);
        }

        public IList<InBoxItem> Items
        {
            get { return _items; }
        }

        public InBoxItem CreateItem(string name)
        {
            var item = _repository.CreateNew(name);
            _items.Add(item);
            FireEvent(Changed);

            return item;
        }

        public void RemoveItem(InBoxItem item)
        {
            _items.Remove(item);
            _repository.DeleteByName(item.Name);
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
