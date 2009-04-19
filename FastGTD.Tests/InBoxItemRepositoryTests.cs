using System;
using System.Collections.Generic;
using FastGTD.DataAccess;
using FastGTD.DataTransfer;
using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;

namespace FastGTD.Tests
{
    [TestFixture, Explicit, Category("slow")]
    public class InBoxItemRepositoryTests
    {
        [Test]
        public void SaveInBoxItem()
        {
            string name = Guid.NewGuid().ToString();
            var repo = new InBoxItemRepository();
            InBoxItem item = repo.CreateNew(name);

            var repo2 = new InBoxItemRepository();
            var actual_item = repo2.GetById(item.Id);
            Assert.That(actual_item.Name, Is.EqualTo(name));
        }

        [Test]
        public void GetAllItems()
        {
            string name = Guid.NewGuid().ToString();
            var repo = new InBoxItemRepository();
            IList<InBoxItem> existing = repo.GetAll();
            int expected_count = existing.Count + 1;
            repo.CreateNew(name);

            var repo2 = new InBoxItemRepository();
            IList<InBoxItem> result = repo2.GetAll();

            Assert.That(result, Has.Count(expected_count));
            Assert.That(result, Has.Some.Property("Name").EqualTo(name));
        }

        [Test]
        public void DeleteAllItems()
        {
            string name = Guid.NewGuid().ToString();
            const int EXPECTED_COUNT = 0;
            var repo = new InBoxItemRepository();
            repo.CreateNew(name);

            repo.DeleteAll();

            var repo2 = new InBoxItemRepository();
            IList<InBoxItem> result = repo2.GetAll();
            Assert.That(result, Has.Count(EXPECTED_COUNT));
        }

        [Test]
        public void Delete()
        {
            string name = Guid.NewGuid().ToString();
            const int EXPECTED_COUNT = 2;
            var repo = new InBoxItemRepository();
            repo.DeleteAll();
            var item = repo.CreateNew(name);
            repo.CreateNew(name);
            repo.CreateNew(name);

            repo.Delete(item);

            var repo2 = new InBoxItemRepository();
            IList<InBoxItem> result = repo2.GetAll();
            Assert.That(result, Has.Count(EXPECTED_COUNT));
        }
    }
}