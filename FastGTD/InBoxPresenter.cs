using System;

namespace FastGTD
{
    public class InBoxPresenter
    {
        private IInboxView _view;
        private IInboxModel _model;

        public InBoxPresenter(IInboxView view, IInboxModel model)
        {
            _view = view;
            _model = model;

            _view.InBoxListFullRowSelect = true;
            _view.SetTextBoxFocus();

            _view.AddItemAction += _model.AddInboxItem;
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