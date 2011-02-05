using System;

namespace FastGTD
{
    public class MainWindowController
    {
        private readonly IMainWindowView _main_view;
        private readonly IInBoxView _inbox_view;
        private readonly IItemView _action_view;

        public MainWindowController(IMainWindowView main_view, IInBoxView inbox_view, IItemView action_view)
        {
            _main_view = main_view;
            _inbox_view = inbox_view;
            _action_view = action_view;
            _main_view.Shown += delegate { SetFocusOnTextBoxInSelectedTab(); };
            _main_view.GotFocus += delegate { SetFocusOnTextBoxInSelectedTab(); };
            _main_view.SelectedTabChanged += SetFocusOnTextBoxInSelectedTab;
        }

        private void SetFocusOnTextBoxInSelectedTab()
        {
            if (_main_view.SelectedTabIndex == 1)
                _action_view.SetFocusOnTextBox();
            _inbox_view.SetFocusOnTextBox();
        }
    }

    public interface IMainWindowView
    {
        event EventHandler Shown;
        event EventHandler GotFocus;
        event Action SelectedTabChanged;

        int SelectedTabIndex { get; set; }
    }
}
