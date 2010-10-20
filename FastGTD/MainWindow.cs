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
    }
}
