using NUnit.Framework;

namespace FastGTD.Tests
{
    [TestFixture]
    public class InboxFormEventTests
    {
        private bool _event_was_raised;

        [Test]
        public void ToActionButton()
        {
            _event_was_raised = false;
            var view = new TestableInBoxForm();
            view.ToActionButtonWasClicked += CatchEvent;
            view.Show();

            view.ClickToActionButton();

            Assert.That(_event_was_raised);
        }

        private void CatchEvent()
        {
            _event_was_raised = true;
        }
    }
}