using FastGTD.DataTransfer;
using FastGTD.Domain;
using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;
using Rhino.Mocks;

namespace FastGTD.UnitTests
{
    [TestFixture]
    public class ItemConversiontests
    {
        private ItemModel<InBoxItem> _model;
        private ItemModel<ActionItem> _actions_list_model;
        private ItemConverter _converter;

        [SetUp]
        public void SetupTests()
        {
            var inbox_persistence = MockRepository.GenerateStub<IItemPersistence<InBoxItem>>();
            var actions_persistence = MockRepository.GenerateStub<IItemPersistence<ActionItem>>();
            _actions_list_model = new ItemModel<ActionItem>(actions_persistence);
            _model = new ItemModel<InBoxItem>(inbox_persistence);
            _converter = new ItemConverter(_model, _actions_list_model);
            Assert.That(_model.Items, Has.Count(0));
        }

        [Test]
        public void ConvertToAction_OnExistingItem_RemovesIt()
        {
            InBoxItem item = _model.Add("foo");
            _converter.ConvertToAction(item);
            Assert.That(_model.Items, Has.Count(0));
        }

        [Test]
        public void ConvertToAction_OnExistingItem_AddsItAsActionOnActionsList()
        {
            InBoxItem item = _model.Add("foo");
            _converter.ConvertToAction(item);
            Assert.That(_actions_list_model.Items, Has.Count(1));
            Assert.That(_actions_list_model.Items, Has.Some.Property("Name").EqualTo("foo"));
        }

        [Test]
        public void ConvertToAction_OnNonExistantItem_DoesNotThrow()
        {
            var item = new InBoxItem("foo");
            _converter.ConvertToAction(item);
        }
    }
}