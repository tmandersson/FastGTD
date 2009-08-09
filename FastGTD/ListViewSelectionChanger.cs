using System.Windows.Forms;

namespace FastGTD
{
    public class ListViewSelectionChanger : IListSelectionChanger
    {
        private readonly ListView _list_view;

        public ListViewSelectionChanger(ListView list_view)
        {
            _list_view = list_view;
        }

        public void MoveDown()
        {
            ChangeSelection(1);
        }

        public void MoveUp()
        {
            ChangeSelection(-1);
        }

        private void ChangeSelection(int step)
        {
            if (ItemsCount() == 0)
                return;

            SetSelectedItem(GetIndexToSelect(step));
        }

        private int GetIndexToSelect(int step)
        {
            if (NoItemSelected() || TryingToStepBeforeFirstItem(step))
                return FirstIndex();

            if (TryingToStepAfterLastItem(step))
                return LastIndex();

            return GetNextIndex(step);
        }

        private bool TryingToStepBeforeFirstItem(int step)
        {
            return GetNextIndex(step) == -1;
        }

        private bool TryingToStepAfterLastItem(int step)
        {
            return GetNextIndex(step) == ItemsCount();
        }

        private bool NoItemSelected()
        {
            return SelectedItemsCount() == 0;
        }

        private static int FirstIndex()
        {
            return 0;
        }

        private int LastIndex()
        {
            return ItemsCount() - 1;
        }

        private int GetNextIndex(int step)
        {
            return GetCurrentlySelectedIndex() + step;
        }

        private int GetCurrentlySelectedIndex()
        {
            ListViewItem last_selected = _list_view.SelectedItems[SelectedItemsCount() - 1];
            return last_selected.Index;
        }

        private void SetSelectedItem(int index)
        {
            _list_view.Focus();
            _list_view.SelectedItems.Clear();
            _list_view.Items[index].Selected = true;
        }

        private int SelectedItemsCount()
        {
            return _list_view.SelectedItems.Count;
        }

        private int ItemsCount()
        {
            return _list_view.Items.Count;
        }
    }
}