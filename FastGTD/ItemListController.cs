using System;
using FastGTD.Domain;

namespace FastGTD
{
    public abstract class ItemListController<T> : IGTDWindow where T : GTDItem
    {
        private readonly IItemView _view;
        private readonly IGTDWindow _window;
        private readonly IItemModel<T> _model;
        private readonly IPublishKeyEvents _key_events;

        protected ItemListController(IItemView view, IGTDWindow window, IItemModel<T> model, IPublishKeyEvents key_events)
        {
            _view = view;
            _window = window;
            _model = model;
            _key_events = key_events;
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
            _key_events.EnterKeyWasPressed += AddInboxItemInTextBox;
            _key_events.AddButtonWasClicked += AddInboxItemInTextBox;
            _key_events.DeleteKeyWasPressed += DeleteSelectedItems;
            _key_events.DeleteButtonWasClicked += DeleteSelectedItems;
            _key_events.DownKeyWasPressed += _view.List.MoveDown;
            _key_events.UpKeyWasPressed += _view.List.MoveUp;
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