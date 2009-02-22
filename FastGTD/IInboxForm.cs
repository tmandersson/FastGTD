using System.Windows.Forms;

namespace FastGTD
{
    public interface IInBoxForm
    {
        ListView.ListViewItemCollection ListViewItems { get; }
    }

    public enum InBoxFormButton
    {
        Add,
        Delete
    }
}
