using System.Windows.Forms;

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
            _inModel = new InBoxModel();
            _inForm = new InBoxForm(InModel);
            _inForm.Show();
        }

        public void Close()
        {
            InForm.Close();
        }

        public void StartWinFormsMessageLoop()
        {
            Application.Run((Form)_inForm);
        }
    }
}