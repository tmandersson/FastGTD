using System.Collections.Generic;
using System.Windows.Forms;

namespace FastGTD
{
    public interface IInboxForm
    {
        IList<string> InBoxItems { get; }

        string TextBoxValue { get; set; }
        Control FocusedControl { get; }
        string ListHeaderText { get; }

        void ClickControl(InboxFormButton button);
        void PerformKeyDown(Keys key);
        // TODO: Move selection to model?
        string FirstSelectedItem { get; set; }
        void SelectItems(IList<string> items);

        void Show();
    }

    public enum InboxFormButton
    {
        Add,
        Delete
    }
}
