using System.Windows.Automation.Peers;
using System.Windows.Controls;
using White.CustomControls.Peers;

namespace White.CustomControls
{
    public class WhiteListBox : ListBox
    {
        protected override AutomationPeer OnCreateAutomationPeer()
        {
            return new WhiteListBoxPeer(this);
        }
    }
}