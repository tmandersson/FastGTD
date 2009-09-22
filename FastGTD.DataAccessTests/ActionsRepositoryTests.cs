using FastGTD.DataAccess;
using FastGTD.DataTransfer;
using FastGTD.Domain;
using NUnit.Framework;

namespace FastGTD.DataAccessTests
{
    [TestFixture]
    public class ActionsRepositoryTests : ItemRepositoryTests<ActionItem>
    {
        protected override ActionItem CreateItem(string name)
        {
            return new ActionItem(name);
        }

        protected override IItemPersistence<ActionItem> CreateRepo()
        {
            return new ActionsRepository();
        }
    }
}