using System;
using System.Collections.Generic;
using System.Windows.Forms;
using FastGTD.Domain;

namespace FastGTD
{
    public partial class ItemListControl: UserControl, IInBoxView
    {
        private readonly ListViewSelectionChanger _selection_changer;

        public event Action AddButtonWasClicked;
        public event Action DeleteButtonWasClicked;
        public event Action EnterKeyWasPressed;
        public event Action DeleteKeyWasPressed;
        public event Action DownKeyWasPressed;
        public event Action UpKeyWasPressed;
        public event Action ToActionButtonWasClicked;
        public event Action AltAKeysWasPressed;

        private ListView _list_view;
        private TextBox _text_box;
        private Button _add_button;
        private Button _to_action_button;
        private Button _delete_button;

        public ItemListControl()
        {
            InitializeComponent();
            _selection_changer = new ListViewSelectionChanger(ListView);
            WireEvents();
        }

        private void WireEvents()
        {
            AddButton.Click += delegate { RaiseEvent(AddButtonWasClicked); };
            DeleteButton.Click += delegate { RaiseEvent(DeleteButtonWasClicked); };
            ToActionButton.Click += delegate { RaiseEvent(ToActionButtonWasClicked); };
            KeyDown += RouteKeyDownEvents;
            Resize += delegate { SetFirstColumnFullWidth(); };
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

        private void SetFirstColumnFullWidth()
        {
            ListView.Columns[0].Width = ListView.Width - 5;
        }

        private static void RaiseEvent(Action @delegate)
        {
            if (@delegate != null)
                @delegate.Invoke();
        }

        public ListView ListView { get { return _list_view; } }
        public TextBox TextBox { get { return _text_box; } }
        public Button AddButton { get { return _add_button; } }
        public Button ToActionButton { get { return _to_action_button; } }
        public Button DeleteButton { get { return _delete_button; } }

        public void HideToActionButton()
        {
            _to_action_button.Visible = false;
        }

        public void SetFocusOnTextBox()
        {
            TextBox.Focus();
        }

        public string TextBoxText
        {
            get { return TextBox.Text; }
            set { TextBox.Text = value; }
        }

        public IEnumerable<GTDItem> SelectedItems
        {
            get
            {
                IList<GTDItem> result = new List<GTDItem>();
                foreach (ListViewItem item in ListView.SelectedItems)
                {
                    result.Add((GTDItem)item.Tag);
                }
                return result;
            }
        }

        public IListSelectionChanger List
        {
            get { return _selection_changer; }
        }

        public void ClearItems()
        {
            ListView.Items.Clear();
        }

        public void AddItem(GTDItem item)
        {
            var list_item = new ListViewItem(item.Name) { Tag = item };
            ListView.Items.Add(list_item);
        }
    }
}
