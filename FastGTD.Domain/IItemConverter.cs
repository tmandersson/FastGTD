using FastGTD.DataTransfer;

namespace FastGTD
{
    public interface IItemConverter
    {
        ActionItem ConvertToAction(InBoxItem item);
    }
}