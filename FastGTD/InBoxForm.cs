using FastGTD.DataTransfer;

namespace FastGTD
{
    public class InBoxForm : ItemListForm<InBoxItem>, IInBoxView, ITestableInBoxView, IGTDWindow
    {
        protected override void OnLoad(System.EventArgs e)
        {
            Text = "InBox";
            base.OnLoad(e);
        }
    }
}