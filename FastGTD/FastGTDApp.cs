using FastGTD.DataAccess;
using FastGTD.DataTransfer;
using FastGTD.Domain;
using StructureMap;
using StructureMap.Attributes;

namespace FastGTD
{
    public static class FastGTDApp
    {
        public static int Main()
        {
            WireClasses();
            IGTDWindow start_form = GetInBox();
            var actions_list = GetActionsList();

            start_form.Show();
            actions_list.Show();

            IGTDWindow main_form = GetMainWindow();
            main_form.Show();
            main_form.StartMessageLoop();

            start_form.Close();
            actions_list.Close();

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
                x.ForRequestedType<IInBoxView>().TheDefaultIsConcreteType<InBoxForm>()
                    .CacheBy(InstanceScope.Singleton);
                x.ForRequestedType<IItemView<ActionItem>>().TheDefaultIsConcreteType<ActionsListForm>()
                    .CacheBy(InstanceScope.Singleton);
                x.ForRequestedType<IItemModel<InBoxItem>>().TheDefaultIsConcreteType<ItemModel<InBoxItem>>()
                    .CacheBy(InstanceScope.Singleton);
                x.ForRequestedType<IItemModel<ActionItem>>().TheDefaultIsConcreteType<ItemModel<ActionItem>>()
                    .CacheBy(InstanceScope.Singleton);
                x.ForRequestedType<IItemConverter>().TheDefaultIsConcreteType<ItemConverter>()
                    .CacheBy(InstanceScope.Singleton);
            });
        }

        private static MainWindow GetMainWindow()
        {
            return ObjectFactory.GetInstance<MainWindow>();
        }

        public static IGTDWindow GetInBox()
        {
            return ObjectFactory.GetInstance<InBoxController>();
        }

        public static IGTDWindow GetActionsList()
        {
            return ObjectFactory.GetInstance<ActionsListController>();
        }
    }
}