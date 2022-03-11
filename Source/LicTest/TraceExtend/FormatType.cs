using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BBK.TraceExtend
{
    [Flags]
    public enum FormatType : int
    {
        Off = 0,
        Fatal = 1,
        Error = 2,
        Warn = 4,
        Info = 8,
        Debug = 16,
        /// <summary>
        /// for compatibility trace will has no format 
        /// </summary>
        Trace = 32,
        All = 0xffff
    }
}
