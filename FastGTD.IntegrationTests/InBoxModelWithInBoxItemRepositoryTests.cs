using System;
using System.Collections.Generic;
using FastGTD.DataAccess;
using FastGTD.DataTransfer;
using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;

namespace FastGTD.IntegrationTests
{
    [TestFixture]
    public class InBoxModelWithInBoxItemRepositoryTests
    {
        [Test]
        public void LoadingExistingItems()
        {
            string name = Guid.NewGuid().ToString();
            var repo = new InBoxItemRepository();
            IList<InBoxItem> existing = repo.GetAll();
            int expected_count = existing.Count + 1;
            var item = new InBoxItem(name);
            repo.Save(item);

            InBoxModel model = CreateModel();
            model.Load();

            Assert.That(model.Items, Has.Count(expected_count));
            Assert.That(model.Items, Has.Member(item));
        }

        [Test]
        public void ClearingItemsDeletesItemsInDatabase()
        {
            InBoxModel model = CreateModel();
            model.Load();
            model.Add("hej");

            model.ClearItems();

            var persisted_model = CreateModel();
            persisted_model.Load();
            Assert.That(persisted_model.Items.Count, Is.EqualTo(0));
        }

        [Test]
        public void SaveNewItems()
        {
            const string ITEM_NAME = "hej";
            var model = CreateModel();
            model.Load();
            model.ClearItems();

            var expected_item = model.Add(ITEM_NAME);

            var persisted_model = CreateModel();
            persisted_model.Load();
            Assert.That(persisted_model.Items.Count, Is.EqualTo(1));
            Assert.That(persisted_model.Items, Has.Member(expected_item));
        }

        private static InBoxModel CreateModel()
        {
            return new InBoxModel(new InBoxItemRepository());
        }
    }
}