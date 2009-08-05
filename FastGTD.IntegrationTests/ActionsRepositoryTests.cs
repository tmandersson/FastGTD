using System;
using FastGTD.DataAccess;
using FastGTD.DataTransfer;
using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;

namespace FastGTD.IntegrationTests
{
    [TestFixture]
    public class ActionsRepositoryTests
    {
        [Test]
        public void SaveInBoxItem()
        {
            string name = Guid.NewGuid().ToString();
            var repo = new ActionsRepository();
            var action = new ActionItem(name);
            repo.Save(action);

            var repo2 = new ActionsRepository();
            var actual_action = repo2.GetById(action.Id);
            Assert.That(actual_action.Name, Is.EqualTo(name));
        }
    }
}