using System.Collections.Generic;
using System.Windows.Forms;

namespace FastGTD
{
    public interface IInboxForm
    {
        string TextBoxValue { get; set; }
        Control FocusedControl { get; }
        string ListHeaderText { get; }

        void ClickControl(InboxFormButton button);
        void PerformKeyDown(Keys key);

        IList<string> InBoxItems { get; }

        IList<string> SelectedItems { get; }
        void SelectItem(string item);
        void SelectItems(IList<string> items);

        void Show();
    }

    public enum InboxFormButton
    {
        Add,
        Delete
    }
}
