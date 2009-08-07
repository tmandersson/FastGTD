using FastGTD.DataAccess;
using FastGTD.DataTransfer;
using FastGTD.Domain;
using StructureMap;
using StructureMap.Attributes;

namespace FastGTD
{
    public class FastGTDApp
    {
        private readonly InBoxModel _inbox_model;
        private readonly InBoxController _inbox_controller;
        private readonly IGTDItemModel<ActionItem> _actions_list_model;

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
                x.ForRequestedType<IInBoxPersistence>().TheDefaultIsConcreteType<InBoxItemRepository>()
                    .CacheBy(InstanceScope.Singleton);
                x.ForRequestedType<IInBoxView>().TheDefaultIsConcreteType<InBoxForm>()
                    .CacheBy(InstanceScope.Singleton);
                x.ForRequestedType<IGTDItemModel<InBoxItem>>().TheDefaultIsConcreteType<InBoxModel>()
                    .CacheBy(InstanceScope.Singleton);
                x.ForRequestedType<IGTDItemModel<ActionItem>>().TheDefaultIsConcreteType<ActionsListModel>()
                    .CacheBy(InstanceScope.Singleton);
                x.ForRequestedType<IItemConverter>().TheDefaultIsConcreteType<ItemConverter>()
                    .CacheBy(InstanceScope.Singleton);
                x.ForRequestedType<IActionsListPersistence>().TheDefaultIsConcreteType<ActionsRepository>()
                    .CacheBy(InstanceScope.Singleton);
            });
        }

        public FastGTDApp()
        {
            _inbox_model = (InBoxModel)ObjectFactory.GetInstance<IGTDItemModel<InBoxItem>>();
            _inbox_controller = ObjectFactory.GetInstance<InBoxController>();
            _actions_list_model = ObjectFactory.GetInstance<IGTDItemModel<ActionItem>>();
            ObjectFactory.GetInstance<ActionsListController>();
            ObjectFactory.GetInstance<IItemConverter>();
        }

        public InBoxModel InboxModel
        {
            get { return _inbox_model; }
        }

        public IGTDItemModel<ActionItem> ActionsListModel
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