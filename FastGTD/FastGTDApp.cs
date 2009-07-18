using FastGTD.DataAccess;
using StructureMap;

namespace FastGTD
{
    public class FastGTDApp
    {
        private readonly InBoxModel _inbox_model;
        private readonly InBoxController _inbox_controller;
        private readonly ActionsListModel _actions_list_model;

        public static int Main()
        {
            FastGTDApp app = Create();
            app.ShowStartForm();
            app.StartMessageLoop();
            app.Close();
            return 0;
        }

        public static FastGTDApp Create()
        {
            ObjectFactory.Initialize(x => {
                x.ForRequestedType<IInBoxItemRepository>().TheDefaultIsConcreteType<InBoxItemRepository>();
                x.ForRequestedType<IInBoxView>().TheDefaultIsConcreteType<InBoxForm>();
                x.ForRequestedType<IInBoxModel>().TheDefaultIsConcreteType<InBoxModel>();
            });
            return ObjectFactory.GetInstance<FastGTDApp>();
        }

        public FastGTDApp(IInBoxModel inbox_model, InBoxController inbox_controller, ActionsListModel actions_list_model)
        {
            // TODO: Fix problem with structuremap signature and cast below.
            _inbox_model = (InBoxModel) inbox_model;
            _inbox_controller = inbox_controller;
            _actions_list_model = actions_list_model;
        }

        public InBoxModel InboxModel
        {
            get { return _inbox_model; }
        }

        public ActionsListModel ActionsListModel
        {
            get { return _actions_list_model; }
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