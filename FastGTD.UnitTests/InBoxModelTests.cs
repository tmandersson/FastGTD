using FastGTD.DataTransfer;
using FastGTD.Domain;
using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;
using Rhino.Mocks;

namespace FastGTD.UnitTests
{
    [TestFixture]
    public class InBoxModelTests
    {
        private InBoxModel _model;

        [SetUp]
        public void SetupTests()
        {
            var repo = MockRepository.GenerateStub<IGTDItemPersistence<InBoxItem>>();
            _model = new InBoxModel(repo);
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
    }
}