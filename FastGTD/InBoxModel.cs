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
        private readonly ActionsListModel _actions_list_model;
        public event VoidDelegate Changed;

        public InBoxModel(IInBoxPersistenceProvider persistence, ActionsListModel model)
        {
            _persistence = persistence;
            _actions_list_model = model;
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

        public ActionItem ConvertToAction(InBoxItem item)
        {
            _items.Remove(item);
            ActionItem action = _actions_list_model.Add(item.Name);
            return action;
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

        private static void FireEvent(VoidDelegate evnt)
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

    public delegate void VoidDelegate();
}
