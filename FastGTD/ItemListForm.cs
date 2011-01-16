using System;
using System.Collections.Generic;
using System.Windows.Forms;
using FastGTD.Domain;

namespace FastGTD
{
    public partial class ItemListForm : Form
    {
        private readonly ListViewSelectionChanger _selection_changer;

        public event Action AddButtonWasClicked;
        public event Action DeleteButtonWasClicked;
        public event Action ToActionButtonWasClicked;
        public event Action AltAKeysWasPressed;
        public event Action EnterKeyWasPressed;
        public event Action DeleteKeyWasPressed;
        public event Action DownKeyWasPressed;
        public event Action UpKeyWasPressed;

        protected ItemListForm()
        {
            InitializeComponent();
            _selection_changer = new ListViewSelectionChanger(_item_list.ListView);
            WireEvents();
        }

        public void AddItem(GTDItem item)
        {
            var list_item = new ListViewItem(item.Name) {Tag = item};
            _item_list.ListView.Items.Add(list_item);
        }

        public IListSelectionChanger List
        {
            get { return _selection_changer; }
        }

        public void ClearItems()
        {
            _item_list.ListView.Items.Clear();
        }

        public IEnumerable<GTDItem> SelectedItems
        {
            get
            {
                IList<GTDItem> result = new List<GTDItem>();
                foreach (ListViewItem item in _item_list.ListView.SelectedItems)
                {
                    result.Add((GTDItem)item.Tag);
                }
                return result;
            }
        }

        public string TextBoxText
        {
            get { return _item_list.TextBox.Text; }
            set { _item_list.TextBox.Text = value; }
        }

        public void SetFocusOnTextBox()
        {
            _item_list.TextBox.Focus();
        }

        public void StartMessageLoop()
        {
            Application.Run(this);
        }

        public void SelectItems(IEnumerable<GTDItem> items)
        {
            _item_list.ListView.SelectedItems.Clear();
            foreach (var item in items)
            {
                foreach (ListViewItem list_item in _item_list.ListView.Items)
                {
                    if (list_item.Tag.Equals(item))
                        list_item.Selected = true;
                }
            }
        }

        public void ClickAddButton()
        {
            _item_list.AddButton.PerformClick();
        }

        public void ClickToActionButton()
        {
            _item_list.ToActionButton.PerformClick();
        }

        private void WireEvents()
        {
            _item_list.AddButton.Click += delegate { RaiseEvent(AddButtonWasClicked); };
            _item_list.DeleteButton.Click += delegate { RaiseEvent(DeleteButtonWasClicked); };
            _item_list.ToActionButton.Click += delegate { RaiseEvent(ToActionButtonWasClicked); };
            KeyPreview = true;
            KeyDown += RouteKeyDownEvents;
            Shown += delegate { SetFirstColumnFullWidth(); };
            Resize += delegate { SetFirstColumnFullWidth(); };
        }

        private void SetFirstColumnFullWidth()
        {
            _item_list.ListView.Columns[0].Width = _item_list.ListView.Width - 5;
        }

        private void RouteKeyDownEvents(object sender, KeyEventArgs e)
        {
            bool key_handled = true;
            switch (e.KeyData)
            {
                case Keys.Enter:
                    RaiseEvent(EnterKeyWasPressed);
                    break;
                case Keys.Delete:
                    RaiseEvent(DeleteKeyWasPressed);
                    break;
                case Keys.Down:
                    RaiseEvent(DownKeyWasPressed);
                    break;
                case Keys.Up:
                    RaiseEvent(UpKeyWasPressed);
                    break;
                case Keys.Alt | Keys.A:
                    RaiseEvent(AltAKeysWasPressed);
                    break;
                default:
                    key_handled = false;
                    break;
            }

            if (key_handled)
                e.SuppressKeyPress = true;
        }

        private static void RaiseEvent(Action @delegate)
        {
            if (@delegate != null)
                @delegate.Invoke();
        }
    }
}
