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
            var model = MockRepository.GenerateMock<IActionsListModel>();
            new ActionsListController(model);
            model.AssertWasCalled(x => x.Load());
        }
    }
}
