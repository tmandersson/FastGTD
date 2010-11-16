using System.Windows.Automation.Peers;
using System.Windows.Controls;
using White.CustomControls.Peers;

namespace White.CustomControls
{
    public class WhiteTreeView : TreeView
    {
        protected override AutomationPeer OnCreateAutomationPeer()
        {
            return new WhiteTreeViewPeer(this);
        }
    }
}