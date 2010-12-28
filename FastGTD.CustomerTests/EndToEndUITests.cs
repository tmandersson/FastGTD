using System;
using System.Threading;
using NUnit.Framework;
using White.Core.UIItems;
using White.Core.UIItems.Finders;
using White.Core.UIItems.WindowItems;
using White.Core.WindowsAPI;
using Application = White.Core.Application;
using Debug = System.Diagnostics.Debug;

namespace FastGTD.CustomerTests
{
    [TestFixture]
    public class EndToEndUITests
    {
        [Test]
        public void AddingItemToInbox()
        {
            string new_item = Guid.NewGuid().ToString();
            
            var app = Application.Launch("FastGTD.exe");
            var inbox_window = app.GetWindow("InBox");
            AddItem(inbox_window, new_item);
            app.WaitWhileBusy();

            AssertListHasItem(inbox_window, new_item);

            app.Kill();
        }

        private static void AssertListHasItem(Window inbox_window, string new_item)
        {
            var list_view = (ListView) inbox_window.GetMultiple(SearchCriteria.ByControlType(typeof(ListView)))[0];
            Assert.That(list_view.Rows, Has.Some.Matches<ListViewRow>(i => i.Cells[0].Text == new_item));
        }

        private static void AddItem(Window inbox_window, string new_item)
        {
            inbox_window.Focus();
            inbox_window.Enter(new_item);
            inbox_window.KeyIn(KeyboardInput.SpecialKeys.RETURN);
        }
    }
}
