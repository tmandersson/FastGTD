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
            string item_name = Guid.NewGuid().ToString();

            FastGTDApp app = CreateAndStartTestApp();
            app.InboxModel.ClearItems();
            Assert.That(app.InboxView.ListViewInBoxItems.Items.Count, Is.EqualTo(0));
            var expected_item = app.InboxModel.CreateItem(item_name);
            app.Close();

            FastGTDApp app2 = CreateAndStartTestApp();
            Assert.That(app2.InboxModel.Items, Has.Count(1));
            Assert.That(app2.InboxModel.Items, Has.Member(expected_item));
            app2.Close();
        }

        [Test]
        public void AddingAndRemovingInBoxResultIsSaved()
        {
            string item_name = Guid.NewGuid().ToString();
            string item_name2 = Guid.NewGuid().ToString();

            FastGTDApp app = CreateAndStartTestApp();
            app.InboxModel.ClearItems();
            Assert.That(app.InboxView.ListViewInBoxItems.Items.Count, Is.EqualTo(0));
            var item = app.InboxModel.CreateItem(item_name);
            var item2 = app.InboxModel.CreateItem(item_name2);
            app.InboxModel.RemoveItem(item);
            app.Close();

            FastGTDApp app2 = CreateAndStartTestApp();
            Assert.That(app2.InboxModel.Items, Has.Count(1));
            Assert.That(app2.InboxModel.Items, Has.Member(item2));
            Assert.That(app2.InboxModel.Items, Has.No.Member(item));
            app2.Close();
        }

        private static FastGTDApp CreateAndStartTestApp()
        {
            var app = new FastGTDApp();
            app.ShowStartForm();
            return app;
        }
    }
}