using System;
using System.Collections.Generic;
using FastGTD.DataAccess;
using FastGTD.DataTransfer;
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

        [Test, Explicit, Category("slow")]
        public void LoadingExistingItems()
        {
            string NAME = Guid.NewGuid().ToString();
            var repo = new InBoxItemRepository();
            IList<InBoxItem> existing = repo.GetAll();
            int EXPECTED_COUNT = existing.Count + 1;
            var item = repo.CreateNew(NAME);

            var model = new InBoxModel(new InBoxItemRepository());
            model.Load();

            Assert.That(model.Items, Has.Count(EXPECTED_COUNT));
            Assert.That(model.Items, Has.Member(item));
        }

        [Test, Explicit, Category("slow")]
        public void ClearingItemsDeletesItemsInDatabase()
        {
            var model = new InBoxModel(new InBoxItemRepository());
            model.Load();
            model.Add("hej");
            model.ClearItems();

            var persisted_model = new InBoxModel(new InBoxItemRepository());
            persisted_model.Load();
            Assert.That(persisted_model.Items.Count, Is.EqualTo(0));
        }

        [Test, Explicit, Category("slow")]
        public void SaveNewItems()
        {
            const string ITEM_NAME = "hej";
            var model = new InBoxModel(new InBoxItemRepository());
            model.Load();
            model.ClearItems();

            var expected_item = model.Add(ITEM_NAME);

            var persisted_model = new InBoxModel(new InBoxItemRepository());
            persisted_model.Load();
            Assert.That(persisted_model.Items.Count, Is.EqualTo(1));
            Assert.That(persisted_model.Items, Has.Member(expected_item));
        }

    }
}