﻿using NUnit.Framework;
using White.Core.UIItems;
using White.Core.UIItems.Finders;
using White.Core.UIItems.WindowItems;
using White.Core.WindowsAPI;

namespace FastGTD.CustomerTests
{
    public class WindowWithListBoxHelper
    {
        private readonly Window _window;

        public WindowWithListBoxHelper(Window window)
        {
            _window = window;
        }

        private void GiveFocus()
        {
            _window.Focus();
        }

        public void ClickAddButton()
        {
            _window.Get<Button>(SearchCriteria.ByText("Add")).Click();
        }

        public void ClickDeleteButton()
        {
            _window.Get<Button>(SearchCriteria.ByText("Delete")).Click();
        }

        public void ClickToActionButton()
        {
            _window.Get<Button>(SearchCriteria.ByText("To Action")).Click();
        }

        public void PressReturnKey()
        {
            _window.KeyIn(KeyboardInput.SpecialKeys.RETURN);
        }

        public void PressDeleteKey()
        {
            _window.KeyIn(KeyboardInput.SpecialKeys.DELETE);
        }

        public void PressDownArrowKey()
        {
            _window.KeyIn(KeyboardInput.SpecialKeys.DOWN);
        }

        public void PressAltAKeys()
        {
            _window.Keyboard.HoldKey(KeyboardInput.SpecialKeys.LEFT_ALT);
            _window.Keyboard.Enter("a");
            _window.Keyboard.LeaveKey(KeyboardInput.SpecialKeys.LEFT_ALT);
        }

        public void InputNewItemInTextBox(string item)
        {
            GiveFocus();
            _window.Enter(item);
        }

        public void AssertListHasItem(string item)
        {
            ListView list_view = GetListView();
            Assert.That(list_view.Rows, Has.Some.Matches<ListViewRow>(i => i.Cells[0].Text == item));
        }

        public void AssertListDoesNotHaveItem(string item)
        {
            ListView list_view = GetListView();
            Assert.That(list_view.Rows, Has.No.Some.Matches<ListViewRow>(i => i.Cells[0].Text == item));
        }

        public void DeleteAllItems()
        {
            GiveFocus();
            ListView list_view = GetListView();
            while(list_view.Rows.Count > 0)
            {
                PressDownArrowKey();
                PressDeleteKey();
            }
        }

        private ListView GetListView()
        {
            return (ListView)_window.GetMultiple(SearchCriteria.ByControlType(typeof(ListView)))[0];
        }
    }
}