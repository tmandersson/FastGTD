﻿using System;
using System.Windows.Forms;

namespace FastGTD
{
    public partial class InBoxForm : Form
    {
        public InBoxForm()
        {
            InitializeComponent();

            KeyDown += KeyDownHandler;
            KeyPreview = true;
            Resize += SizeHandler;
            Shown += SizeHandler;
        }

        private void SizeHandler(object sender, EventArgs e)
        {
            SetFirstColumnFullWidth();
        }

        private void SetFirstColumnFullWidth()
        {
            listViewInBoxItems.Columns[0].Width = listViewInBoxItems.Width - 5;
        }

        private void KeyDownHandler(object sender, KeyEventArgs e)
        {
            bool key_handled = true;
            if (e.KeyData == Keys.Enter)
                AddInboxItemInTextBox();
            else if (e.KeyData == Keys.Delete)
                DeleteSelectedItem();
            else if (e.KeyData == Keys.Down)
                ChangeSelection(1);
            else if (e.KeyData == Keys.Up)
                ChangeSelection(-1);
            else
                key_handled = false;

            if (key_handled)
                e.SuppressKeyPress = true;
        }

        private void ChangeSelection(int step)
        {
            int count_selected = listViewInBoxItems.SelectedItems.Count;
            int next_index;
            if (count_selected == 0)
                next_index = 0;
            else
                next_index = listViewInBoxItems.SelectedItems[count_selected - 1].Index + step;

            listViewInBoxItems.SelectedItems.Clear();
            listViewInBoxItems.Items[next_index].Selected = true;
        }

        private void buttonAdd_Click(object sender, System.EventArgs e)
        {
            AddInboxItemInTextBox();
        }

        private void buttonDelete_Click(object sender, EventArgs e)
        {
            DeleteSelectedItem();
        }

        public void AddInboxItemInTextBox()
        {
            string new_item = textBox.Text;
            AddInboxItem(new_item);
        }

        public void AddInboxItem(string new_item)
        {
            listViewInBoxItems.Items.Add(new_item);
            textBox.Text = string.Empty;
        }

        public void PerformKeyDown(Keys key)
        {
            KeyDownHandler(this, new KeyEventArgs(key));
        }

        public void DeleteSelectedItem()
        {
            foreach(ListViewItem item in listViewInBoxItems.SelectedItems)
            {
                item.Remove();
            }
        }
    }
}
