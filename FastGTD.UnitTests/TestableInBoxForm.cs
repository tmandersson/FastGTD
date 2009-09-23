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
            get { return _list_view.Columns[0].Text; }
        }

        public void SelectItem(string item)
        {
            IList<string> items = new List<string> { item };
            SelectItems(items);
        }

        public void SelectItems(IList<string> items)
        {
            _list_view.SelectedItems.Clear();
            foreach (ListViewItem item in _list_view.Items)
            {
                if (items.Contains(item.Text))
                    item.Selected = true;
            }
        }

        public void ClickDeleteButton()
        {
            _delete_button.PerformClick();
        }

        public void PerformKeyDown(Keys keys)
        {
            OnKeyDown(new KeyEventArgs(keys));
        }
    }
}