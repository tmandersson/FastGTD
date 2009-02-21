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
            var form = new InBoxController(view);

            form.Show();

            view.AssertWasCalled(x => x.SetFocusOnTextBox());
        }

    }
}
