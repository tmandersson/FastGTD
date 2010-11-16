using System.Windows.Automation.Peers;
using System.Windows.Controls;
using White.CustomControls.Peers;

namespace White.CustomControls
{
    public class WhiteScrollViewer : ScrollViewer
    {
        protected override AutomationPeer OnCreateAutomationPeer()
        {
            return new WhiteScrollViewerPeer(this);
        }
    }
}