using System;
using System.Collections.Generic;
using FastGTD.DataTransfer;
using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;
using StructureMap;

namespace FastGTD.IntegrationTests.CustomerTests
{
    [TestFixture]
    public class StartingWithEmptyModelsTests
    {
        private FastGTDApp _app;
        private readonly string _item_name1 = Guid.NewGuid().ToString();
        private readonly string _item_name2 = Guid.NewGuid().ToString();
        private ITestableInBoxView _view;

        [SetUp]
        public void SetupTests()
        {
            GetApplicationWithEmptyModels();
        }

        [Test]
        public void AddingInBoxItems()
        {
            AddItemToInBox(_item_name1);
            AddItemToInBox(_item_name2);
            Assert.That(InBoxItemCount(), Is.EqualTo(2));
            Assert.That(InBoxContainsItemWithName(_item_name1), Is.True);
            Assert.That(InBoxContainsItemWithName(_item_name2), Is.True);
        }


        // TODO: Kanske slå ihop dessa tre tester när de fungerar?
        [Test]
        public void ConvertInBoxItemToActionItem()
        {
            AddItemToInBox(_item_name1);
            AddItemToInBox(_item_name2);
            ConvertToActionItem(GetLastAddedInBoxItem());
            Assert.That(InBoxItemCount(), Is.EqualTo(1));
            Assert.That(InBoxContainsItemWithName(_item_name1), Is.True);
            Assert.That(InBoxContainsItemWithName(_item_name2), Is.False);
            Assert.That(ActionItemCount(), Is.EqualTo(1));
            Assert.That(ActionsListContainsItemWithName(_item_name2));
        }

        [Test]
        public void ConvertInBoxItemToActionItem_RemovalOfInBoxItemIsPersisted()
        {
            AddItemToInBox(_item_name1);
            AddItemToInBox(_item_name2);
            ConvertToActionItem(GetLastAddedInBoxItem());

            GetApplicationWithPreviousData();
            Assert.That(InBoxItemCount(), Is.EqualTo(1));
            Assert.That(InBoxContainsItemWithName(_item_name1), Is.True);
            Assert.That(InBoxContainsItemWithName(_item_name2), Is.False);
        }

        [Test]
        public void ConvertInBoxItemToActionItem_ChangesIsPersisted()
        {
            AddItemToInBox(_item_name1);
            AddItemToInBox(_item_name2);
            ConvertToActionItem(GetLastAddedInBoxItem());

            GetApplicationWithPreviousData();
            Assert.That(InBoxItemCount(), Is.EqualTo(1));
            Assert.That(InBoxContainsItemWithName(_item_name1), Is.True);
            Assert.That(InBoxContainsItemWithName(_item_name2), Is.False);
            Assert.That(ActionItemCount(), Is.EqualTo(1));
            Assert.That(ActionsListContainsItemWithName(_item_name2));
        }

        private void ConvertToActionItem(InBoxItem item)
        {
            _view.SelectItems(new List<InBoxItem> {item});
            _view.ClickToActionButton();
        }

        private void AddItemToInBox(string item_name)
        {
            _view.TextBoxText = item_name;
            _view.ClickAddButton();
        }

        private InBoxItem GetLastAddedInBoxItem()
        {
            return _app.InboxModel.Items[_app.InboxModel.Items.Count - 1];
        }

        private int InBoxItemCount()
        {
            return _app.InboxModel.Items.Count;
        }

        private int ActionItemCount()
        {
            return _app.ActionsListModel.Items.Count;
        }

        private bool InBoxContainsItemWithName(string name)
        {
            var items = new List<InBoxItem>(_app.InboxModel.Items);
            return items.Exists(x => x.Name == name);
        }

        private bool ActionsListContainsItemWithName(string name)
        {
            var items = new List<ActionItem>(_app.ActionsListModel.Items);
            return items.Exists(x => x.Name == name);
        }

        private void GetApplicationWithEmptyModels()
        {
            _app = CreateAndStartTestApp();
            _app.InboxModel.ClearItems();
        }

        private void GetApplicationWithPreviousData()
        {
            _app = CreateAndStartTestApp();
        }

        private FastGTDApp CreateAndStartTestApp()
        {
            FastGTDApp.WireClasses();
            InjectView();
            var app = new FastGTDApp();
            app.ShowStartForm();
            return app;
        }

        private void InjectView()
        {
            var form = new InBoxForm();
            _view = form;
            ObjectFactory.Inject((IInBoxView) form);
        }
    }
}