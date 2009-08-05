using FastGTD.DataTransfer;
using FastGTD.Domain;
using NUnit.Framework;
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
            var persistence = MockRepository.GenerateMock<IActionsListPersistence>();
            var model = new ActionsListModel(persistence);
            
            model.Add(NAME);

            persistence.AssertWasCalled(x => x.Save(Arg<ActionItem>.Matches(i => i.Name == NAME)));
        }
    }
}
