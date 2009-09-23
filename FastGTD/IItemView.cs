using System;
using System.Collections.Generic;
using FastGTD.Domain;

namespace FastGTD
{
    public interface IItemView<T> where T:GTDItem
    {
        void Show();
        void SetFocusOnTextBox();
        void StartMessageLoop();
        void Close();

        event Action AddButtonWasClicked;
        event Action DeleteButtonWasClicked;
        event Action EnterKeyWasPressed;
        event Action DeleteKeyWasPressed;
        event Action DownKeyWasPressed;
        event Action UpKeyWasPressed;
        
        string TextBoxText { get; set; }
        IEnumerable<T> SelectedItems { get; }
        IListSelectionChanger List { get; }
        void ClearItems();
        void AddItem(T item);
    }
}