using System;
using System.IO;
using log4net.Config;

namespace Bricks.Logging
{
    public class BricksLogger
    {
        protected BricksLogger()
        {
            string log4NetFilePath = ConfigFile();
            if (File.Exists(log4NetFilePath))
            {
                var configFile = new FileInfo(log4NetFilePath);
                XmlConfigurator.ConfigureAndWatch(configFile);
            }
            else
                Console.Error.WriteLine("Log4Net not configured. Looked for file: {0}", new FileInfo(log4NetFilePath).FullName);
        }

        protected virtual string ConfigFile()
        {
            return @"log4net.config";
        }
    }
}