using System;
using FastGTD.Domain;

namespace FastGTD
{
    public abstract class ItemListController<T> : IGTDWindow where T : GTDItem
    {
        private readonly IItemView<T> _view;
        private readonly IGTDWindow _window;
        private readonly IItemModel<T> _model;

        protected ItemListController(IItemView<T> view, IGTDWindow window, IItemModel<T> model)
        {
            _view = view;
            _window = window;
            _model = model;
            HandleEvents();
            _model.Load();
        }

        public void Show()
        {
            _window.Show();
            _view.SetFocusOnTextBox();
        }

        public void Close()
        {
            _window.Close();
        }

        public void StartMessageLoop()
        {
            _window.StartMessageLoop();
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

        protected void ForEachSelectedItem(Action<T> action)
        {
            foreach (T item in _view.SelectedItems)
            {
                action(item);
            }
        }

        private void UpdateFromModel()
        {
            _view.ClearItems();
            foreach (T item in _model.Items)
            {
                _view.AddItem(item);
            }
        }
    }
}