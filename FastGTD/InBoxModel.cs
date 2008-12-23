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
        }
    }

    public delegate void VoidDelegate();
}
