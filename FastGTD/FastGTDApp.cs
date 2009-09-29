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
            InBoxController start_form = GetStartForm();
            start_form.Show();
            start_form.StartMessageLoop();
            start_form.Close();
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

        public static InBoxController GetStartForm()
        {
            return ObjectFactory.GetInstance<InBoxController>();
        }
    }
}