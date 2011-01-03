using System;
using NUnit.Framework;
using White.Core.Configuration;
using Application = White.Core.Application;

namespace FastGTD.CustomerTests
{
    [TestFixture]
    public class EndToEndUITests
    {
        private Application _app;
        private InBoxWindowTestHelper _window;
        private string _new_item;

        [SetUp]
        public void Setup()
        {
            _new_item = Guid.NewGuid().ToString();
            CoreAppXmlConfiguration.Instance.BusyTimeout = 10000;
            CoreAppXmlConfiguration.Instance.UIAutomationZeroWindowBugTimeout = 10000;
            _app = Application.Launch("FastGTD.exe");
            _window = new InBoxWindowTestHelper(_app.GetWindow("InBox"));
        }

        [TearDown]
        public void CleanUp()
        {
            _window.DeleteAllItems();
            _app.Kill();
        }

        [Test]
        public void AddingItemToInboxByPressingReturnKey()
        {
            _window.InputNewItemInTextBox(_new_item);
            _window.PressReturnKey();
            _window.AssertListHasItem(_new_item);
        }

        [Test]
        public void AddingItemToInboxByClickingButton()
        {
            _window.InputNewItemInTextBox(_new_item);
            _window.ClickAddButton();
            _window.AssertListHasItem(_new_item);
        }

        [Test]
        public void DeleteItemByPressingDeleteKey()
        {
            AddItem(_new_item);
            _window.PressDownArrowKey();
            _window.PressDeleteKey();
            _window.AssertListDoesNotHaveItem(_new_item);
        }

        [Test]
        public void DeleteItemByClickingDeleteButton()
        {
            AddItem(_new_item);
            _window.PressDownArrowKey();
            _window.ClickDeleteButton();

        }

        private void AddItem(string new_item)
        {
            _window.InputNewItemInTextBox(new_item);
            _window.PressReturnKey();
        }
    }
}
