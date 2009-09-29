using FastGTD.DataTransfer;
using FastGTD.Domain;

namespace FastGTD
{
    public class InBoxController : ItemListController<InBoxItem>, IGTDWindow
    {
        private readonly IInBoxView _inbox_view;
        private readonly IItemConverter _converter;

        public InBoxController(IInBoxView view, 
            IItemModel<InBoxItem> model, 
            IItemConverter converter) : base(view, model)
        {
            _inbox_view = view;
            _converter = converter;
            HandleEvents();
        }

        private void HandleEvents()
        {
            _inbox_view.ToActionButtonWasClicked += ConvertSelectedItemToAction;
            _inbox_view.AltAKeysWasPressed += ConvertSelectedItemToAction;
        }

        private void ConvertSelectedItemToAction()
        {
            ForEachSelectedItem(item => _converter.ConvertToAction(item));
        }
    }
}
