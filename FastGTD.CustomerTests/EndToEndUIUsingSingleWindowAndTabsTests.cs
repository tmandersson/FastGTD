using System;
using NUnit.Framework;
using White.Core;
using White.Core.Configuration;
using White.Core.UIItems.TabItems;
using White.Core.UIItems.WindowItems;

namespace FastGTD.CustomerTests
{
    [TestFixture, Explicit]
    public class EndToEndUIUsingSingleWindowAndTabsTests
    {
        private Application _app;
        private TabPageWithListBoxHelper _inbox;
        private TabPageWithListBoxHelper _actions;
        private string _new_item;

        [SetUp]
        public void Setup()
        {
            ConfigureWhiteTimeouts();
            _new_item = Guid.NewGuid().ToString();
            _app = Application.Launch("FastGTD.exe");
            Window window = _app.GetWindow("FastGTD");
            Tab tab = window.Tabs[0];
            ITabPage inbox_page = tab.Pages.Find(x => x.Name == "InBox");
            _inbox = new TabPageWithListBoxHelper(inbox_page, window);
            ITabPage actions_page = tab.Pages.Find(x => x.Name == "Actions");
            _actions = new TabPageWithListBoxHelper(actions_page, window);
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
            _actions.DeleteAllItems();
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
        public void AddingInboxItemToInboxByClickingAddButton()
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
            _inbox.AssertListDoesNotHaveItem(_new_item);
        }

        [Test]
        public void AddingActionByPressingReturnKey()
        {
            _actions.InputNewItemInTextBox(_new_item);
            _actions.PressReturnKey();
            _actions.AssertListHasItem(_new_item);
        }

        [Test]
        public void AddingActionByClickingAddButton()
        {
            _actions.InputNewItemInTextBox(_new_item);
            _actions.ClickAddButton();
            _actions.AssertListHasItem(_new_item);    
        }

        [Test]
        public void DeleteActionByPressingDeleteKey()
        {
            AddActionItem(_new_item);
            _actions.PressDownArrowKey();
            _actions.PressDeleteKey();
            _actions.AssertListDoesNotHaveItem(_new_item);            
        }

        [Test]
        public void DeleteActionByClickingDeleteButton()
        {
            AddActionItem(_new_item);
            _actions.PressDownArrowKey();
            _actions.ClickDeleteButton();
            _actions.AssertListDoesNotHaveItem(_new_item);
        }

        private void AddInboxItem(string item)
        {
            _inbox.InputNewItemInTextBox(item);
            _inbox.PressReturnKey();
        }

        private void AddActionItem(string item)
        {
            _actions.InputNewItemInTextBox(item);
            _actions.PressReturnKey();
        }
    }
}