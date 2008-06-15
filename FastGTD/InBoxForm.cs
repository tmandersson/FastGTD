using System.Windows.Forms;

namespace FastGTD
{
    public partial class InBoxForm : Form
    {
        public InBoxForm()
        {
            InitializeComponent();

            textBoxNewItem.KeyDown += KeyDownHandler;
        }

        public void KeyDownHandler(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                AddInboxItem();
                e.SuppressKeyPress = true;
            }
        }

        private void buttonAdd_Click(object sender, System.EventArgs e)
        {
            AddInboxItem();
        }

        private void AddInboxItem()
        {
            string new_item = textBoxNewItem.Text;
            listViewInBoxItems.Items.Add(new_item);
        }
    }
}
