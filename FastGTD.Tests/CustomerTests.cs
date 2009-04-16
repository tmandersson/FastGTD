using System;
using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;

namespace FastGTD.Tests
{
    [TestFixture, Explicit, Category("slow")]
    public class CustomerTests
    {
        [Test]
        public void NewInBoxItemIsSaved()
        {
            string ITEM = Guid.NewGuid().ToString();

            FastGTDApp app = FastGTDApp.StartNewTestApplication();
            app.InboxModel.ClearItems();
            Assert.That(app.InboxForm.ListViewItems.Count, Is.EqualTo(0));
            var expected_item = app.InboxModel.CreateItem(ITEM);
            app.Close();

            FastGTDApp app2 = FastGTDApp.StartNewTestApplication();
            Assert.That(app2.InboxModel.Items, Has.Count(1));
            Assert.That(app2.InboxModel.Items, Has.Member(expected_item));
            app2.Close();
        }

        [Test]
        public void AddingAndRemovingInBoxResultIsSaved()
        {
            string ITEM = Guid.NewGuid().ToString();
            string ITEM2 = Guid.NewGuid().ToString();

            FastGTDApp app = FastGTDApp.StartNewTestApplication();
            app.InboxModel.ClearItems();
            Assert.That(app.InboxForm.ListViewItems.Count, Is.EqualTo(0));
            var item = app.InboxModel.CreateItem(ITEM);
            var item2 = app.InboxModel.CreateItem(ITEM2);
            app.InboxModel.RemoveItem(item);
            app.Close();

            FastGTDApp app2 = FastGTDApp.StartNewTestApplication();
            Assert.That(app2.InboxModel.Items, Has.Count(1));
            Assert.That(app2.InboxModel.Items, Has.Member(item2));
            Assert.That(app2.InboxModel.Items, Has.No.Member(item));
            app2.Close();
        }
    }
}