using System;

namespace FastGTD
{
    public class InBoxPresenter
    {
        private IInBoxView _view;
        private IInBoxModel _model;

        public InBoxPresenter(IInBoxView view, IInBoxModel model)
        {
            _view = view;
            _model = model;

            _view.InBoxListFullRowSelect = true;
            _view.SetTextBoxFocus();

            _view.AddItemAction += _model.AddInboxItem;
            _model.ContentUpdated += UpdateInBoxList;
        }

        private void UpdateInBoxList()
        {
            _view.InBoxList = _model.InboxItems;
        }

        public IInBoxView View
        {
            get { return _view; }
        }

        public void Show()
        {
            _view.Show();
        }
    }
}