using System.Windows.Forms;

namespace FastGTD
{
    public class Program
    {
        public static int Main(string[] argv)
        {
            Form in_form = CreateInBoxForm();
            Application.Run(in_form);

            return 0;
        }

        public static InBoxForm CreateInBoxForm()
        {
            InBoxForm view = new InBoxForm();
            IInboxModel model = new InboxModel();
            new InBoxPresenter(view, model);
            return view;
        }
    }
}