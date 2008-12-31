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

        [Test]
        public void DeleteByName()
        {
            string NAME = Guid.NewGuid().ToString();
            const int EXPECTED_COUNT = 0;
            var repo = new InBoxItemRepository();
            repo.CreateNew(NAME);
            repo.CreateNew(NAME);
            repo.CreateNew(NAME);

            repo.DeleteByName(NAME);

            var repo2 = new InBoxItemRepository();
            IList<InBoxItem> result = repo2.GetAll();
            Assert.That(result, Has.Count(EXPECTED_COUNT));
        }
    }
}