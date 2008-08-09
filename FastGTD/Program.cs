using System.Windows.Forms;

namespace FastGTD
{
    public class Program
    {
        public static int Main(string[] argv)
        {
            InBoxPresenter in_form = CreateInBoxForm();
            Application.Run(in_form.View.Form);

            return 0;
        }

        public static InBoxPresenter CreateInBoxForm()
        {
            InBoxForm view = new InBoxForm();
            IInboxModel model = new InboxModel();
            return new InBoxPresenter(view, model);
        }
    }
}