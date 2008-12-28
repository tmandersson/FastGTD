using System.Windows.Forms;
using FastGTD.DataAccess;

namespace FastGTD
{
    public class FastGTDApp
    {
        private InBoxModel _inModel;
        private IInBoxForm _inForm;

        public InBoxModel InModel
        {
            get { return _inModel; }
        }

        public IInBoxForm InForm
        {
            get { return _inForm; }
        }

        public void Start()
        {
            _inModel = new InBoxModel(new InBoxItemRepository());
            _inForm = new InBoxForm(InModel);
            _inForm.Show();
            _inModel.Load();
        }

        public void Close()
        {
            InForm.Close();
        }

        public void StartWinFormsMessageLoop()
        {
            Application.Run((Form)_inForm);
        }

        public static FastGTDApp StartNewTestApplication()
        {
            var app = new FastGTDApp();
            app.Start();
            return app;
        }
    }
}