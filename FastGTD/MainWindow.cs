using System.Windows.Forms;

namespace FastGTD
{
    public partial class MainWindow : Form, IGTDWindow
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        public void StartMessageLoop()
        {
            Application.Run(this);
        }

        public ItemListControl InBoxControls
        {
            get { return _inbox_controls; }
        }

        public ItemListControl ActionControls
        {
            get { return _actions_controls; }
        }
    }
}
