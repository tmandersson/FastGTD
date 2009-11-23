using System;
using FastGTD.DataTransfer;

namespace FastGTD
{
    public interface IInBoxView : IItemView<InBoxItem>, IGTDWindow
    {
        event Action ToActionButtonWasClicked;
        event Action AltAKeysWasPressed;
    }
}