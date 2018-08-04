using log4net;
using log4net.Appender;
using log4net.Layout;
using log4net.Repository.Hierarchy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kingdee.K3.WANYUE.PlugIn.service.tools
{
   public static class WANYUELogHelpService
    {
        public static void SetLevel(string loggerName, string levelName)
        {
            ILog log = LogManager.GetLogger(loggerName);
            var l = (Logger)log.Logger;

            l.Level = l.Hierarchy.LevelMap[levelName];
        }

        // Add an appender to a logger
        public static void AddAppender(string loggerName, log4net.Appender.IAppender appender)
        {
            log4net.ILog log = LogManager.GetLogger(loggerName);
            var l = (Logger)log.Logger;

            l.AddAppender(appender);
        }

        // Create a new file appender
        public static log4net.Appender.IAppender CreateFileAppender(string name, string fileName)
        {
            var appender = new  FileAppender { Name = name, File = fileName, AppendToFile = true };
            var layout = new PatternLayout { ConversionPattern = "%date [%thread] %level %logger - %message%newline" };
            layout.ActivateOptions();

            appender.Layout = layout;
            appender.ActivateOptions();

            return appender;
        }
    }
}
