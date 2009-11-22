using FastGTD.DataTransfer;

namespace FastGTD
{
    public class ActionsListForm : ItemListForm<ActionItem>, IItemView<ActionItem>
    {

        protected override void OnLoad(System.EventArgs e)
        {
            Text = "Actions";
            _item_list.HideToActionButton();
            base.OnLoad(e);
        }
    }
}