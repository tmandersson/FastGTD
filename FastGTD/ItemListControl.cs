using System.Windows.Forms;

namespace FastGTD
{
    public partial class ItemListControl : UserControl
    {
        public ItemListControl()
        {
            InitializeComponent();
        }

        public ListView ListView { get; private set; }
        public TextBox TextBox { get; private set; }
        public Button AddButton { get; private set; }
        public Button ToActionButton { get; private set; }
        public Button DeleteButton { get; private set; }

        public void HideToActionButton()
        {
            ToActionButton.Visible = false;
        }
    }
}
