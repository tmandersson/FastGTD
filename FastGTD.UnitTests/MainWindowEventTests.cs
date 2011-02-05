using System.Windows.Forms;
using NUnit.Framework;

namespace FastGTD.UnitTests
{
    [TestFixture]
    public class MainWindowEventTests : EventTestingFixture
    {
        private ItemListControl _inbox;
        private MainWindow _window;

        [SetUp]
        public void SetupFixture()
        {
            _inbox = new ItemListControl();
            _window = new MainWindow(_inbox, new ItemListControl());
            _window.Show();            
        }

        [Test]
        public void InboxAddButton()
        {
            _window.AddButtonWasClicked += CatchEvent;
            _inbox.AddButton.PerformClick();
            Assert.That(EventWasRaised());
        }

        [Test]
        public void EnterKey()
        {
            _window.EnterKeyWasPressed += CatchEvent;
            _window.PerformKeyDown(Keys.Enter);
            Assert.That(EventWasRaised());
        }

        [Test]
        public void DeleteKey()
        {
            _window.DeleteKeyWasPressed += CatchEvent;
            _window.PerformKeyDown(Keys.Delete);
            Assert.That(EventWasRaised());
        }

        [Test]
        public void DownKey()
        {
            _window.DownKeyWasPressed += CatchEvent;
            _window.PerformKeyDown(Keys.Down);
            Assert.That(EventWasRaised());
        }
    }
}