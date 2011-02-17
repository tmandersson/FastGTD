using FastGTD.DataAccess;
using FastGTD.DataTransfer;
using FastGTD.Domain;
using StructureMap;
using StructureMap.Configuration.DSL;

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
                x.AddRegistry(new AutoWireRegistry());

                x.For<IItemPersistence<InBoxItem>>()
                    .Singleton()
                    .Use<ItemRepository<InBoxItem>>();
                x.For<IItemPersistence<ActionItem>>()
                    .Singleton()
                    .Use<ItemRepository<ActionItem>>();
                x.For<IInBoxView>()
                    .Singleton()
                    .Use<InBoxForm>();
                x.For<IItemView>()
                    .Singleton()
                    .Use<ActionsListForm>();
                x.For<IItemModel<InBoxItem>>()
                    .Singleton()
                    .Use<ItemModel<InBoxItem>>();
                x.For<IItemModel<ActionItem>>()
                    .Singleton()
                    .Use<ItemModel<ActionItem>>();
            });
        }

        private static MainWindow GetMainWindow()
        {
            var inbox_view = new ItemListControl();
            var actions_view = new ItemListControl();
            var window = new MainWindow(inbox_view, actions_view);
            
            var inbox_model = ObjectFactory.GetInstance<IItemModel<InBoxItem>>();
            var action_model = ObjectFactory.GetInstance<IItemModel<ActionItem>>();
            var converter = new ItemConverter(inbox_model, action_model);
            new InBoxController(window.InBoxControls, window, inbox_model, converter, window);
            new ActionsListController(window.ActionControls, window, action_model, window);

            new MainWindowController(window, inbox_view, actions_view);

            return window;
        }

        public static IGTDWindow GetInBox()
        {
            var view = new InBoxForm();
            var model = ObjectFactory.GetInstance<IItemModel<InBoxItem>>();
            var action_model = ObjectFactory.GetInstance<IItemModel<ActionItem>>();
            var converter = new ItemConverter(model, action_model);
            return new InBoxController(view, view, model, converter, view);
        }

        public static IGTDWindow GetActionsList()
        {
            var view = new ActionsListForm();
            var model = ObjectFactory.GetInstance<IItemModel<ActionItem>>();
            return new ActionsListController(view, view, model, view);
        }
    }

    public class AutoWireRegistry : Registry
    {
        public AutoWireRegistry()
        {
            Scan(x => { 
                x.TheCallingAssembly();
                x.SingleImplementationsOfInterface();
            });
        }
    }
}