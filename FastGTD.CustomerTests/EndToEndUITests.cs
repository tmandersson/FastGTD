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
        private WindowWithListBoxHelper _inbox;
        private WindowWithListBoxHelper _actions;
        private string _new_item;

        [SetUp]
        public void Setup()
        {
            ConfigureWhiteTimeouts();
            _new_item = Guid.NewGuid().ToString();
            _app = Application.Launch("FastGTD.exe");
            _inbox = new WindowWithListBoxHelper(_app.GetWindow("InBox"));
            _actions = new WindowWithListBoxHelper(_app.GetWindow("Actions"));
        }

        private static void ConfigureWhiteTimeouts()
        {
            CoreAppXmlConfiguration.Instance.BusyTimeout = 10000;
            CoreAppXmlConfiguration.Instance.UIAutomationZeroWindowBugTimeout = 10000;
        }

        [TearDown]
        public void CleanUp()
        {
            _inbox.DeleteAllItems();
            _app.Kill();
        }

        [Test]
        public void AddingInboxItemToInboxByPressingReturnKey()
        {
            _inbox.InputNewItemInTextBox(_new_item);
            _inbox.PressReturnKey();
            _inbox.AssertListHasItem(_new_item);
        }

        [Test]
        public void AddingInboxItemToInboxByClickingButton()
        {
            _inbox.InputNewItemInTextBox(_new_item);
            _inbox.ClickAddButton();
            _inbox.AssertListHasItem(_new_item);
        }

        [Test]
        public void DeleteInBoxItemByPressingDeleteKey()
        {
            AddInboxItem(_new_item);
            _inbox.PressDownArrowKey();
            _inbox.PressDeleteKey();
            _inbox.AssertListDoesNotHaveItem(_new_item);
        }

        [Test]
        public void DeleteInBoxItemByClickingDeleteButton()
        {
            AddInboxItem(_new_item);
            _inbox.PressDownArrowKey();
            _inbox.ClickDeleteButton();
        }

        [Test]
        public void AddingActionByPressingReturnKey()
        {
            _actions.InputNewItemInTextBox(_new_item);
            _actions.PressReturnKey();
            _actions.AssertListHasItem(_new_item);            
        }

        private void AddInboxItem(string new_item)
        {
            _inbox.InputNewItemInTextBox(new_item);
            _inbox.PressReturnKey();
        }
    }
}
