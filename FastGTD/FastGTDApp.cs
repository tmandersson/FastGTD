using System.Windows.Forms;
using FastGTD.DataAccess;

namespace FastGTD
{
    public class FastGTDApp
    {
        private InBoxModel _inbox_model;
        private IInBoxView _inbox_view;
        private InBoxController _controller;

        public InBoxModel InboxModel
        {
            get { return _inbox_model; }
        }

        public IInBoxView InboxView
        {
            get { return _inbox_view; }
        }

        public void Start()
        {
            _inbox_model = new InBoxModel(new InBoxItemRepository());
            _inbox_view = new InBoxForm();
            _controller = new InBoxController(_inbox_view, _inbox_model);

            _controller.Show();
        }

        public void StartMessageLoop()
        {
            _inbox_view.StartMessageLoop();
        }

        public void Close()
        {
            _inbox_view.Close();
        }

        public static FastGTDApp StartNewTestApplication()
        {
            var app = new FastGTDApp();
            app.Start();
            return app;
        }
    }
}