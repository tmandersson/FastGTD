using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace FastGTD
{
    public partial class InBoxForm : Form, IInboxForm
    {
        private readonly InBoxModel _model;

        public InBoxForm(InBoxModel model)
        {
            InitializeComponent();

            _model = model;

            KeyDown += KeyDownHandler;
            KeyPreview = true;
            Resize += delegate { SetFirstColumnFullWidth(); };
            Shown += delegate { SetFirstColumnFullWidth(); };
            _buttonAdd.Click += delegate { AddInboxItemInTextBox(); };
            _buttonDelete.Click += delegate { DeleteSelectedItems(); };
            _model.Changed += UpdateFromModel;
        }

        private void UpdateFromModel()
        {
            _listViewInBoxItems.Items.Clear();
            foreach (string item in _model.Items)
            {
                _listViewInBoxItems.Items.Add(item);
            }
        }

        public IList<string> InBoxItems
        {
            get
            {
                return _model.Items;
            }
        }

        public string TextBoxValue
        {
            get { return _textBox.Text; }
            set { _textBox.Text = value; }
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

        public string ListHeaderText
        {
            get { return _listViewInBoxItems.Columns[0].Text; }
        }

        // TODO: Temporary hack.
        public string FirstSelectedItem
        {
            get
            {
                return _listViewInBoxItems.SelectedItems[0].Text;
            }
            set
            {
                IList<string> items = new List<string> {value};
                SelectItems(items);
            }
        }

        public void SelectItems(IList<string> items)
        {
            _listViewInBoxItems.SelectedItems.Clear();
            foreach (ListViewItem item in _listViewInBoxItems.Items)
            {
                if (items.Contains(item.Text))
                    item.Selected = true;
            }
        }

        public void ClickControl(InboxFormButton button_id)
        {
            Button button;
            switch (button_id)
            {
                case InboxFormButton.Add:
                    button = _buttonAdd;
                    break;
                case InboxFormButton.Delete:
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

        private void SetFirstColumnFullWidth()
        {
            _listViewInBoxItems.Columns[0].Width = _listViewInBoxItems.Width - 5;
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
            int count_items = _listViewInBoxItems.Items.Count;
            int count_selected = _listViewInBoxItems.SelectedItems.Count;

            if (count_items == 0)
                return;

            int next_index;
            if (count_selected == 0)
                next_index = 0;
            else
            {
                ListViewItem last_selected = _listViewInBoxItems.SelectedItems[count_selected - 1];
                next_index = last_selected.Index + step;
            }

            if (next_index == -1)
                next_index = 0;

            if (next_index == count_items)
                next_index = count_items - 1;

            _listViewInBoxItems.Focus();
            _listViewInBoxItems.SelectedItems.Clear();
            _listViewInBoxItems.Items[next_index].Selected = true;
        }

        private void AddInboxItemInTextBox()
        {
            string new_item = _textBox.Text;
            _model.AddItem(new_item);
            _textBox.Text = string.Empty;
        }

        private void DeleteSelectedItems()
        {
            foreach(ListViewItem item in _listViewInBoxItems.SelectedItems)
            {
                _model.RemoveItem(item.Text);
            }
        }
    }
}
