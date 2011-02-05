using System;

namespace FastGTD
{
    public interface IPublishKeyEvents
    {
        event Action AddButtonWasClicked;
        event Action DeleteButtonWasClicked;
        event Action EnterKeyWasPressed;
        event Action DeleteKeyWasPressed;
        event Action DownKeyWasPressed;
        event Action UpKeyWasPressed;       
    }
}