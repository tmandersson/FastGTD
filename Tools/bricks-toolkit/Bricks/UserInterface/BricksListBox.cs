using System;
using System.Windows.Forms;

namespace Lunar.Client.View.CommonControls
{
    public class BricksListBox : ListBox
    {
        private int selectedIndex;

        protected override void OnSelectedIndexChanged(EventArgs e)
        {
            using (CursorManager.WaitCursor(this))
            {
                if (selectedIndex.Equals(SelectedIndex)) return;
                base.OnSelectedIndexChanged(e);
                selectedIndex = SelectedIndex;
            }
        }
    }
}