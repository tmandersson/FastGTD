using System.Windows.Forms;

namespace FastGTD
{
    public class ListViewController
    {
        private readonly ListView _listViewInBoxItems;

        public ListViewController(ListView listView)
        {
            _listViewInBoxItems = listView;
        }

        public void ChangeSelection(int step)
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
            ListViewItem last_selected = _listViewInBoxItems.SelectedItems[SelectedItemsCount() - 1];
            return last_selected.Index;
        }

        private void SetSelectedItem(int index)
        {
            _listViewInBoxItems.Focus();
            _listViewInBoxItems.SelectedItems.Clear();
            _listViewInBoxItems.Items[index].Selected = true;
        }

        private int SelectedItemsCount()
        {
            return _listViewInBoxItems.SelectedItems.Count;
        }

        private int ItemsCount()
        {
            return _listViewInBoxItems.Items.Count;
        }
    }
}