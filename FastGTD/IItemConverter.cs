using FastGTD.DataTransfer;
using FastGTD.Domain;

namespace FastGTD
{
    public interface IItemConverter
    {
        ActionItem ConvertToAction(InBoxItem item);
        IItemModel<ActionItem> ActionModel { get; }
    }
}