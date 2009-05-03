namespace FastGTD
{
    public static class Program
    {
        public static int Main()
        {
            var app = new FastGTDApp();
            app.ShowStartForm();
            app.StartMessageLoop();
            app.Close();
            return 0;
        }
    }
}