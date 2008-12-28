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
        public void LoadingExistingItems()
        {
            string NAME = Guid.NewGuid().ToString();
            var repo = new InBoxItemRepository();
            IList<InBoxItem> existing = repo.GetAll();
            int EXPECTED_COUNT = existing.Count + 1;
            repo.CreateNew(NAME);

            var model = new InBoxModel(new InBoxItemRepository());
            model.Load();

            Assert.That(model.Items, Has.Count(EXPECTED_COUNT));
            Assert.That(model.Items, Has.Member(NAME));
        }

        [Test]
        public void ClearingItemsDeletesItemsInDatabase()
        {
            var model = new InBoxModel(new InBoxItemRepository());
            model.Load();
            model.AddItem("hej");
            model.ClearItems();

            var persisted_model = new InBoxModel(new InBoxItemRepository());
            persisted_model.Load();
            Assert.That(persisted_model.Items.Count, Is.EqualTo(0));
        }

        [Test]
        public void SaveNewItems()
        {
            const string ITEM_NAME = "hej";
            var model = new InBoxModel(new InBoxItemRepository());
            model.Load();
            model.ClearItems();

            model.AddItem(ITEM_NAME);

            var persisted_model = new InBoxModel(new InBoxItemRepository());
            persisted_model.Load();
            Assert.That(persisted_model.Items.Count, Is.EqualTo(1));
            Assert.That(persisted_model.Items, Has.Member(ITEM_NAME));
        }

    }
}