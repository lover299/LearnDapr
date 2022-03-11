using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BBK.TraceExtend
{
    public class TraceLogger
    {
        public static FormattedLogListener RegistListener(string name)
        {
            var listener = new FormattedLogListener(name);
            Trace.Listeners.Add(listener);
            return listener;
        }

        public static void TraceLog(FormattedLog log)
        {
            Trace.WriteLine(log.Content);
        }

        public static void TraceLog(FormatType type, string logContent)
        {
            TraceLog(new FormattedLog(type, logContent, DateTime.Now));
        }

        public static void TraceFatal(string logContent)
        {
            TraceLog(FormatType.Fatal, logContent);
        }

        public static void TraceError(string logContent)
        {
            TraceLog(FormatType.Error, logContent);
        }
        public static void TraceWarn(string logContent)
        {
            TraceLog(FormatType.Warn, logContent);
        }
        public static void TraceInfo(string logContent)
        {
            TraceLog(FormatType.Info, logContent);
        }
        public static void TraceDebug(string logContent)
        {
            TraceLog(FormatType.Debug, logContent);
        }

        public static void TraceException(FormatType type, Exception exception)
        {
            if (exception != null)
            {
                string message = exception.Message;
                string stackTrace = exception.StackTrace;
                TraceLog(type, $"{message}{Environment.NewLine}{stackTrace}");
            }
        }

        public static void TraceFatalException(Exception exception)
        {
            TraceException(FormatType.Fatal, exception);
        }

        public static void TraceInfoException(Exception exception)
        {
            TraceException(FormatType.Info, exception);
        }

        public static FormattedLog FormatLog(string log)
        {
            FormattedLog formattedLog = new FormattedLog(log);
            return formattedLog;
        }

        public static bool IsLogtype(FormattedLog log, FormatType type)
        {
            bool isLogtype = false;
            if (log != null&&log.LogType!= FormatType.Off)
            {
                if ((log.LogType & type) == log.LogType)
                {
                    isLogtype = true;
                }
            }
            return isLogtype;
        }
    }
}
