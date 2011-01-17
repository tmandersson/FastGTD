using System.Windows.Forms;

namespace FastGTD
{
    public partial class MainWindow : Form, IGTDWindow
    {
        public MainWindow()
        {
            InitializeComponent();
            Shown += delegate { SetFocusForActivatedView(); };
            GotFocus += delegate { SetFocusForActivatedView(); };
            _tab_control.SelectedIndexChanged += delegate { SetFocusForActivatedView(); };
        }

        private void SetFocusForActivatedView()
        {
            if (_tab_control.SelectedTab.Contains(_inbox_controls))
                _inbox_controls.SetFocusOnTextBox();
            if (_tab_control.SelectedTab.Contains(_actions_controls))
                _actions_controls.SetFocusOnTextBox();
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
    }
}
