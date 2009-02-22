using System.Windows.Forms;

namespace FastGTD
{
    public class Program
    {
        public static int Main(string[] argv)
        {
            var app = new FastGTDApp();
            app.Start();
            app.StartMessageLoop();
            app.Close();
            return 0;
        }
    }
}