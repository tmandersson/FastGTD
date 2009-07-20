using FastGTD.DataAccess;
using StructureMap;
using StructureMap.Attributes;

namespace FastGTD
{
    public class FastGTDApp
    {
        private readonly InBoxModel _inbox_model;
        private readonly InBoxController _inbox_controller;
        private readonly ActionsListModel _actions_list_model;
        private readonly IItemConverter _converter;

        public static int Main()
        {
            var app = new FastGTDApp();
            app.ShowStartForm();
            app.StartMessageLoop();
            app.Close();
            return 0;
        }

        public FastGTDApp()
        {
            ObjectFactory.Initialize(x =>
            {
                x.ForRequestedType<IInBoxPersistenceProvider>().TheDefaultIsConcreteType<InBoxItemRepository>()
                    .CacheBy(InstanceScope.Singleton);
                x.ForRequestedType<IInBoxView>().TheDefaultIsConcreteType<InBoxForm>()
                    .CacheBy(InstanceScope.Singleton);
                x.ForRequestedType<IInBoxModel>().TheDefaultIsConcreteType<InBoxModel>()
                    .CacheBy(InstanceScope.Singleton);
                x.ForRequestedType<ActionsListModel>().TheDefaultIsConcreteType<ActionsListModel>()
                    .CacheBy(InstanceScope.Singleton);
                x.ForRequestedType<IItemConverter>().TheDefaultIsConcreteType<ItemConverter>()
                    .CacheBy(InstanceScope.Singleton);
            });

            _inbox_model = (InBoxModel) ObjectFactory.GetInstance<IInBoxModel>();
            _inbox_controller = ObjectFactory.GetInstance<InBoxController>();
            _actions_list_model = ObjectFactory.GetInstance<ActionsListModel>();
            _converter = ObjectFactory.GetInstance<IItemConverter>();
        }

        public InBoxModel InboxModel
        {
            get { return _inbox_model; }
        }

        public ActionsListModel ActionsListModel
        {
            get { return _actions_list_model; }
        }

        public IItemConverter Converter
        {
            get { return _converter; }
        }

        public void ShowStartForm()
        {
            _inbox_controller.Show();
        }

        private void StartMessageLoop()
        {
            _inbox_controller.StartMessageLoop();
        }

        public void Close()
        {
            _inbox_controller.Close();
        }
    }
}