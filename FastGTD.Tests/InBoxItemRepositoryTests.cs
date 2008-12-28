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
            const string NAME = "foo";
            var repo = new InBoxItemRepository();
            InBoxItem item = repo.CreateNew(NAME);

            var repo2 = new InBoxItemRepository();
            var actual_item = repo2.GetByID(item.ID);
            Assert.That(actual_item.Name, Is.EqualTo(NAME));
        }

        [Test]
        public void GetAllItems()
        {
            const string NAME = "foo";
            var repo = new InBoxItemRepository();
            IList<InBoxItem> existing = repo.GetAll();
            int EXPECTED_COUNT = existing.Count + 1;
            repo.CreateNew(NAME);

            var repo2 = new InBoxItemRepository();
            IList<InBoxItem> result = repo2.GetAll();

            Assert.That(result, Has.Count(EXPECTED_COUNT));
            Assert.That(result, Has.Some.Property("Name").EqualTo(NAME));
        }
    }
}