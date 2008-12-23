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
            var item = new InBoxItem(NAME);
            repo.Save(item);

            var repo2 = new InBoxItemRepository();
            var actual_item = repo2.GetByID(item.ID);
            Assert.That(actual_item.Name, Is.EqualTo(NAME));
        }

        [Test, Ignore("Unfinished")]
        public void GetAllItems()
        {
            
        }
    }
}