using FastGTD.DataTransfer;

namespace FastGTD
{
    public class ActionsListForm : ItemListForm<ActionItem>, IItemView<ActionItem>
    {

        protected override void OnLoad(System.EventArgs e)
        {
            Text = "Actions";
            _to_action_button.Visible = false;
            base.OnLoad(e);
        }
    }
}