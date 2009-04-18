using System;
using System.Collections.Generic;
using System.Windows.Forms;
using FastGTD.DataTransfer;

namespace FastGTD
{
    public partial class InBoxForm : Form, IInBoxForm, IInBoxView
    {
        private readonly ListViewController _list_controller;

        public InBoxForm()
        {
            InitializeComponent();

            _list_controller = new ListViewController(ListViewInBoxItems);
        }

        public ListView ListViewInBoxItems
        {
            get { return _listViewInBoxItems; }
        }

        public void AddInBoxItem(InBoxItem item)
        {
            ListViewInBoxItems.Items.Add(item.Name);
        }

        public void ClearInBoxItems()
        {
            ListViewInBoxItems.Items.Clear();
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
            get { return ListViewInBoxItems.Columns[0].Text; }
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

        public ListView.ListViewItemCollection ListViewItems
        {
            get { return ListViewInBoxItems.Items; }
        }

        public ListViewController ListController
        {
            get { return _list_controller; }
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

        public void SetFirstColumnFullWidth()
        {
            ListViewInBoxItems.Columns[0].Width = ListViewInBoxItems.Width - 5;
        }

        public event EventHandler AddButtonClick;
        public event EventHandler DeleteButtonClick;

        public string TextBoxText
        {
            get { return _textBox.Text; }
            set { _textBox.Text = value; }
        }

        public void SetFocusOnTextBox()
        {
            _textBox.Focus();
        }

        public void StartMessageLoop()
        {
            Application.Run(this);
        }

        private void _buttonAdd_Click(object sender, EventArgs e)
        {
            if (AddButtonClick != null)
                AddButtonClick.Invoke(sender, e);
        }

        private void _buttonDelete_Click(object sender, EventArgs e)
        {
            if (DeleteButtonClick != null)
                DeleteButtonClick.Invoke(sender, e);
        }
    }
}
