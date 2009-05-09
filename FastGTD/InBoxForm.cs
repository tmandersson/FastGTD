using System.Collections.Generic;
using System.Windows.Forms;
using FastGTD.DataTransfer;

namespace FastGTD
{
    public partial class InBoxForm : Form, IInBoxView
    {
        private readonly ListViewSelectionChanger _selection_changer;

        public InBoxForm()
        {
            InitializeComponent();

            _selection_changer = new ListViewSelectionChanger(ListViewInBoxItems);
            _buttonAdd.Click += delegate { RaiseEvent(AddButtonWasClicked); };
            _buttonDelete.Click += delegate { RaiseEvent(DeleteButtonWasClicked); };
            KeyDown += RouteKeyDownEvents;
        }

        protected ListView ListViewInBoxItems
        {
            get { return _listViewInBoxItems; }
        }

        public void AddInBoxItem(InBoxItem item)
        {
            var list_item = new ListViewItem(item.Name) {Tag = item};
            ListViewInBoxItems.Items.Add(list_item);
        }

        public IListSelectionChanger List
        {
            get { return _selection_changer; }
        }

        public void ClearInBoxItems()
        {
            ListViewInBoxItems.Items.Clear();
        }

        public IEnumerable<InBoxItem> SelectedItems
        {
            get
            {
                IList<InBoxItem> result = new List<InBoxItem>();
                foreach (ListViewItem item in ListViewInBoxItems.SelectedItems)
                {
                    result.Add((InBoxItem) item.Tag);
                }
                return result;
            }
        }

        public void SetFirstColumnFullWidth()
        {
            ListViewInBoxItems.Columns[0].Width = ListViewInBoxItems.Width - 5;
        }

        public event VoidDelegate AddButtonWasClicked;
        public event VoidDelegate DeleteButtonWasClicked;
        public event VoidDelegate EnterKeyWasPressed;
        public event VoidDelegate DeleteKeyWasPressed;
        public event VoidDelegate DownKeyWasPressed;
        public event VoidDelegate UpKeyWasPressed;

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
                default:
                    key_handled = false;
                    break;
            }

            if (key_handled)
                e.SuppressKeyPress = true;
        }

        private static void RaiseEvent(VoidDelegate @delegate)
        {
            if (@delegate != null)
                @delegate.Invoke();
        }
    }
}
