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
            var model = MockRepository.GenerateMock<IGTDItemModel<ActionItem>>();
            new ActionsListController(model);
            model.AssertWasCalled(x => x.Load());
        }
    }
}
