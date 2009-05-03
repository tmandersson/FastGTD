using System;
using System.Collections.Generic;
using System.Windows.Forms;
using FastGTD.DataTransfer;

namespace FastGTD
{
    public interface IInBoxView
    {
        void SetFocusOnTextBox();

        bool KeyPreview { set; }
        string TextBoxText { get; set; }
        ListView ListViewInBoxItems { get; }
        IEnumerable<InBoxItem> SelectedItems { get; }
        event EventHandler Resize;
        void SetFirstColumnFullWidth();

        event EventHandler AddButtonClick;
        event EventHandler DeleteButtonClick;
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