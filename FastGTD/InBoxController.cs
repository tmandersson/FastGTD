namespace FastGTD
{
    public class InBoxController
    {
        private readonly IInBoxView _view;

        public InBoxController(IInBoxView view)
        {
            _view = view;
        }

        public void Show()
        {
            _view.SetFocusOnTextBox();
        }
    }
}
