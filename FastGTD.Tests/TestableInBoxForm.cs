using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace FastGTD.Tests
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

        public IList<string> SelectedItems
        {
            get
            {
                IList<string> list = new List<string>();
                foreach(ListViewItem item in ListViewInBoxItems.SelectedItems)
                {
                    list.Add(item.Text);
                }
                return list;
            }
        }

        public string ListHeaderText
        {
            get { return ListViewInBoxItems.Columns[0].Text; }
        }

        public void SelectItem(string item)
        {
            IList<string> items = new List<string> { item };
            SelectItems(items);
        }

        public void SelectItems(IList<string> items)
        {
            ListViewInBoxItems.SelectedItems.Clear();
            foreach (ListViewItem item in ListViewInBoxItems.Items)
            {
                if (items.Contains(item.Text))
                    item.Selected = true;
            }
        }

        public void ClickControl(InBoxFormButton button_id)
        {
            Button button;
            switch (button_id)
            {
                case InBoxFormButton.Add:
                    button = _buttonAdd;
                    break;
                case InBoxFormButton.Delete:
                    button = _buttonDelete;
                    break;
                default:
                    throw new InvalidOperationException("Unknown button.");
            }
            button.PerformClick();
        }

        public void PerformKeyDown(Keys key)
        {
            OnKeyDown(new KeyEventArgs(key));
        }
    }

    public enum InBoxFormButton
    {
        Add,
        Delete
    }
}