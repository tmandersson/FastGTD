using System.Windows.Forms;

namespace FastGTD
{
    public partial class InBoxForm : Form
    {
        public InBoxForm()
        {
            InitializeComponent();

            textBox.KeyDown += KeyDownHandler;
        }

        private void KeyDownHandler(object sender, KeyEventArgs e)
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

        public void AddInboxItem()
        {
            string new_item = textBox.Text;
            listViewInBoxItems.Items.Add(new_item);
            textBox.Text = string.Empty;
        }

        public void PerformKeyDown(Keys key)
        {
            KeyDownHandler(this, new KeyEventArgs(key));
        }
    }
}
