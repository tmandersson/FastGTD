using System;
using NUnit.Framework;
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
            _app = Application.Launch("FastGTD.exe");
            _window = new InBoxWindowTestHelper(_app.GetWindow("InBox"));
        }

        [TearDown]
        public void CleanUp()
        {
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
    }
}
