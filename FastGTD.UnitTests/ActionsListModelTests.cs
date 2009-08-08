using System.Collections.Generic;
using FastGTD.DataTransfer;
using FastGTD.Domain;
using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;
using Rhino.Mocks;

namespace FastGTD.UnitTests
{
    [TestFixture]
    public class ActionsListModelTests
    {
        [Test]
        public void Add_WithText_SavesNewActionWithTextAsName()
        {
            const string NAME = "foo";
            var persistence = MockRepository.GenerateMock<IGTDItemPersistence<ActionItem>>();
            var model = new ActionsListModel(persistence);
            
            model.Add(NAME);

            persistence.AssertWasCalled(x => x.Save(Arg<ActionItem>.Matches(i => i.Name == NAME)));
        }

        [Test]
        public void Load_OnEmptyModel_LoadsItems()
        {
            var item1 = new ActionItem("foo");
            var item2 = new ActionItem("foo2");
            var persistence = MockRepository.GenerateStub<IGTDItemPersistence<ActionItem>>();
            persistence.Stub(x => x.GetAll()).Return(new List<ActionItem> {item1, item2});

            var model = new ActionsListModel(persistence);
            model.Load();

            Assert.That(model.Items, Has.Member(item1));
            Assert.That(model.Items, Has.Member(item2));
        }

        [Test]
        public void ClearItems_WithTwoItems_RemovesThem()
        {
            var persistence = MockRepository.GenerateStub<IGTDItemPersistence<ActionItem>>();
            var model = new ActionsListModel(persistence);
            model.Add("foo");
            Assert.That(model.Items, Has.Count(1));
            model.Add("bar");
            Assert.That(model.Items, Has.Count(2));

            model.ClearItems();

            Assert.That(model.Items, Has.Count(0));
        }

        [Test]
        public void ClearItems_DeletesAllWithPersistence()
        {
            var persistence = MockRepository.GenerateMock<IGTDItemPersistence<ActionItem>>();
            var model = new ActionsListModel(persistence);
            model.ClearItems();
            persistence.AssertWasCalled(x => x.DeleteAll());
        }
    }
}
