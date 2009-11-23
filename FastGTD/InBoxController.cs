using FastGTD.DataTransfer;
using FastGTD.Domain;

namespace FastGTD
{
    public class InBoxController : ItemListController<InBoxItem>
    {
        private readonly IInBoxView _inbox_view;
        private readonly IItemConverter _converter;

        public InBoxController(IInBoxView view, 
            IGTDWindow window,
            IItemModel<InBoxItem> model,
            IItemConverter converter) : base(view, window, model)
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
