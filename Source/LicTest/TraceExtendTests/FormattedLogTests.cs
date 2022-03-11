using Microsoft.VisualStudio.TestTools.UnitTesting;
using BBK.TraceExtend;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Threading;
using System.Globalization;

namespace BBK.TraceExtend.Tests
{
    [TestClass()]
    public class FormattedLogTests
    {
        [TestMethod()]
        [DataRow("11111", "2022:03:10-17:20:46:229", FormatType.Debug)]
        [DataRow(null, "2022:03:10-17:20:46:229", FormatType.Fatal)]
        [DataRow("", "2022:03:10-17:20:46:229", FormatType.Fatal)]
        public void FormattedLogCreateTest(string log, string dateTime, FormatType type)
        {
            DateTime time;
            if (DateTime.TryParseExact(dateTime, FormattedLog.DateFormatter, Thread.CurrentThread.CurrentCulture, DateTimeStyles.None, out time))
            {
                FormattedLog formattedLog = new FormattedLog(type, log, time);
                FormattedLog deconde = new FormattedLog(formattedLog.Content);
                Trace.WriteLine(formattedLog.Content);
                Assert.AreEqual(formattedLog.Log ?? "", deconde.Log);
                Assert.AreEqual(formattedLog.Content, deconde.Content);
                Assert.AreEqual(formattedLog.LogType, deconde.LogType);
                Assert.AreEqual(formattedLog.LogTime.ToString(FormattedLog.DateFormatter), deconde.LogTime.ToString(FormattedLog.DateFormatter));
            }
            else
            {
                Assert.Fail();
            }
            //FormattedLog formattedLog = new FormattedLog(type,log);
        }
    }
}