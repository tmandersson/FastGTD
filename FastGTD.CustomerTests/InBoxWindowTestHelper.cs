using NUnit.Framework;
using White.Core.UIItems;
using White.Core.UIItems.Finders;
using White.Core.UIItems.WindowItems;
using White.Core.WindowsAPI;

namespace FastGTD.CustomerTests
{
    public class InBoxWindowTestHelper
    {
        private readonly Window _inbox_window;

        public InBoxWindowTestHelper(Window inbox_window)
        {
            _inbox_window = inbox_window;
        }

        public void ClickAddButton()
        {
            _inbox_window.Get<Button>(SearchCriteria.ByText("Add")).Click();
        }

        public void PressReturnKey()
        {
            _inbox_window.KeyIn(KeyboardInput.SpecialKeys.RETURN);
        }

        public void InputNewItemInTextBox(string new_item)
        {
            _inbox_window.Focus();
            _inbox_window.Enter(new_item);
        }

        public void AssertListHasItem(string new_item)
        {
            var list_view = (ListView)_inbox_window.GetMultiple(SearchCriteria.ByControlType(typeof(ListView)))[0];
            Assert.That(list_view.Rows, Has.Some.Matches<ListViewRow>(i => i.Cells[0].Text == new_item));
        }
    }
}