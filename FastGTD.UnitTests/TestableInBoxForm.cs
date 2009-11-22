using System.Collections.Generic;
using System.Windows.Forms;

namespace FastGTD.UnitTests
{
    public class TestableInBoxForm : InBoxForm
    {
        public Control FocusedControl
        {
            get
            {
                if (Focused) return this;
                
                foreach (Control c in Controls)
                {
                    if (c.Focused)
                    {
                        return c;
                    }
                }
                return null;
            }
        }

        public string ListHeaderText
        {
            get { return _item_list.ListView.Columns[0].Text; }
        }

        public void SelectItem(string item)
        {
            IList<string> items = new List<string> { item };
            SelectItems(items);
        }

        public void SelectItems(IList<string> items)
        {
            _item_list.ListView.SelectedItems.Clear();
            foreach (ListViewItem item in _item_list.ListView.Items)
            {
                if (items.Contains(item.Text))
                    item.Selected = true;
            }
        }

        public void ClickDeleteButton()
        {
            _item_list.DeleteButton.PerformClick();
        }

        public void PerformKeyDown(Keys keys)
        {
            OnKeyDown(new KeyEventArgs(keys));
        }
    }
}