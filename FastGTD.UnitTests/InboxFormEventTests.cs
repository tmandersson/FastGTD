using System.Windows.Forms;
using NUnit.Framework;

namespace FastGTD.UnitTests
{
    [TestFixture]
    public class InboxFormEventTests : EventTestingFixture
    {
        private TestableInBoxForm _view;

        [SetUp]
        public void SetupTests()
        {
            _view = new TestableInBoxForm();
            _view.Show();
        }

        [Test]
        public void ToActionButtonEvent()
        {
            _view.ToActionButtonWasClicked += CatchEvent;
            _view.ClickToActionButton();

            Assert.That(EventWasRaised());
        }

        [Test]
        public void AltAEvent()
        {
            _view.AltAKeysWasPressed += CatchEvent;
            _view.PerformKeyDown(Keys.Alt | Keys.A);

            Assert.That(EventWasRaised());
        }
    }
}