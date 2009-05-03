using FastGTD.DataAccess;
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

    }
}
