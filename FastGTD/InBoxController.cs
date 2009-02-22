using System.Windows.Forms;

namespace FastGTD
{
    public class InBoxController
    {
        private readonly IInBoxView _view;
        private readonly IInBoxModel _model;

        public InBoxController(IInBoxView view, IInBoxModel model)
        {
            _view = view;
            _model = model;
        }

        public void Show()
        {
            _view.Show();
            _view.SetFocusOnTextBox();
            _model.Load();
        }
    }
}
