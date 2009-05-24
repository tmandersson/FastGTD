using System.Windows.Forms;
using NUnit.Framework;

namespace FastGTD.Tests
{
    [TestFixture]
    public class InboxFormEventTests
    {
        private bool _event_was_raised;
        private TestableInBoxForm _view;

        [SetUp]
        public void SetupTests()
        {
            _event_was_raised = false;
            _view = new TestableInBoxForm();
            _view.Show();
        }

        [Test]
        public void ToActionButtonEvent()
        {
            _view.ToActionButtonWasClicked += CatchEvent;
            _view.ClickToActionButton();

            Assert.That(_event_was_raised);
        }

        [Test]
        public void AltAEvent()
        {
            _view.AltAKeysWasPressed += CatchEvent;
            _view.PerformKeyDown(Keys.Alt | Keys.A);

            Assert.That(_event_was_raised);
        }

        private void CatchEvent()
        {
            _event_was_raised = true;
        }
    }
}