using System;
using System.Collections.Generic;
using System.Windows.Forms;
using FastGTD.DataTransfer;

namespace FastGTD
{
    public partial class InBoxForm : Form, IInBoxView, ITestableInBoxView
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

        public InBoxForm()
        {
            InitializeComponent();
            _selection_changer = new ListViewSelectionChanger(_list_view);
            WireEvents();
        }

        public void AddInBoxItem(InBoxItem item)
        {
            var list_item = new ListViewItem(item.Name) {Tag = item};
            _list_view.Items.Add(list_item);
        }

        public IListSelectionChanger List
        {
            get { return _selection_changer; }
        }

        public void ClearInBoxItems()
        {
            _list_view.Items.Clear();
        }

        public IEnumerable<InBoxItem> SelectedItems
        {
            get
            {
                IList<InBoxItem> result = new List<InBoxItem>();
                foreach (ListViewItem item in _list_view.SelectedItems)
                {
                    result.Add((InBoxItem) item.Tag);
                }
                return result;
            }
        }

        public string TextBoxText
        {
            get { return _text_box.Text; }
            set { _text_box.Text = value; }
        }

        public void SetFocusOnTextBox()
        {
            _text_box.Focus();
        }

        public void StartMessageLoop()
        {
            Application.Run(this);
        }

        public void SelectItems(IEnumerable<InBoxItem> items)
        {
            _list_view.SelectedItems.Clear();
            foreach (var item in items)
            {
                foreach (ListViewItem list_item in _list_view.Items)
                {
                    if (list_item.Tag.Equals(item))
                        list_item.Selected = true;
                }
            }
        }

        public void ClickAddButton()
        {
            _add_button.PerformClick();
        }

        public void ClickToActionButton()
        {
            _to_action_button.PerformClick();
        }

        private void WireEvents()
        {
            _add_button.Click += delegate { RaiseEvent(AddButtonWasClicked); };
            _delete_button.Click += delegate { RaiseEvent(DeleteButtonWasClicked); };
            _to_action_button.Click += delegate { RaiseEvent(ToActionButtonWasClicked); };
            KeyPreview = true;
            KeyDown += RouteKeyDownEvents;
            Shown += delegate { SetFirstColumnFullWidth(); };
            Resize += delegate { SetFirstColumnFullWidth(); };
        }

        private void SetFirstColumnFullWidth()
        {
            _list_view.Columns[0].Width = _list_view.Width - 5;
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
