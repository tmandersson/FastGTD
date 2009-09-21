using System;
using FastGTD.DataAccess;
using FastGTD.DataTransfer;
using FastGTD.Domain;
using NUnit.Framework;

namespace FastGTD.DataAccessTests
{
    [TestFixture]
    public class ActionsRepositoryTests : ItemRepositoryTests<ActionItem>
    {
        [Test]
        public void GetAll_LoadsExistingActions()
        {
            string name = Guid.NewGuid().ToString();
            var repo = new ActionsRepository();
            var action = new ActionItem(name);
            repo.Save(action);

            var repo2 = new ActionsRepository();
            var actual_actions = repo2.GetAll();
            Assert.That(actual_actions, Has.Member(action));
        }

        [Test]
        public void DeleteAll_DeletesAllActions()
        {
            string name = Guid.NewGuid().ToString();
            var repo = new ActionsRepository();
            var action = new ActionItem(name);
            repo.Save(action);

            repo.DeleteAll();

            var repo2 = new ActionsRepository();
            var actual_actions = repo2.GetAll();
            Assert.That(actual_actions, Has.Count.EqualTo(0));
        }

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