using System;
using FastGTD.DataTransfer;
using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;

namespace FastGTD.IntegrationTests.CustomerTests
{
    [TestFixture]
    public class StartingWithEmptyModelsTests
    {
        private FastGTDApp _app;
        private readonly string _item_name1 = Guid.NewGuid().ToString();
        private readonly string _item_name2 = Guid.NewGuid().ToString();

        [Test]
        public void AddingInBoxItems()
        {
            GetApplicationWithEmptyModels();
            InBoxItem item1 = AddItemToInBox(_item_name1);
            InBoxItem item2 = AddItemToInBox(_item_name2);
            Assert.That(InBoxItemCount(), Is.EqualTo(2));
            Assert.That(InBoxContains(item1), Is.True);
            Assert.That(InBoxContains(item2), Is.True);
        }

        [Test]
        public void ConvertInBoxItemToActionItem()
        {
            GetApplicationWithEmptyModels();
            InBoxItem item1 = AddItemToInBox(_item_name1);
            InBoxItem item2 = AddItemToInBox(_item_name2);
            ActionItem action = ConvertToActionItem(item2);
            Assert.That(InBoxItemCount(), Is.EqualTo(1));
            Assert.That(InBoxContains(item1), Is.True);
            Assert.That(InBoxContains(item2), Is.False);
            Assert.That(ActionItemCount(), Is.EqualTo(1));
            Assert.That(ActionsListContains(action));
        }

        private ActionItem ConvertToActionItem(InBoxItem item)
        {
            return _app.InboxModel.ConvertToAction(item);
        }

        private InBoxItem AddItemToInBox(string item_name)
        {
            return _app.InboxModel.Add(item_name);
        }

        private int InBoxItemCount()
        {
            return _app.InboxModel.Items.Count;
        }

        private int ActionItemCount()
        {
            return _app.ActionsListModel.Items.Count;
        }

        private bool InBoxContains(InBoxItem item)
        {
            return _app.InboxModel.Items.Contains(item);
        }

        private bool ActionsListContains(ActionItem item)
        {
            return _app.ActionsListModel.Items.Contains(item);
        }

        private void GetApplicationWithEmptyModels()
        {
            _app = CreateAndStartTestApp();
            _app.InboxModel.ClearItems();
        }

        private static FastGTDApp CreateAndStartTestApp()
        {
            var app = new FastGTDApp();
            app.ShowStartForm();
            return app;
        }
    }
}