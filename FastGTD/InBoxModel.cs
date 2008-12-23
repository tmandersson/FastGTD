using System.Collections.Generic;

namespace FastGTD
{
    public class InBoxModel
    {
        private readonly IList<string> _items = new List<string>();
        public event VoidDelegate Changed;

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
            string[] _items_to_delete = new string[_items.Count];
            _items.CopyTo(_items_to_delete, 0);
            foreach (string item in _items_to_delete)
            {
                _items.Remove(item);
            }
            FireEvent(Changed);
        }
    }

    public delegate void VoidDelegate();
}
