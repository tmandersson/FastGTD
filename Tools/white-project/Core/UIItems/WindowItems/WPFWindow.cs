using System.Diagnostics;
using System.Windows.Automation;
using White.Core.Factory;
using White.Core.Sessions;
using White.Core.UIItems.Finders;
using White.Core.UIItems.MenuItems;

namespace White.Core.UIItems.WindowItems
{
    [PlatformSpecificItem]
    internal class WPFWindow : Window
    {
        private readonly WindowFactory windowFactory;
        protected WPFWindow() {}

        public WPFWindow(AutomationElement automationElement, WindowFactory windowFactory, InitializeOption initializeOption,
                         WindowSession windowSession) : base(automationElement, initializeOption, windowSession)
        {
            this.windowFactory = windowFactory;
        }

        public override PopUpMenu Popup
        {
            get { return factory.WPFPopupMenu(this) ?? windowFactory.PopUp(this); }
        }

        public override Window ModalWindow(string title, InitializeOption option)
        {
            WindowFactory desktopWindowsFactory = WindowFactory.Desktop;
            return desktopWindowsFactory.FindModalWindow(title, Process.GetProcessById(automationElement.Current.ProcessId), option, automationElement,
                                                         windowSession.ModalWindowSession(option));
        }

        public override Window ModalWindow(SearchCriteria searchCriteria, InitializeOption option)
        {
            WindowFactory desktopWindowsFactory = WindowFactory.Desktop;
            return desktopWindowsFactory.FindModalWindow(searchCriteria, option, automationElement, windowSession.ModalWindowSession(option));
        }
    }
}