using System.Windows.Automation.Peers;
using System.Windows.Controls;
using White.CustomControls.Peers;

namespace White.CustomControls
{
    public class WhiteMediaElement : MediaElement
    {
        protected override AutomationPeer OnCreateAutomationPeer()
        {
            return new WhiteMediaElementPeer(this);
        }
    }
}