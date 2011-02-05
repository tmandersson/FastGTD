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
            IItemConverter converter,
            IPublishKeyEvents key_events)
            : base(view, window, model, key_events)
        {
            _inbox_view = view;
            _converter = converter;
            InBoxModel = model;
            if (_converter != null)
                ActionModel = _converter.ActionModel;
            HandleEvents();
        }

        public IItemModel<InBoxItem> InBoxModel { get; private set; }
        public IItemModel<ActionItem> ActionModel { get; private set; }
        public IInBoxView InBoxView { get { return _inbox_view; } }

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
