using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BBK.TraceExtend
{
    public class FormattedLog
    {
        public const string LogHeader = "FormattedLog---";
        public const string DateFormatter = "yyyy:MM:dd-HH:mm:ss:fff";
        public FormatType LogType { get; set; }
        public string Log { get; set; }
        public DateTime LogTime { get; set; }

        public string Content { get; set; }

        public FormattedLog()
        {

        }

        public FormattedLog(FormatType type, string log, DateTime time)
        {
            LogType = type;
            Log = log;
            LogTime = time;
            Content = $"{LogHeader}[{LogType.ToString()}][{LogTime.ToString(DateFormatter)}]:{log}";
        }

        public FormattedLog(string fullLog)
        {
            Content = fullLog;
            if (!string.IsNullOrWhiteSpace(fullLog))
            {
                if (fullLog.StartsWith(LogHeader))
                {
                    string temp = fullLog.Remove(0, LogHeader.Length);
                    string[] messages = temp.Split(new char[] { '[', ']' });
                    if (messages.Length >= 4)
                    {
                        //+5 is '[', ']', '[', ']', ':', five char
                        int prefixLength = messages[0].Length + messages[1].Length + messages[2].Length + messages[3].Length + 5;
                        DateTime time;
                        if (!DateTime.TryParseExact(messages[3], FormattedLog.DateFormatter, Thread.CurrentThread.CurrentCulture, DateTimeStyles.None, out time))
                        {
                            time = DateTime.Now;
                        }
                        LogType = (FormatType)Enum.Parse(typeof(FormatType), messages[1], true);
                        LogTime = time;
                        Log = temp.Remove(0, prefixLength);
                        return;
                    }
                }
                LogType = FormatType.Trace;
                Log = fullLog;
            }
            else
            {
                LogType = FormatType.Off;
            }
        }
    }
}
