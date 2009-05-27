﻿using System;
using System.Collections.Generic;
using FastGTD.DataAccess;
using FastGTD.DataTransfer;

namespace FastGTD
{
    public class InBoxModel : IInBoxModel
    {
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

        public void ConvertToAction(InBoxItem item)
        {
            throw new NotImplementedException();
        }

        public IList<InBoxItem> Items
        {
            get { return _items; }
        }

        public InBoxItem Add(string name)
        {
            var item1 = new InBoxItem(name);
            var item = item1;
            _repository.Save(item);
            _items.Add(item);
            FireEvent(Changed);

            return item;
        }

        public void Remove(InBoxItem item)
        {
            _items.Remove(item);
            _repository.Delete(item);
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
