namespace FastGTD
{
    public class InBoxPresenter
    {
        public InBoxPresenter(IInboxView view, IInboxModel model)
        {
            view.FullRowSelect = true;
        }
    }
}