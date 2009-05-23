using NUnit.Framework;
using Rhino.Mocks;

namespace FastGTD.Tests
{
    [TestFixture]
    public class InBoxControllerTests
    {
        [Test]
        public void TextBoxStartsWithFocus()
        {
            var view = MockRepository.GenerateMock<IInBoxView>();
            view.Stub(x => x.List).Return(MockRepository.GenerateMock<IListSelectionChanger>());
            var model = MockRepository.GenerateStub<IInBoxModel>();
            var form = new InBoxController(view, model);

            form.Show();

            view.AssertWasCalled(x => x.SetFocusOnTextBox());
        }

        [Test]
        public void xxx()
        {
            const string ITEM_NAME = "foo";
            var repo = MockRepository.GenerateMock<IActionPersister>();

            repo.AssertWasCalled(x => x.Save(Arg<Action>.Matches(a => a.Name == ITEM_NAME)));
        }
    }

    public interface IActionPersister
    {
        void Save(Action action);
    }

    public class Action
    {
        public string Name { get; set; }
    }
}




