using System;
using System.Collections.Generic;
using System.Windows.Forms;
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

            _view.KeyDown += KeyDownHandler;
            _view.KeyPreview = true;
            _view.Resize += delegate { _view.SetFirstColumnFullWidth(); };
            _view.AddButtonClick += delegate { AddInboxItemInTextBox(); };
            _view.DeleteButtonClick += delegate { DeleteSelectedItems(); };
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

        private void KeyDownHandler(object sender, KeyEventArgs e)
        {
            bool key_handled = true;
            if (e.KeyData == Keys.Enter)
                AddInboxItemInTextBox();
            else if (e.KeyData == Keys.Delete)
                DeleteSelectedItems();
            else if (e.KeyData == Keys.Down)
                _view.List.MoveDown();
            else if (e.KeyData == Keys.Up)
                _view.List.MoveUp();
            else
                key_handled = false;

            if (key_handled)
                e.SuppressKeyPress = true;
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
