using FastGTD.DataTransfer;
using FastGTD.Domain;
using NUnit.Framework;
using Rhino.Mocks;

namespace FastGTD.UnitTests
{
    [TestFixture]
    public class ActionsListControllerTests
    {
        [Test]
        public void OnCreation_ModelGetsLoaded()
        {
            var model = MockRepository.GenerateMock<IItemModel<ActionItem>>();
            var view = MockRepository.GenerateStub<IItemView<ActionItem>>();
            view.Stub(x => x.List).Return(MockRepository.GenerateStub<IListSelectionChanger>());
            new ActionsListController(view, model);
            model.AssertWasCalled(x => x.Load());
        }
    }
}
