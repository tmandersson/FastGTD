using System.Collections.Generic;
using FastGTD.DataTransfer;

namespace FastGTD
{
    public interface ITestableInBoxView
    {
        string TextBoxText { set; }
        void SelectItems(IEnumerable<InBoxItem> items);
        void ClickAddButton();
        void ClickToActionButton();
    }
}