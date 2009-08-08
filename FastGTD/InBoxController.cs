using System;
using FastGTD.DataTransfer;
using FastGTD.Domain;

namespace FastGTD
{
    public class InBoxController
    {
        private readonly IInBoxView _view;
        private readonly IItemModel<InBoxItem> _model;
        private readonly IItemConverter _converter;

        public InBoxController(IInBoxView view, IItemModel<InBoxItem> model, IItemConverter converter)
        {
            _view = view;
            _model = model;
            _converter = converter;
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
            HandleViewEvents();
        }

        private void HandleViewEvents()
        {
            _view.EnterKeyWasPressed += AddInboxItemInTextBox;
            _view.AddButtonWasClicked += AddInboxItemInTextBox;
            _view.DeleteKeyWasPressed += DeleteSelectedItems;
            _view.DeleteButtonWasClicked += DeleteSelectedItems;
            _view.ToActionButtonWasClicked += ConvertSelectedItemToAction;
            _view.AltAKeysWasPressed += ConvertSelectedItemToAction;
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

        private void ConvertSelectedItemToAction()
        {
            ForEachSelectedItem(item => _converter.ConvertToAction(item));
        }

        private void ForEachSelectedItem(Action<InBoxItem> action)
        {
            foreach (InBoxItem item in _view.SelectedItems)
            {
                action(item);
            }
        }

        private void UpdateFromModel()
        {
            _view.ClearInBoxItems();
            foreach (InBoxItem item in _model.Items)
            {
                _view.AddInBoxItem(item);
            }
        }
    }
}
