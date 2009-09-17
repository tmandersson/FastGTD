using System;
using FastGTD.DataAccess;
using FastGTD.DataTransfer;
using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;

namespace FastGTD.DataAccessTests
{
    [TestFixture]
    public class ActionsRepositoryTests
    {
        [Test]
        public void Save_WithAnAction_SavesIt()
        {
            string name = Guid.NewGuid().ToString();
            var repo = new ActionsRepository();
            var action = new ActionItem(name);
            repo.Save(action);

            var repo2 = new ActionsRepository();
            var actual_action = repo2.GetById(action.Id);
            Assert.That(actual_action.Name, Is.EqualTo(name));
        }

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
            Assert.That(actual_actions, Has.Count(0));
        }
    }
}