using FastGTD.DataTransfer;

namespace FastGTD
{
    public class InBoxForm : ItemListForm, IInBoxView, ITestableInBoxView, IGTDWindow
    {
        protected override void OnLoad(System.EventArgs e)
        {
            Text = "InBox";
            base.OnLoad(e);
        }
    }
}