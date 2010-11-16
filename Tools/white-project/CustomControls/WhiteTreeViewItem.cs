using System.Windows.Automation.Peers;
using System.Windows.Controls;
using White.CustomControls.Peers;

namespace White.CustomControls
{
    public class WhiteTreeViewItem : TreeViewItem
    {
        protected override AutomationPeer OnCreateAutomationPeer()
        {
            return new WhiteTreeViewItemPeer(this);
        }
    }
}