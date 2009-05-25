using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;

namespace FastGTD.Tests
{
    [TestFixture]
    public class InBoxModelTests
    {
        [Test]
        public void DeletingItem()
        {
            var model = new InBoxModel(new FakeInBoxItemRepository());
            Assert.That(model.Items, Has.Count(0));

            model.Add("foo");
            Assert.That(model.Items, Has.Count(1));
            var item = model.Add("bar");
            Assert.That(model.Items, Has.Count(2));

            model.Remove(item);
            Assert.That(model.Items, Has.Count(1));
            Assert.That(model.Items[0].Name, Is.EqualTo("foo"));
        }

        [Test]
        public void ClearItems()
        {
            var model = new InBoxModel(new FakeInBoxItemRepository());

            Assert.That(model.Items, Has.Count(0));

            model.Add("foo");
            Assert.That(model.Items, Has.Count(1));
            model.Add("bar");
            Assert.That(model.Items, Has.Count(2));

            model.ClearItems();
            Assert.That(model.Items, Has.Count(0));
        }
    }
}