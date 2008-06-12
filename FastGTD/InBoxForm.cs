using System.Windows.Forms;

namespace FastGTD
{
    public partial class InBoxForm : Form
    {
        public InBoxForm()
        {
            InitializeComponent();
        }

        private void buttonAdd_Click(object sender, System.EventArgs e)
        {
            string new_item = textBoxNewItem.Text;
            listViewInBoxItems.Items.Add(new_item);
        }
    }
}
