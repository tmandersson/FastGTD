using FastGTD.DataAccess;

namespace FastGTD
{
    public class FastGTDApp
    {
        private readonly InBoxModel _inbox_model;
        private readonly IInBoxView _inbox_view;
        private readonly InBoxController _inbox_controller;

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
            _inbox_model = new InBoxModel(new InBoxItemRepository());
            _inbox_view = new InBoxForm();
            _inbox_controller = new InBoxController(_inbox_view, _inbox_model);
        }

        public InBoxModel InboxModel
        {
            get { return _inbox_model; }
        }

        public IInBoxView InboxView
        {
            get { return _inbox_view; }
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