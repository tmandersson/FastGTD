using System;
using System.Collections.Generic;
using FastGTD.DataTransfer;

namespace FastGTD
{
    public interface IInBoxView
    {
        void SetFocusOnTextBox();

        bool KeyPreview { set; }
        string TextBoxText { get; set; }
        IEnumerable<InBoxItem> SelectedItems { get; }
        event EventHandler Resize;
        void SetFirstColumnFullWidth();

        event VoidDelegate AddButtonWasClicked;
        event VoidDelegate DeleteButtonWasClicked;
        event VoidDelegate EnterKeyWasPressed;
        event VoidDelegate DeleteKeyWasPressed;
        event VoidDelegate DownKeyWasPressed;
        event VoidDelegate UpKeyWasPressed;

        void Show();
        void StartMessageLoop();
        void Close();

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