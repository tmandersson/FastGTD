using System;
using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;

namespace FastGTD.Tests
{
    [TestFixture]
    public class CustomerTests
    {
        [Test]
        public void InBoxResultIsSaved()
        {
            // TODO: Move tests into main assembly again? No benefit in having it separate?
            // TODO: Model and Form should not be responsible for saving etc. Refactor test and code.

            string ITEM = Guid.NewGuid().ToString();

            var model = new InBoxModel();
            IInboxForm form = new InBoxForm(model);
            form.Show(); // TODO: Eliminate need to show dialog when testing.

            model.ClearItems();
            Assert.That(form.InBoxItems.Count, Is.EqualTo(0));
            model.AddItem(ITEM);
            form.Close();

            var model2 = new InBoxModel();
            IInboxForm form2 = new InBoxForm(model2);
            form2.Show(); // TODO: Eliminate need to show dialog when testing.
            Assert.That(model2.Items, Has.Count(1));
            Assert.That(model2.Items, Has.Member(ITEM));
        }
    }
}