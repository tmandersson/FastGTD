using System.Collections.Generic;
using FastGTD.Domain;
using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;
using Rhino.Mocks;

namespace FastGTD.UnitTests
{
    [TestFixture]
    public class ItemModelTests
    {
        private IItemPersistence<GTDItem> _persistence;
        private ItemModel<GTDItem> _model;

        [Test]
        public void Add_WithText_SavesNewActionWithTextAsName()
        {
            CreatePersistenceAsMock();
            CreateModel();
            const string NAME = "foo";
            
            _model.Add(NAME);

            _persistence.AssertWasCalled(x => x.Save(Arg<GTDItem>.Matches(i => i.Name == NAME)));
        }

        [Test]
        public void Load_OnEmptyModel_LoadsItems()
        {
            CreatePersistenceAsStub();
            CreateModel();
            var item1 = new GTDItem("foo");
            var item2 = new GTDItem("foo2");
            _persistence.Stub(x => x.GetAll()).Return(new List<GTDItem> { item1, item2 });

            _model.Load();

            Assert.That(_model.Items, Has.Member(item1));
            Assert.That(_model.Items, Has.Member(item2));
        }

        [Test]
        public void Remove_OnItem_RemovesIt()
        {
            CreatePersistenceAsStub();
            CreateModel();
            _model.Add("foo");
            var item = _model.Add("bar");

            _model.Remove(item);

            Assert.That(_model.Items, Has.Count(1));
            Assert.That(_model.Items[0].Name, Is.EqualTo("foo"));
        }

        [Test]
        public void ClearItems_WithTwoItems_RemovesThem()
        {
            CreatePersistenceAsStub();
            CreateModel();
            _model.Add("foo");
            Assert.That(_model.Items, Has.Count(1));
            _model.Add("bar");
            Assert.That(_model.Items, Has.Count(2));

            _model.ClearItems();

            Assert.That(_model.Items, Has.Count(0));
        }

        [Test]
        public void ClearItems_DeletesAllWithPersistence()
        {
            CreatePersistenceAsMock();
            CreateModel();
            _model.ClearItems();
            _persistence.AssertWasCalled(x => x.DeleteAll());
        }

        private void CreateModel()
        {
            _model = new ItemModel<GTDItem>(_persistence);
        }

        private void CreatePersistenceAsMock()
        {
            _persistence = MockRepository.GenerateMock<IItemPersistence<GTDItem>>();
        }

        private void CreatePersistenceAsStub()
        {
            _persistence = MockRepository.GenerateStub<IItemPersistence<GTDItem>>();
        }
    }
}
