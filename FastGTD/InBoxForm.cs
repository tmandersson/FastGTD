using System;
using System.Collections.Generic;
using System.Windows.Forms;
using FastGTD.DataTransfer;

namespace FastGTD
{
    public partial class InBoxForm : Form, IInBoxForm
    {
        private readonly InBoxModel _model;
        private readonly ListViewController _list_controller;

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

            _list_controller = new ListViewController(_listViewInBoxItems);
        }

        private void UpdateFromModel()
        {
            _listViewInBoxItems.Items.Clear();
            foreach (InBoxItem item in _model.Items)
            {
                _listViewInBoxItems.Items.Add(item.Name);
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

        public IList<string> SelectedItems
        {
            get
            {
                IList<string> list = new List<string>();
                foreach(ListViewItem item in _listViewInBoxItems.SelectedItems)
                {
                    list.Add(item.Text);
                }
                return list;
            }
        }

        public ListView.ListViewItemCollection ListViewItems
        {
            get { return _listViewInBoxItems.Items; }
        }

        public void SelectItem(string item)
        {
            IList<string> items = new List<string> { item };
            SelectItems(items);
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
                _list_controller.ChangeSelection(1);
            else if (e.KeyData == Keys.Up)
                _list_controller.ChangeSelection(-1);
            else
                key_handled = false;

            if (key_handled)
                e.SuppressKeyPress = true;
        }

        private void AddInboxItemInTextBox()
        {
            string new_item = _textBox.Text;
            _model.CreateItem(new_item);
            _textBox.Text = string.Empty;
        }

        private void DeleteSelectedItems()
        {
            IList<InBoxItem> items_to_remove = new List<InBoxItem>();
            foreach (ListViewItem item in _listViewInBoxItems.SelectedItems)
            {
                foreach (InBoxItem itm in _model.Items)
                {
                    if (itm.Name == item.Text)
                    {
                        items_to_remove.Add(itm);
                        continue;
                    }
                }
            }

            foreach (InBoxItem item in items_to_remove)
                _model.RemoveItem(item);
        }
    }
}
