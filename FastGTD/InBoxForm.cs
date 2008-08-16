using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace FastGTD
{
    public partial class InBoxForm : Form, IInboxForm
    {
        public InBoxForm()
        {
            InitializeComponent();

            KeyDown += KeyDownHandler;
            KeyPreview = true;
            Resize += delegate { SetFirstColumnFullWidth(); };
            Shown += delegate { SetFirstColumnFullWidth(); };
            buttonAdd.Click += delegate { AddInboxItemInTextBox(); };
            buttonDelete.Click += delegate { DeleteSelectedItems(); };
        }

        public IList<string> InBoxItems
        {
            get
            {
                IList<string> list = new List<string>();
                foreach (ListViewItem item in listViewInBoxItems.Items)
                {
                    list.Add(item.Text);
                }
                return list;
            }
        }

        public string TextBoxValue
        {
            get { return textBox.Text; }
            set { textBox.Text = value; }
        }

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

        public string SelectedItem
        {
            get
            {
                return listViewInBoxItems.SelectedItems[0].Text;
            }
            set
            {
                listViewInBoxItems.SelectedItems.Clear();
                foreach (ListViewItem item in listViewInBoxItems.Items)
                {
                    if (item.Text == value) item.Selected = true;
                }
            }
        }

        public void ClickControl(InboxFormButton button_id)
        {
            Button button;
            switch (button_id)
            {
                case InboxFormButton.Add:
                    button = buttonAdd;
                    break;
                case InboxFormButton.Delete:
                    button = buttonDelete;
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

        private void SetFirstColumnFullWidth()
        {
            listViewInBoxItems.Columns[0].Width = listViewInBoxItems.Width - 5;
        }

        private void KeyDownHandler(object sender, KeyEventArgs e)
        {
            bool key_handled = true;
            if (e.KeyData == Keys.Enter)
                AddInboxItemInTextBox();
            else if (e.KeyData == Keys.Delete)
                DeleteSelectedItems();
            else if (e.KeyData == Keys.Down)
                ChangeSelection(1);
            else if (e.KeyData == Keys.Up)
                ChangeSelection(-1);
            else
                key_handled = false;

            if (key_handled)
                e.SuppressKeyPress = true;
        }

        private void ChangeSelection(int step)
        {
            int count_items = listViewInBoxItems.Items.Count;
            int count_selected = listViewInBoxItems.SelectedItems.Count;

            if (count_items == 0)
                return;

            int next_index;
            if (count_selected == 0)
                next_index = 0;
            else
            {
                ListViewItem last_selected = listViewInBoxItems.SelectedItems[count_selected - 1];
                next_index = last_selected.Index + step;
            }

            if (next_index == -1)
                next_index = 0;

            if (next_index == count_items)
                next_index = count_items - 1;

            listViewInBoxItems.Focus();
            listViewInBoxItems.SelectedItems.Clear();
            listViewInBoxItems.Items[next_index].Selected = true;
        }

        public void AddInboxItemInTextBox()
        {
            string new_item = textBox.Text;
            AddInboxItem(new_item);
        }

        public void AddInboxItem(string new_item)
        {
            listViewInBoxItems.Items.Add(new_item);
            textBox.Text = string.Empty;
        }

        public void DeleteSelectedItems()
        {
            foreach(ListViewItem item in listViewInBoxItems.SelectedItems)
            {
                item.Remove();
            }
        }
    }
}
