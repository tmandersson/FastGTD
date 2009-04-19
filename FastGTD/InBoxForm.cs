using System;
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
        }

        public ListView ListViewInBoxItems
        {
            get { return _listViewInBoxItems; }
        }

        public void AddInBoxItem(InBoxItem item)
        {
            var list_item = new ListViewItem(item.Name) {Tag = item};
            ListViewInBoxItems.Items.Add(list_item);
        }

        public void MoveListSelectionDown()
        {
            _selection_changer.ChangeSelection(1);
        }

        public void MoveListSelectionUp()
        {
            _selection_changer.ChangeSelection(-1);
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
