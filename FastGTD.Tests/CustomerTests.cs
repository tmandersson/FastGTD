using System;
using FastGTD.DataTransfer;
using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;

namespace FastGTD.Tests
{
    [TestFixture]
    public class CustomerTests
    {
        [Test]
        public void NewInBoxItemIsSaved()
        {
            // TODO: Move tests into main assembly again? No benefit in having it separate?
            // TODO: Model and Form should not be responsible for saving etc. Refactor test and code.

            string ITEM = Guid.NewGuid().ToString();

            FastGTDApp app = FastGTDApp.StartNewTestApplication();
            app.InModel.ClearItems();
            Assert.That(app.InForm.InBoxItems.Count, Is.EqualTo(0));
            app.InModel.AddItem(ITEM);
            app.Close();

            FastGTDApp app2 = FastGTDApp.StartNewTestApplication();
            Assert.That(app2.InModel.Items, Has.Count(1));
            Assert.That(app2.InModel.Items, Has.Member(ITEM));
            app2.Close();
        }

        [Test]
        public void AddingAndRemovingInBoxResultIsSaved()
        {
            string ITEM = Guid.NewGuid().ToString();
            string ITEM2 = Guid.NewGuid().ToString();

            FastGTDApp app = FastGTDApp.StartNewTestApplication();
            app.InModel.ClearItems();
            Assert.That(app.InForm.InBoxItems.Count, Is.EqualTo(0));
            app.InModel.AddItem(ITEM);
            app.InModel.AddItem(ITEM2);
            app.InModel.RemoveItem(ITEM);
            app.Close();

            FastGTDApp app2 = FastGTDApp.StartNewTestApplication();
            Assert.That(app2.InModel.Items, Has.Count(1));
            Assert.That(app2.InModel.Items, Has.Member(ITEM2));
            Assert.That(app2.InModel.Items, Has.No.Member(ITEM));
            app2.Close();
        }
    }
}