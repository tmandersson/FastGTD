using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using FastGTD.DataTransfer;

namespace FastGTD.Domain
{
    public class ActionsListModel : IItemModel<ActionItem>
    {
        private IList<ActionItem> _items = new List<ActionItem>();
        private readonly IItemPersistence<ActionItem> _persistence;

        public event Action Changed;

        public ActionsListModel(IItemPersistence<ActionItem> persistence)
        {
            _persistence = persistence;
        }

        public void Load()
        {
            _items = _persistence.GetAll();
        }

        public IList<ActionItem> Items
        {
            get { return new ReadOnlyCollection<ActionItem>(_items); }
        }

        public ActionItem Add(string name)
        {
            var action = new ActionItem(name);
            _persistence.Save(action);
            _items.Add(action);
            FireEvent(Changed);
            return action;
        }

        public void Remove(ActionItem item)
        {
            _items.Remove(item);
            _persistence.Delete(item);
            FireEvent(Changed);
        }

        public void ClearItems()
        {
            _items.Clear();
            _persistence.DeleteAll();
        }

        private static void FireEvent(Action evnt)
        {
            if (evnt != null)
                evnt();
        }
    }
}