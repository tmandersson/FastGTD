using System;
using System.Collections.Generic;
using FastGTD.DataAccess;
using FastGTD.DataTransfer;
using FastGTD.Domain;
using NUnit.Framework;

namespace FastGTD.DataAccessTests
{
    [TestFixture]
    public class InBoxItemRepositoryTests : ItemRepositoryTests<InBoxItem>
    {
        [Test]
        public void GetAllItems()
        {
            string name = Guid.NewGuid().ToString();
            var repo = new InBoxItemRepository();
            IList<InBoxItem> existing = repo.GetAll();
            int expected_count = existing.Count + 1;
            var item = new InBoxItem(name);
            repo.Save(item);

            var repo2 = new InBoxItemRepository();
            IList<InBoxItem> result = repo2.GetAll();

            Assert.That(result, Has.Count.EqualTo(expected_count));
            Assert.That(result, Has.Some.Property("Name").EqualTo(name));
        }

        [Test]
        public void DeleteAllItems()
        {
            string name = Guid.NewGuid().ToString();
            const int EXPECTED_COUNT = 0;
            var repo = new InBoxItemRepository();
            var item = new InBoxItem(name);
            InBoxItem temp = item;

            repo.DeleteAll();

            var repo2 = new InBoxItemRepository();
            IList<InBoxItem> result = repo2.GetAll();
            Assert.That(result, Has.Count.EqualTo(EXPECTED_COUNT));
        }

        [Test]
        public void Delete()
        {
            string name = Guid.NewGuid().ToString();
            const int EXPECTED_COUNT = 2;
            var repo = new InBoxItemRepository();
            repo.DeleteAll();
            var item1 = new InBoxItem(name);
            repo.Save(item1);
            var item2 = new InBoxItem(name);
            repo.Save(item2);
            var item3 = new InBoxItem(name);
            repo.Save(item3);

            repo.Delete(item1);

            var repo2 = new InBoxItemRepository();
            IList<InBoxItem> result = repo2.GetAll();
            Assert.That(result, Has.Count.EqualTo(EXPECTED_COUNT));
        }

        protected override InBoxItem CreateItem(string name)
        {
            return new InBoxItem(name);
        }

        protected override IItemPersistence<InBoxItem> CreateRepo()
        {
            return new InBoxItemRepository();
        }
    }
}