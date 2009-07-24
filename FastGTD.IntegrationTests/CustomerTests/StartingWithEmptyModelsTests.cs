using System;
using System.Collections.Generic;
using FastGTD.DataTransfer;
using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;
using Rhino.Mocks;
using StructureMap;

namespace FastGTD.IntegrationTests.CustomerTests
{
    [TestFixture]
    public class StartingWithEmptyModelsTests
    {
        private FastGTDApp _app;
        private IInBoxView _view_stub;
        private readonly string _item_name1 = Guid.NewGuid().ToString();
        private readonly string _item_name2 = Guid.NewGuid().ToString();

        [Test]
        public void AddingInBoxItems()
        {
            GetApplicationWithEmptyModels();
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
            GetApplicationWithEmptyModels();
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
            GetApplicationWithEmptyModels();
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
            GetApplicationWithEmptyModels();
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
            _view_stub.Stub(x => x.SelectedItems).Return(new List<InBoxItem> {item});
            _view_stub.Raise(x => x.ToActionButtonWasClicked += null);
        }

        private void AddItemToInBox(string item_name)
        {
            _view_stub.TextBoxText = item_name;
            _view_stub.Raise(x => x.AddButtonWasClicked += null);
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
            InjectViewStub();
            var app = new FastGTDApp();
            app.ShowStartForm();
            return app;
        }

        private void InjectViewStub()
        {
            _view_stub = MockRepository.GenerateStub<IInBoxView>();
            _view_stub.Stub(x => x.List).Return(MockRepository.GenerateStub<IListSelectionChanger>());
            ObjectFactory.Inject(_view_stub);
        }
    }
}