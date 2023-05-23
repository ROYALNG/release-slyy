using NLog;

namespace GHCore
{
    public static class GHLogger
    {
        private static readonly Logger logger = LogManager.GetLogger("GHLogger");

        public static void Log(string message, LogCategory category)
        {
            LogLevel logLevel;
            switch (category)
            {
                case LogCategory.Debug:
                    logLevel = LogLevel.Debug;
                    break;
                case LogCategory.Exception:
                    logLevel = LogLevel.Error;
                    break;
                case LogCategory.Info:
                    logLevel = LogLevel.Info;
                    break;
                case LogCategory.Warn:
                    logLevel = LogLevel.Warn;
                    break;
                default:
                    logLevel = LogLevel.Trace;
                    break;
            }
            logger.Log(logLevel, message);
        }
    }

    // 摘要:
    //     Defines values for the categories used by Microsoft.Practices.Prism.Logging.ILoggerFacade.
    public enum LogCategory
    {
        // 摘要:
        //     Debug category.
        Debug = 0,
        //
        // 摘要:
        //     Exception category.
        Exception = 1,
        //
        // 摘要:
        //     Informational category.
        Info = 2,
        //
        // 摘要:
        //     Warning category.
        Warn = 3,
    }
}
