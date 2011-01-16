using System;

namespace FastGTD
{
    public interface IInBoxView : IItemView
    {
        event Action ToActionButtonWasClicked;
        event Action AltAKeysWasPressed;
    }
}