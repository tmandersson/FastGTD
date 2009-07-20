using FastGTD.DataAccess;
using FastGTD.DataTransfer;
using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;
using Rhino.Mocks;

namespace FastGTD.UnitTests
{
    [TestFixture]
    public class ItemConversiontests
    {
        private InBoxModel _model;
        private ActionsListModel _actions_list_model;
        private ItemConverter _converter;

        [SetUp]
        public void SetupTests()
        {
            var repo = MockRepository.GenerateStub<IInBoxPersistenceProvider>();
            _actions_list_model = new ActionsListModel();
            _model = new InBoxModel(repo);
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