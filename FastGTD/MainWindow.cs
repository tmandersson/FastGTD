using System;
using System.Windows.Forms;

namespace FastGTD
{
    public partial class MainWindow : Form, IGTDWindow, IMainWindowView, IPublishKeyEvents
    {
        public event Action AddButtonWasClicked;
        public event Action DeleteButtonWasClicked;
        public event Action EnterKeyWasPressed;
        public event Action DeleteKeyWasPressed;
        public event Action DownKeyWasPressed;
        public event Action UpKeyWasPressed;

        public event Action SelectedTabChanged;

        public MainWindow(ItemListControl inbox_view, ItemListControl actions_view)
        {
            InitializeComponent();
            _inbox_controls = inbox_view;
            _actions_controls = actions_view;
            _inbox_tab.Controls.Add(_inbox_controls);
            _actions_tab.Controls.Add(_actions_controls);
            ConfigureItemListControl(_inbox_controls);
            ConfigureItemListControl(_actions_controls);

            _tab_control.SelectedIndexChanged += delegate { RaiseEvent(SelectedTabChanged); };
            _inbox_controls.AddButtonWasClicked += () => RaiseEvent(AddButtonWasClicked);
            _inbox_controls.DeleteButtonWasClicked += () => RaiseEvent(DeleteButtonWasClicked);
            _actions_controls.AddButtonWasClicked += () => RaiseEvent(AddButtonWasClicked);

            KeyPreview = true;
            KeyDown += RouteKeyDownEvents;
        }

        private static void ConfigureItemListControl(ItemListControl control)
        {
            control.Dock = DockStyle.Fill;
            control.Location = new System.Drawing.Point(3, 3);
            control.Size = new System.Drawing.Size(507, 517);
            control.TabIndex = 0;
        }

        private void RouteKeyDownEvents(object sender, KeyEventArgs e)
        {
            bool key_handled = true;
            switch (e.KeyData)
            {
                case Keys.Enter:
                    RaiseEvent(EnterKeyWasPressed);
                    break;
                case Keys.Delete:
                    RaiseEvent(DeleteKeyWasPressed);
                    break;
                case Keys.Down:
                    RaiseEvent(DownKeyWasPressed);
                    break;
                case Keys.Up:
                    RaiseEvent(UpKeyWasPressed);
                    break;
                //case Keys.Alt | Keys.A:
                //    RaiseEvent(AltAKeysWasPressed);
                //    break;
                default:
                    key_handled = false;
                    break;
            }

            if (key_handled)
                e.SuppressKeyPress = true;
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

        public int SelectedTabIndex
        {
            get { return _tab_control.SelectedIndex; }
            set { }
        }

        public void PerformKeyDown(Keys keys)
        {
            OnKeyDown(new KeyEventArgs(keys));
        }

        private static void RaiseEvent(Action @delegate)
        {
            if (@delegate != null)
                @delegate.Invoke();
        }
    }
}
