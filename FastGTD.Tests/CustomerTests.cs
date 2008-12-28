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
            var expected_item = app.InModel.CreateItem(ITEM);
            app.Close();

            FastGTDApp app2 = FastGTDApp.StartNewTestApplication();
            Assert.That(app2.InModel.Items, Has.Count(1));
            Assert.That(app2.InModel.Items, Has.Member(expected_item));
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
            var item = app.InModel.CreateItem(ITEM);
            var item2 = app.InModel.CreateItem(ITEM2);
            app.InModel.RemoveItem(item);
            app.Close();

            FastGTDApp app2 = FastGTDApp.StartNewTestApplication();
            Assert.That(app2.InModel.Items, Has.Count(1));
            Assert.That(app2.InModel.Items, Has.Member(item2));
            Assert.That(app2.InModel.Items, Has.No.Member(item));
            app2.Close();
        }

        [Test, Ignore("Not done")]
        public void RemovingItemShouldNotDeleteOtherItemsWithSameName()
        {
            string ITEM = Guid.NewGuid().ToString();
            string ITEM2 = Guid.NewGuid().ToString();

            FastGTDApp app = FastGTDApp.StartNewTestApplication();
            app.InModel.ClearItems();
            Assert.That(app.InForm.InBoxItems.Count, Is.EqualTo(0));
            var item = app.InModel.CreateItem(ITEM);
            var existing_item = app.InModel.CreateItem(ITEM);
            app.InModel.CreateItem(ITEM);
            var item2 = app.InModel.CreateItem(ITEM2);
            app.InModel.RemoveItem(item);
            app.Close();

            FastGTDApp app2 = FastGTDApp.StartNewTestApplication();
            Assert.That(app2.InModel.Items, Has.Count(3));
            Assert.That(app2.InModel.Items, Has.Member(item2));
            Assert.That(app2.InModel.Items, Has.Member(existing_item));
            app2.Close();
        }
    }
}