namespace FastGTD
{
    public class ActionsListForm : ItemListForm, IItemView, IGTDWindow
    {

        protected override void OnLoad(System.EventArgs e)
        {
            Text = "Actions";
            _item_list.HideToActionButton();
            base.OnLoad(e);
        }
    }
}