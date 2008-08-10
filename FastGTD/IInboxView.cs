using System.Collections.Generic;

namespace FastGTD
{
    public interface IInBoxView
    {
        InBoxForm Form { get; }
        bool InBoxListFullRowSelect { get; set; }

        IList<string> InBoxList { get; set; }

        void Show();

        void SetTextBoxFocus();

        event AddItemEvent AddItemAction;
    }

    public delegate void AddItemEvent(string new_in_item);
}