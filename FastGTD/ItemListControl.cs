using System.Windows.Forms;

namespace FastGTD
{
    public partial class ItemListControl: UserControl
    {
        private ListView _list_view;
        private TextBox _text_box;
        private Button _add_button;
        private Button _to_action_button;
        private Button _delete_button;

        public ItemListControl()
        {
            InitializeComponent();
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
    }
}
