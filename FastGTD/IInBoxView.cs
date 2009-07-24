using System;
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

        event Action AddButtonWasClicked;
        event Action DeleteButtonWasClicked;
        event Action ToActionButtonWasClicked;
        event Action AltAKeysWasPressed;
        event Action EnterKeyWasPressed;
        event Action DeleteKeyWasPressed;
        event Action DownKeyWasPressed;
        event Action UpKeyWasPressed;

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