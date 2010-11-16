using System.Windows.Automation.Peers;
using System.Windows.Controls;
using White.CustomControls.Peers;

namespace White.CustomControls
{
    public class WhiteToolBar : ToolBar
    {
        protected override AutomationPeer OnCreateAutomationPeer()
        {
            return new WhiteToolBarPeer(this);
        }
    }
}