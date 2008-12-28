using System;
using System.Collections.Generic;
using FastGTD.DataAccess;
using FastGTD.DataTransfer;
using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;

namespace FastGTD.Tests
{
    [TestFixture]
    public class InBoxItemRepositoryTests
    {
        [Test]
        public void SaveInBoxItem()
        {
            string NAME = Guid.NewGuid().ToString();
            var repo = new InBoxItemRepository();
            InBoxItem item = repo.CreateNew(NAME);

            var repo2 = new InBoxItemRepository();
            var actual_item = repo2.GetByID(item.ID);
            Assert.That(actual_item.Name, Is.EqualTo(NAME));
        }

        [Test]
        public void GetAllItems()
        {
            string NAME = Guid.NewGuid().ToString();
            var repo = new InBoxItemRepository();
            IList<InBoxItem> existing = repo.GetAll();
            int EXPECTED_COUNT = existing.Count + 1;
            repo.CreateNew(NAME);

            var repo2 = new InBoxItemRepository();
            IList<InBoxItem> result = repo2.GetAll();

            Assert.That(result, Has.Count(EXPECTED_COUNT));
            Assert.That(result, Has.Some.Property("Name").EqualTo(NAME));
        }

        [Test]
        public void DeleteAllItems()
        {
            string NAME = Guid.NewGuid().ToString();
            const int EXPECTED_COUNT = 0;
            var repo = new InBoxItemRepository();
            repo.CreateNew(NAME);

            repo.DeleteAll();

            var repo2 = new InBoxItemRepository();
            IList<InBoxItem> result = repo2.GetAll();
            Assert.That(result, Has.Count(EXPECTED_COUNT));
        }
    }

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

    }
}