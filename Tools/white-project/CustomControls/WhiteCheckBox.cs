using System.Windows.Automation.Peers;
using System.Windows.Controls;
using White.CustomControls.Peers;

namespace White.CustomControls
{
    public class WhiteCheckBox : CheckBox
    {
        protected override AutomationPeer OnCreateAutomationPeer()
        {
            return new WhiteCheckBoxPeer(this);
        }
    }
}