using System;
using System.Windows.Forms;

namespace Lunar.Client.View.CommonControls
{
    public class CursorManager : IDisposable
    {
        private Control control;
        private Control owner;
        public static event UserActionStartedDelegate UserActionStarted = delegate { return false; };
        public static event UserActionCompletedDelegate UserActionCompleted = delegate { return false; };

        public delegate bool UserActionStartedDelegate(Control parentControl);

        public delegate bool UserActionCompletedDelegate(Control parentControl, bool userActionDoesntLeadToAnotherWindow);

        private bool userActionDoesntLeadToAnotherWindow;

        // Null check required after Open Window Navigation. Window1 opens Window2 and at the end it launches Window3.
        // But it between if Window 2 has been closed would lead to this scenario.
        private CursorManager(Control control, Control owner)
        {
            this.control = control;
            this.owner = owner;

            if (owner == null) return;

            this.owner.Cursor = Cursors.WaitCursor;
        }

        public virtual void Dispose()
        {
            if (owner != null) owner.Cursor = Cursors.Default;
            UserActionCompleted(control, userActionDoesntLeadToAnotherWindow);
        }

        public static CursorManager WaitCursor(Control control)
        {
            return WaitCursor(control, control.FindForm());
        }

        public static CursorManager WaitCursor(Control control, Control owner)
        {
            UserActionStarted(control);
            return new CursorManager(control, owner);
        }

        public virtual void UserActionDoesntLeadAnotherWindow()
        {
            userActionDoesntLeadToAnotherWindow = true;
        }
    }
}