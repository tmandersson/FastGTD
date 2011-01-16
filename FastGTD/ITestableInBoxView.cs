using System.Collections.Generic;
using FastGTD.Domain;

namespace FastGTD
{
    public interface ITestableInBoxView
    {
        string TextBoxText { set; }
        void SelectItems(IEnumerable<GTDItem> items);
        void ClickAddButton();
        void ClickToActionButton();
    }
}