using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;

namespace FastGTD.Tests
{
    [TestFixture]
    public class InBoxModelTests
    {
        private bool ContentUpdatedEventWasHandled;

        [Test]
        public void EmptyModel()
        {
            IInBoxModel model = new InBoxModel();
            Assert.That(model.InboxItems.Count, Is.EqualTo(0));
        }

        [Test]
        public void AddingInBoxItem()
        {
            IInBoxModel model = new InBoxModel();

            model.AddInboxItem("foo");
            Assert.That(model.InboxItems.Count, Is.EqualTo(1));
            Assert.That(model.InboxItems[0], Is.EqualTo("foo"));

            model.AddInboxItem("bar");
            Assert.That(model.InboxItems.Count, Is.EqualTo(2));
            Assert.That(model.InboxItems[1], Is.EqualTo("bar"));
        }

        [Test]
        public void FireContentUpdatedEvent()
        {
            IInBoxModel model = new InBoxModel();
            model.ContentUpdated += HandleContentUpdatedEvent;
            model.AddInboxItem("foo");
            Assert.That(ContentUpdatedEventWasHandled, Is.True);
        }

        private void HandleContentUpdatedEvent()
        {
            ContentUpdatedEventWasHandled = true;
        }
    }
}
