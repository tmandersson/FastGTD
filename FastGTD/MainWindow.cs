using System;
using System.Windows.Forms;

namespace FastGTD
{
    public partial class MainWindow : Form, IGTDWindow, IMainWindowView
    {
        public MainWindow()
        {
            InitializeComponent();
            _tab_control.SelectedIndexChanged += delegate { SelectedTabChanged.Invoke(this, EventArgs.Empty); };
        }

        public void StartMessageLoop()
        {
            Application.Run(this);
        }

        public IInBoxView InBoxControls
        {
            get { return _inbox_controls; }
        }

        public IItemView ActionControls
        {
            get { return _actions_controls; }
        }

        public event EventHandler SelectedTabChanged;

        public int SelectedTabIndex
        {
            get { return _tab_control.SelectedIndex; }
            set { }
        }
    }
}
