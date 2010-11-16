using System.Windows.Automation.Peers;
using System.Windows.Controls;
using White.CustomControls.Peers;

namespace White.CustomControls
{
    public class WhiteHyperlinkButton : HyperlinkButton
    {
        protected override AutomationPeer OnCreateAutomationPeer()
        {
            return new WhiteHyperlinkButtonPeer(this);
        }
    }
}