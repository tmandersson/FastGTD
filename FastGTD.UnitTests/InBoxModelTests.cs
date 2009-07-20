using FastGTD.DataAccess;
using FastGTD.DataTransfer;
using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;
using Rhino.Mocks;

namespace FastGTD.UnitTests
{
    [TestFixture]
    public class InBoxModelTests
    {
        private InBoxModel _model;
        private ActionsListModel _actions_list_model;

        [SetUp]
        public void SetupTests()
        {
            var repo = MockRepository.GenerateStub<IInBoxPersistenceProvider>();
            _actions_list_model = new ActionsListModel();
            _model = new InBoxModel(repo, _actions_list_model);
            Assert.That(_model.Items, Has.Count(0));
        }

        [Test]
        public void Remove_OnItem_RemovesIt()
        {

            _model.Add("foo");
            Assert.That(_model.Items, Has.Count(1));
            var item = _model.Add("bar");
            Assert.That(_model.Items, Has.Count(2));

            _model.Remove(item);

            Assert.That(_model.Items, Has.Count(1));
            Assert.That(_model.Items[0].Name, Is.EqualTo("foo"));
        }

        [Test]
        public void ClearItems_WithTwoItems_RemovesThem()
        {
            _model.Add("foo");
            Assert.That(_model.Items, Has.Count(1));
            _model.Add("bar");
            Assert.That(_model.Items, Has.Count(2));

            _model.ClearItems();

            Assert.That(_model.Items, Has.Count(0));
        }

        [Test]
        public void ConvertToAction_OnExistingItem_RemovesIt()
        {
            InBoxItem item = _model.Add("foo");
            _model.ConvertToAction(item);
            Assert.That(_model.Items, Has.Count(0));
        }

        [Test]
        public void ConvertToAction_OnExistingItem_AddsItAsActionOnActionsList()
        {
            InBoxItem item = _model.Add("foo");
            _model.ConvertToAction(item);
            Assert.That(_actions_list_model.Items, Has.Count(1));
            Assert.That(_actions_list_model.Items, Has.Some.Property("Name").EqualTo("foo"));
        }

        [Test]
        public void ConvertToAction_OnNonExistantItem_DoesNotThrow()
        {
            var item = new InBoxItem("foo");
            _model.ConvertToAction(item);
        }
    }
}