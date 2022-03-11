using Microsoft.VisualStudio.TestTools.UnitTesting;
using BBK.TraceExtend;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BBK.TraceExtend.Tests
{
    [TestClass()]
    public class TraceExtendTests
    {
        [TestMethod()]
        [DataRow(FormatType.Error, FormatType.Error, true)]
        [DataRow(FormatType.Error, FormatType.Fatal, false)]
        [DataRow(FormatType.Error, FormatType.Error | FormatType.Warn, true)]
        [DataRow(FormatType.Warn, FormatType.Error | FormatType.Warn, true)]
        [DataRow(FormatType.Error, FormatType.Warn | FormatType.Info, false)]
        [DataRow(FormatType.Error, FormatType.All, true)]
        [DataRow(FormatType.Error, FormatType.Off, false)]
        [DataRow(FormatType.Off, FormatType.All, false)]
        [DataRow(FormatType.Off, FormatType.Error, false)]
        public void IsLogtypeTest(FormatType type, FormatType filter, bool result)
        {
            Assert.AreEqual(TraceLogger.IsLogtype(new FormattedLog() { LogType = type }, filter), result);
        }
    }
}