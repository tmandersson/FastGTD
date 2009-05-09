using FastGTD.DataTransfer;

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

            _model.Changed += UpdateFromModel;

            _view.EnterKeyWasPressed += AddInboxItemInTextBox;
            _view.DeleteKeyWasPressed += DeleteSelectedItems;
            _view.DownKeyWasPressed += _view.List.MoveDown;
            _view.UpKeyWasPressed += _view.List.MoveUp;

            _view.KeyPreview = true;
            _view.Resize += delegate { _view.SetFirstColumnFullWidth(); };
            _view.AddButtonWasClicked += AddInboxItemInTextBox;
            _view.DeleteButtonWasClicked += DeleteSelectedItems;
        }

        public void Show()
        {
            _view.Show();
            _view.SetFocusOnTextBox();
            _view.SetFirstColumnFullWidth();
            _model.Load();
        }

        private void UpdateFromModel()
        {
            _view.ClearInBoxItems();
            foreach (InBoxItem item in _model.Items)
            {
                _view.AddInBoxItem(item);
            }
        }

        private void AddInboxItemInTextBox()
        {
            string new_item = _view.TextBoxText;
            _model.CreateItem(new_item);
            _view.TextBoxText = string.Empty;
        }

        private void DeleteSelectedItems()
        {
            foreach (InBoxItem item in _view.SelectedItems)
            {
                _model.RemoveItem(item);
            }
        }

        public void StartMessageLoop()
        {
            _view.StartMessageLoop();
        }

        public void Close()
        {
            _view.Close();
        }
    }
}
