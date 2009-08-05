using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using FastGTD.DataTransfer;

namespace FastGTD.Domain
{
    public class ActionsListModel : IActionsListModel
    {
        private readonly IActionsListPersistence _persistence;
        private IList<ActionItem> _items = new List<ActionItem>();

        public ActionsListModel(IActionsListPersistence persistence)
        {
            _persistence = persistence;
        }

        public IList<ActionItem> Items
        {
            get { return new ReadOnlyCollection<ActionItem>(_items); }
        }

        public ActionItem Add(string name)
        {
            var action = new ActionItem(name);
            _items.Add(action);
            _persistence.Save(action);
            return action;
        }

        public void Load()
        {
            _items = _persistence.GetAll();
        }

        public void ClearItems()
        {
            _items.Clear();
            _persistence.DeleteAll();
        }
    }
}