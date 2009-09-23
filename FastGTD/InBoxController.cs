using System;
using FastGTD.DataTransfer;
using FastGTD.Domain;

namespace FastGTD
{
    public abstract class ItemListController<T> where T : GTDItem
    {
        private readonly IItemView<InBoxItem> _view;
        private readonly IItemModel<InBoxItem> _model;

        protected ItemListController(IItemView<InBoxItem> view, IItemModel<InBoxItem> model)
        {
            _view = view;
            _model = model;
            HandleEvents();
            _model.Load();
        }

        public void Show()
        {
            _view.Show();
            _view.SetFocusOnTextBox();
        }

        public void Close()
        {
            _view.Close();
        }

        public void StartMessageLoop()
        {
            _view.StartMessageLoop();
        }

        private void HandleEvents()
        {
            _model.Changed += UpdateFromModel;
            _view.EnterKeyWasPressed += AddInboxItemInTextBox;
            _view.AddButtonWasClicked += AddInboxItemInTextBox;
            _view.DeleteKeyWasPressed += DeleteSelectedItems;
            _view.DeleteButtonWasClicked += DeleteSelectedItems;
            _view.DownKeyWasPressed += _view.List.MoveDown;
            _view.UpKeyWasPressed += _view.List.MoveUp;
        }

        private void AddInboxItemInTextBox()
        {
            string new_item = _view.TextBoxText;
            _model.Add(new_item);
            _view.TextBoxText = string.Empty;
        }

        private void DeleteSelectedItems()
        {
            ForEachSelectedItem(item => _model.Remove(item));
        }

        protected void ForEachSelectedItem(Action<InBoxItem> action)
        {
            foreach (InBoxItem item in _view.SelectedItems)
            {
                action(item);
            }
        }

        protected void UpdateFromModel()
        {
            _view.ClearItems();
            foreach (InBoxItem item in _model.Items)
            {
                _view.AddItem(item);
            }
        }
    }

    public class InBoxController : ItemListController<InBoxItem>
    {
        private readonly IInBoxView _inbox_view;
        private readonly IItemConverter _converter;

        public InBoxController(IInBoxView view, 
            IItemModel<InBoxItem> model, 
            IItemConverter converter) : base(view, model)
        {
            _inbox_view = view;
            _converter = converter;
            HandleEvents();
        }

        private void HandleEvents()
        {
            _inbox_view.ToActionButtonWasClicked += ConvertSelectedItemToAction;
            _inbox_view.AltAKeysWasPressed += ConvertSelectedItemToAction;
        }

        private void ConvertSelectedItemToAction()
        {
            ForEachSelectedItem(item => _converter.ConvertToAction(item));
        }
    }
}
