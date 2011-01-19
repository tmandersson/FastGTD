using System;
using System.Windows.Forms;

namespace FastGTD
{
    public partial class MainWindow : Form, IGTDWindow, IMainWindowView
    {
        public MainWindow(ItemListControl inbox_view, ItemListControl actions_view)
        {
            InitializeComponent();
            _inbox_controls = inbox_view;
            _actions_controls = actions_view;
            _inbox_tab.Controls.Add(_inbox_controls);
            _actions_tab.Controls.Add(_actions_controls);
            ConfigureItemListControl(_inbox_controls);
            ConfigureItemListControl(_actions_controls);
            _tab_control.SelectedIndexChanged += delegate { SelectedTabChanged.Invoke(this, EventArgs.Empty); };
        }

        private static void ConfigureItemListControl(ItemListControl control)
        {
            control.Dock = DockStyle.Fill;
            control.Location = new System.Drawing.Point(3, 3);
            control.Size = new System.Drawing.Size(507, 517);
            control.TabIndex = 0;
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
