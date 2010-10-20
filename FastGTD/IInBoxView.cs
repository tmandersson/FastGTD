using System;
using FastGTD.DataTransfer;

namespace FastGTD
{
    public interface IInBoxView : IItemView<InBoxItem>
    {
        event Action ToActionButtonWasClicked;
        event Action AltAKeysWasPressed;
    }
}