using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BBK.TraceExtend
{
    public class FormattedLogListener: TraceListener
    {
        private List<string> cache = new List<string>();
        private string notLineMessageCache = string.Empty;
        public FormattedLogListener() : base()
        {
        }

        public FormattedLogListener(string name) : base(name)
        {

        }

        /// <summary>
        /// log occurred
        /// </summary>
        //public event Action<string> LogOccurred;
        public event Action<FormattedLog> FormattedLogOccurred;

        public override void Write(string message)
        {
            notLineMessageCache = $"{notLineMessageCache}{message}";
        }

        public override void WriteLine(string message)
        {
            string tempMessage = $"{notLineMessageCache}{message}";
            if (FormattedLogOccurred == null)
            {
                cache.Add(tempMessage);
            }
            else
            {
                foreach (var log in cache)
                {
                    OnFormattedLogOccurred(log);
                    //LogOccurred.Invoke(log);
                }
                OnFormattedLogOccurred(tempMessage);
                cache.Clear();
            }
            notLineMessageCache = string.Empty;
        }

        private void OnFormattedLogOccurred(string message)
        {
            FormattedLog formattedLog = new FormattedLog(message);
            FormattedLogOccurred?.Invoke(formattedLog);
        }
    }
}
