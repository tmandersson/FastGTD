using System.Collections.Generic;
using FastGTD.DataTransfer;

namespace FastGTD
{
    public interface IInBoxView
    {
        void Show();
        void SetFocusOnTextBox();
        void StartMessageLoop();
        void Close();

        event VoidDelegate AddButtonWasClicked;
        event VoidDelegate DeleteButtonWasClicked;
        event VoidDelegate EnterKeyWasPressed;
        event VoidDelegate DeleteKeyWasPressed;
        event VoidDelegate DownKeyWasPressed;
        event VoidDelegate UpKeyWasPressed;

        string TextBoxText { get; set; }
        IEnumerable<InBoxItem> SelectedItems { get; }

        void ClearInBoxItems();
        void AddInBoxItem(InBoxItem item);
        IListSelectionChanger List { get; }
    }

    public interface IListSelectionChanger
    {
        void MoveDown();
        void MoveUp();
    }
}