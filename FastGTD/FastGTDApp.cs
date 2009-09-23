using FastGTD.DataAccess;
using FastGTD.DataTransfer;
using FastGTD.Domain;
using StructureMap;
using StructureMap.Attributes;

namespace FastGTD
{
    public class FastGTDApp
    {
        private readonly ItemModel<InBoxItem> _inbox_model;
        private readonly InBoxController _inbox_controller;
        private readonly IItemModel<ActionItem> _actions_list_model;

        public static int Main()
        {
            WireClasses();
            var app = new FastGTDApp();
            app.ShowStartForm();
            app.StartMessageLoop();
            app.Close();
            return 0;
        }

        public static void WireClasses()
        {
            ObjectFactory.Initialize(x =>
            {
                x.ForRequestedType<IItemPersistence<InBoxItem>>()
                    .CacheBy(InstanceScope.Singleton)
                    .TheDefault.Is.OfConcreteType<ItemRepository<InBoxItem>>()
                    .WithCtorArg("table").EqualTo(InBoxItem.Table);
                x.ForRequestedType<IItemPersistence<ActionItem>>()
                    .CacheBy(InstanceScope.Singleton)
                    .TheDefault.Is.OfConcreteType<ItemRepository<ActionItem>>()
                    .WithCtorArg("table").EqualTo(ActionItem.Table);
                x.ForRequestedType<IInBoxView>().TheDefaultIsConcreteType<NewInBoxForm>()
                    .CacheBy(InstanceScope.Singleton);
                x.ForRequestedType<IItemView<ActionItem>>().TheDefaultIsConcreteType<ActionListForm>()
                    .CacheBy(InstanceScope.Singleton);
                x.ForRequestedType<IItemModel<InBoxItem>>().TheDefaultIsConcreteType<ItemModel<InBoxItem>>()
                    .CacheBy(InstanceScope.Singleton);
                x.ForRequestedType<IItemModel<ActionItem>>().TheDefaultIsConcreteType<ItemModel<ActionItem>>()
                    .CacheBy(InstanceScope.Singleton);
                x.ForRequestedType<IItemConverter>().TheDefaultIsConcreteType<ItemConverter>()
                    .CacheBy(InstanceScope.Singleton);
            });
        }

        public FastGTDApp()
        {
            _inbox_model = (ItemModel<InBoxItem>)ObjectFactory.GetInstance<IItemModel<InBoxItem>>();
            _inbox_controller = ObjectFactory.GetInstance<InBoxController>();
            _actions_list_model = ObjectFactory.GetInstance<IItemModel<ActionItem>>();
            ObjectFactory.GetInstance<ActionsListController>();
            ObjectFactory.GetInstance<IItemConverter>();
        }

        public ItemModel<InBoxItem> InboxModel
        {
            get { return _inbox_model; }
        }

        public IItemModel<ActionItem> ActionsListModel
        {
            get { return _actions_list_model; }
        }

        private InBoxController InboxController
        {
            get { return _inbox_controller; }
        }

        public void ShowStartForm()
        {
            InboxController.Show();
        }

        private void StartMessageLoop()
        {
            InboxController.StartMessageLoop();
        }

        public void Close()
        {
            InboxController.Close();
        }
    }
}