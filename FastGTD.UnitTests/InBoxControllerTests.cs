using System.Collections.Generic;
using FastGTD.DataTransfer;
using FastGTD.Domain;
using NUnit.Framework;
using Rhino.Mocks;

namespace FastGTD.UnitTests
{
    [TestFixture]
    public class InBoxControllerTests
    {
        private InBoxController _form;

        private IInBoxView _view;
        private IItemConverter _converter;

        [SetUp]
        public void SetupTests()
        {
            _view = MockRepository.GenerateMock<IInBoxView>();
            _view.Stub(x => x.List).Return(MockRepository.GenerateMock<IListSelectionChanger>());
            var model = MockRepository.GenerateStub<IItemModel<InBoxItem>>();
            _converter = MockRepository.GenerateMock<IItemConverter>();
            _form = new InBoxController(_view, model, _converter);
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

            _converter.AssertWasCalled(x => x.ConvertToAction(Arg<InBoxItem>.Is.Equal(expected_item)));
            _converter.AssertWasCalled(x => x.ConvertToAction(Arg<InBoxItem>.Is.Equal(expected_item2)));
        }

        [Test]
        public void PressingAltAConvertsSelectedItemsToActions()
        {
            var expected_item = new InBoxItem("foo");
            var expected_item2 = new InBoxItem("foo2");
            _view.Stub(x => x.SelectedItems).Return(new List<InBoxItem> { expected_item, expected_item2 });

            _view.Raise(x => x.AltAKeysWasPressed += null);

            _converter.AssertWasCalled(x => x.ConvertToAction(Arg<InBoxItem>.Is.Equal(expected_item)));
            _converter.AssertWasCalled(x => x.ConvertToAction(Arg<InBoxItem>.Is.Equal(expected_item2)));
        }
    }
}