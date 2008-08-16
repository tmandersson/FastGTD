using System.Collections.Generic;
using System.Windows.Forms;

namespace FastGTD
{
    public interface IInboxForm
    {
        IList<string> InBoxItems { get; }
        void AddInboxItem(string new_item);

        string TextBoxValue { get; set; }
        Control FocusedControl { get; }
        void ClickControl(InboxFormButton button);
        void PerformKeyDown(Keys enter);
        string SelectedItem { get; set; }
        void DeleteSelectedItem();
        void Show();
    }

    public enum InboxFormButton
    {
        Add,
        Delete
    }
}
