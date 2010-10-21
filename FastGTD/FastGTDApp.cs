using FastGTD.DataAccess;
using FastGTD.DataTransfer;
using FastGTD.Domain;
using StructureMap;

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
                x.For<IItemPersistence<InBoxItem>>()
                    .Singleton()
                    .Use<ItemRepository<InBoxItem>>()
                    .Ctor<string>().Is(InBoxItem.Table);
                x.For<IItemPersistence<ActionItem>>()
                    .Singleton()
                    .Use<ItemRepository<ActionItem>>()
                    .Ctor<string>().Is(ActionItem.Table);
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

                x.ForRequestedType<InBoxForm>()
                    .CacheBy(InstanceScope.Singleton);
                x.ForRequestedType<ActionsListForm>()
                    .CacheBy(InstanceScope.Singleton);
                x.ForConcreteType<InBoxController>().Configure
                    .CtorDependency<IGTDWindow>().Is(w => w.ConstructedBy(ObjectFactory.GetInstance<IInBoxView>));
                x.ForConcreteType<ActionsListController>().Configure
                    .CtorDependency<IGTDWindow>().Is(w => w.ConstructedBy(ObjectFactory.GetInstance<IItemView<ActionItem>>));
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