using System;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;

namespace Bricks.Power
{
    class Program
    {
        [DllImport("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, uint Msg, int wParam, int lParam);

        static void Main(string[] args)
        {
            uint WM_SYSCOMMAND = 0x112;
            int SC_MONITORPOWER = 0xF170;
            Form form = new Form();
            form.Show();
            if (args.Length > 0)
            Thread.Sleep(int.Parse(args[0])*1000);
            SendMessage(form.Handle, WM_SYSCOMMAND, SC_MONITORPOWER, 2);
            form.Close();
        }
    }
}
