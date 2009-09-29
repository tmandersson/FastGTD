using System;
using System.Collections.Generic;
using FastGTD.DataTransfer;
using FastGTD.Domain;
using NUnit.Framework;
using StructureMap;

namespace FastGTD.CustomerTests
{
    [TestFixture]
    public class StartingWithEmptyModelsTests
    {
        private readonly string _item_name1 = Guid.NewGuid().ToString();
        private readonly string _item_name2 = Guid.NewGuid().ToString();
        private ITestableInBoxView _view;
        private IItemModel<InBoxItem> _inbox_model;
        private IItemModel<ActionItem> _actions_list_model;

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
            return _inbox_model.Items[_inbox_model.Items.Count - 1];
        }

        private int InBoxItemCount()
        {
            return _inbox_model.Items.Count;
        }

        private int ActionItemCount()
        {
            return _actions_list_model.Items.Count;
        }

        private bool InBoxContainsItemWithName(string name)
        {
            var items = new List<InBoxItem>(_inbox_model.Items);
            return items.Exists(x => x.Name == name);
        }

        private bool ActionsListContainsItemWithName(string name)
        {
            var items = new List<ActionItem>(_actions_list_model.Items);
            return items.Exists(x => x.Name == name);
        }

        private void GetApplicationWithEmptyModels()
        {
            CreateAndStartTestApp();
            _inbox_model = ObjectFactory.GetInstance<IItemModel<InBoxItem>>();
            _inbox_model.ClearItems();
            _actions_list_model = ObjectFactory.GetInstance<IItemModel<ActionItem>>();
            _actions_list_model.ClearItems();
        }

        private void GetApplicationWithPreviousData()
        {
            CreateAndStartTestApp();
            _inbox_model = ObjectFactory.GetInstance<IItemModel<InBoxItem>>();
            _actions_list_model = ObjectFactory.GetInstance<IItemModel<ActionItem>>();
            _actions_list_model.Load();
        }

        private void CreateAndStartTestApp()
        {
            FastGTDApp.WireClasses();
            InjectView();
            var start_form = FastGTDApp.GetStartForm();
            start_form.Show();
        }

        private void InjectView()
        {
            var form = new InBoxForm();
            _view = form;
            ObjectFactory.Inject((IInBoxView) form);
        }
    }
}