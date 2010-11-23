using System;
using System.Windows.Forms;

namespace Lunar.Client.View.CommonControls
{
    public class BricksButton : Button
    {
        protected override void OnClick(EventArgs e)
        {
            using (CursorManager.WaitCursor(this))
                base.OnClick(e);
        }
    }
}