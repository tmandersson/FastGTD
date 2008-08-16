using System.Collections.Generic;
using System.Windows.Forms;

namespace FastGTD
{
    public interface IInboxForm
    {
        IList<string> InBoxItems { get; }
        // TODO: A type of duplication: Multiple ways to add item and AddInBoxItem() don't correspond with real UI usage. Belongs in model.
        void DeleteSelectedItems();

        string TextBoxValue { get; set; }
        Control FocusedControl { get; }
        string ListHeaderText { get; }

        void ClickControl(InboxFormButton button);
        void PerformKeyDown(Keys key);
        // TODO: Real UI can select multiple items and delete method deletes all selected items.
        // TODO: Selecting multiple objects is not tested.
        string SelectedItem { get; set; }
        void Show();
    }

    public enum InboxFormButton
    {
        Add,
        Delete
    }
}
