using log4net;

namespace Bricks.Logging
{
    public class CoreLogger : BricksLogger
    {
        static CoreLogger()
        {
            new CoreLogger();
        }

        private CoreLogger() {}
        public static readonly ILog Instance = LogManager.GetLogger("root");
    }
}