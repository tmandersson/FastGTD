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
            var view = MockRepository.GenerateStub<IItemView>();
            var window = MockRepository.GenerateStub<IGTDWindow>();
            view.Stub(x => x.List).Return(MockRepository.GenerateStub<IListSelectionChanger>());

            CreateActionListController(view, window, model);

            model.AssertWasCalled(x => x.Load());
        }

        private static void CreateActionListController(IItemView view, IGTDWindow window, IItemModel<ActionItem> model)
        {
            new ActionsListController(view, window, model);
        }
    }
}
