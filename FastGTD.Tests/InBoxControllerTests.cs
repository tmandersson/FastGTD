using System.Collections.Generic;
using FastGTD.DataTransfer;
using NUnit.Framework;
using Rhino.Mocks;

namespace FastGTD.Tests
{
    [TestFixture]
    public class InBoxControllerTests
    {
        private InBoxController _form;

        private IInBoxView _view;
        private IInBoxModel _model;

        [SetUp]
        public void SetupTests()
        {
            _view = MockRepository.GenerateMock<IInBoxView>();
            _view.Stub(x => x.List).Return(MockRepository.GenerateMock<IListSelectionChanger>());
            _model = MockRepository.GenerateMock<IInBoxModel>();
            _form = new InBoxController(_view, _model);
        }

        [Test]
        public void TextBoxStartsWithFocus()
        {
            _form.Show();
            _view.AssertWasCalled(x => x.SetFocusOnTextBox());
        }

        [Test]
        public void ActionButtonClickConvertsSelectedItemsToAction()
        {
            var expected_item = new InBoxItem("foo");
            var expected_item2 = new InBoxItem("foo2");
            _view.Stub(x => x.SelectedItems).Return(new List<InBoxItem> { expected_item, expected_item2 });

            _view.Raise(x => x.ToActionButtonWasClicked += null);

            _model.AssertWasCalled(x => x.ConvertToAction(Arg<InBoxItem>.Is.Equal(expected_item)));
            _model.AssertWasCalled(x => x.ConvertToAction(Arg<InBoxItem>.Is.Equal(expected_item2)));
        }

        [Test]
        public void PressingAltAConvertsSelectedItemsToActions()
        {
            var expected_item = new InBoxItem("foo");
            var expected_item2 = new InBoxItem("foo2");
            _view.Stub(x => x.SelectedItems).Return(new List<InBoxItem> { expected_item, expected_item2 });

            _view.Raise(x => x.AltAKeysWasPressed += null);

            _model.AssertWasCalled(x => x.ConvertToAction(Arg<InBoxItem>.Is.Equal(expected_item)));
            _model.AssertWasCalled(x => x.ConvertToAction(Arg<InBoxItem>.Is.Equal(expected_item2)));
        }
    }
}
