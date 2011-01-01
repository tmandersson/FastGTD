using System;
using NUnit.Framework;
using White.Core.UIItems;
using White.Core.UIItems.Finders;
using White.Core.UIItems.WindowItems;
using White.Core.WindowsAPI;
using Application = White.Core.Application;

namespace FastGTD.CustomerTests
{
    [TestFixture]
    public class EndToEndUITests
    {
        private Application _app;

        [TearDown]
        public void CleanUp()
        {
            _app.Kill();
        }

        [Test]
        public void AddingItemToInboxByPressingReturnKey()
        {
            string new_item = Guid.NewGuid().ToString();
            
            _app = Application.Launch("FastGTD.exe");
            var inbox_window = _app.GetWindow("InBox");
            InputNewItemInTextBox(inbox_window, new_item);
            PressReturnKey(inbox_window);
            _app.WaitWhileBusy();

            AssertListHasItem(inbox_window, new_item);
        }

        [Test]
        public void AddingItemToInboxByClickingButton()
        {
            string new_item = Guid.NewGuid().ToString();

            _app = Application.Launch("FastGTD.exe");
            var inbox_window = _app.GetWindow("InBox");
            InputNewItemInTextBox(inbox_window, new_item);
            ClickAddButton(inbox_window);
            _app.WaitWhileBusy();

            AssertListHasItem(inbox_window, new_item);
        }

        private static void ClickAddButton(Window inbox_window)
        {
            inbox_window.Get<Button>(SearchCriteria.ByText("Add")).Click();
        }

        private static void PressReturnKey(Window inbox_window)
        {
            inbox_window.KeyIn(KeyboardInput.SpecialKeys.RETURN);
        }

        private static void InputNewItemInTextBox(Window inbox_window, string new_item)
        {
            inbox_window.Focus();
            inbox_window.Enter(new_item);
        }

        private static void AssertListHasItem(Window inbox_window, string new_item)
        {
            var list_view = (ListView) inbox_window.GetMultiple(SearchCriteria.ByControlType(typeof(ListView)))[0];
            Assert.That(list_view.Rows, Has.Some.Matches<ListViewRow>(i => i.Cells[0].Text == new_item));
        }
    }
}
