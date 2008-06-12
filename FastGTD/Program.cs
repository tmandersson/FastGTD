using System.Windows.Forms;

namespace FastGTD
{
    public class Program
    {
        public static int Main(string[] argv)
        {
            Form in_form = new InBoxForm();
            Application.Run(in_form);

            return 0;
        }
    }
}