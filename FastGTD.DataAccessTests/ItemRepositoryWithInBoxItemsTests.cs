using FastGTD.DataAccess;
using FastGTD.DataTransfer;
using FastGTD.Domain;
using NUnit.Framework;

namespace FastGTD.DataAccessTests
{
    [TestFixture]
    public class ItemRepositoryWithInBoxItemsTests : ItemRepositoryFixtureBase<InBoxItem>
    {
        protected override InBoxItem CreateItem(string name)
        {
            return new InBoxItem(name);
        }

        protected override IItemPersistence<InBoxItem> CreateRepo()
        {
            return new ItemRepository<InBoxItem>(InBoxItem.Table);
        }
    }
}