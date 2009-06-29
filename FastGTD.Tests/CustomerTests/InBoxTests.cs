using System;
using FastGTD.DataTransfer;
using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;

namespace FastGTD.Tests.CustomerTests
{
    [TestFixture, Explicit, Category("slow")]
    public class InBoxTests
    {
        [Test]
        public void NewInBoxItemIsSaved()
        {
            string item_name = Guid.NewGuid().ToString();

            FastGTDApp app = CreateAndStartTestApp();
            app.InboxModel.ClearItems();
            var expected_item = app.InboxModel.Add(item_name);
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
            var item = app.InboxModel.Add(item_name);
            var item2 = app.InboxModel.Add(item_name2);
            app.InboxModel.Remove(item);
            app.Close();

            FastGTDApp app2 = CreateAndStartTestApp();
            Assert.That(app2.InboxModel.Items, Has.Count(1));
            Assert.That(app2.InboxModel.Items, Has.Member(item2));
            Assert.That(app2.InboxModel.Items, Has.No.Member(item));
            app2.Close();
        }

        private static FastGTDApp CreateAndStartTestApp()
        {
            var app = FastGTDApp.Create();
            app.ShowStartForm();
            return app;
        }
    }

    [TestFixture, Explicit, Category("slow")]
    public class XxxTests
    {
        private FastGTDApp _app;
        private readonly string _item_name1 = Guid.NewGuid().ToString();
        private readonly string _item_name2 = Guid.NewGuid().ToString();

        [Test]
        public void AddingInBoxItems()
        {
            GetAppWithEmptyInBox();
            InBoxItem item1 = AddInBoxItem(_item_name1);
            InBoxItem item2 = AddInBoxItem(_item_name2);
            Assert.That(InBoxCount(), Is.EqualTo(2));
            Assert.That(InBoxContains(item1), Is.True);
            Assert.That(InBoxContains(item2), Is.True);
        }

        private InBoxItem AddInBoxItem(string item_name)
        {
            return _app.InboxModel.Add(item_name);
        }

        private int InBoxCount()
        {
            return _app.InboxModel.Items.Count;
        }

        private bool InBoxContains(InBoxItem item_name)
        {
            return _app.InboxModel.Items.Contains(item_name);
        }

        private void GetAppWithEmptyInBox()
        {
            _app = CreateAndStartTestApp();
            _app.InboxModel.ClearItems();
        }

        private static FastGTDApp CreateAndStartTestApp()
        {
            var app = FastGTDApp.Create();
            app.ShowStartForm();
            return app;
        }
    }
}