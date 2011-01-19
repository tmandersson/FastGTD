using System;
using NUnit.Framework;
using Rhino.Mocks;

namespace FastGTD.UnitTests
{
    [TestFixture]
    public class MainWindowControllerTests
    {
        private IMainWindowView _main_view;
        private IInBoxView _inbox_view;
        private IItemView _action_view;

        [SetUp]
        public void WireUpController()
        {
            _main_view = MockRepository.GenerateMock<IMainWindowView>();
            _inbox_view = MockRepository.GenerateMock<IInBoxView>();
            _action_view = MockRepository.GenerateMock<IItemView>();
            new MainWindowController(_main_view, _inbox_view, _action_view);
        }

        [Test]
        public void ShowingMainWindowSetsFocusOnTextBox()
        {
            _main_view.Raise(x => x.Shown += null, this, EventArgs.Empty);
            _inbox_view.AssertWasCalled(x => x.SetFocusOnTextBox());
        }

        [Test]
        public void GivingWindowFocusSetsFocusOnTextBox()
        {
            _main_view.Raise(x => x.GotFocus += null, this, EventArgs.Empty);
            _inbox_view.AssertWasCalled(x => x.SetFocusOnTextBox());
        }

        [Test]
        public void OtherTabIsSelectedSetsFocusOnCorrectTextBox()
        {
            _main_view.Stub(x => x.SelectedTabIndex).Return(1);
            _main_view.Raise(x => x.Shown += null, this, EventArgs.Empty);
            _action_view.AssertWasCalled(x => x.SetFocusOnTextBox());
        }

        [Test]
        public void SelectingTabSetsFocusOnCorrectTextBox()
        {
            _main_view.Stub(x => x.SelectedTabIndex).Return(1);
            _main_view.Raise(x => x.SelectedTabChanged += null, this, EventArgs.Empty);
            _action_view.AssertWasCalled(x => x.SetFocusOnTextBox());
        }
    }
}
