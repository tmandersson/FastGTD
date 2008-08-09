using System;

namespace FastGTD
{
    public class InBoxPresenter
    {
        private IInboxView _view;

        public InBoxPresenter(IInboxView view, IInboxModel model)
        {
            _view = view;
            _view.FullRowSelect = true;
        }

        public IInboxView View
        {
            get { return _view; }
        }

        public void Show()
        {
            _view.Show();
        }
    }
}