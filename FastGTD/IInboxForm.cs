using System.Collections.Generic;
using System.Windows.Forms;

namespace FastGTD
{
    public interface IInBoxForm
    {
        string TextBoxValue { get; set; }
        Control FocusedControl { get; }
        string ListHeaderText { get; }

        void ClickControl(InBoxFormButton button);
        void PerformKeyDown(Keys key);

        IList<string> SelectedItems { get; }
        ListView.ListViewItemCollection ListViewItems { get; }
        void SelectItem(string item);
        void SelectItems(IList<string> items);

        void Show();
        void Close();
    }

    public enum InBoxFormButton
    {
        Add,
        Delete
    }
}
