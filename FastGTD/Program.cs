using System.Collections.Generic;
using System.Windows.Forms;

namespace FastGTD
{
    public class Program
    {
        public static int Main(string[] argv)
        {
            var in_form = CreateInBoxForm();
            Application.Run((Form) in_form);

            return 0;
        }

        public static IInboxForm CreateInBoxForm()
        {
            var model = new InBoxModel();
            return new InBoxForm(model);
        }
    }
}