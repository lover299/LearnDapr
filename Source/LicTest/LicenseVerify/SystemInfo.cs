using LicTest.Param;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LicenseVerify
{
    public class SystemInfo
    {
        public static string GetSystemID()
        {
            CommonParam param= Hardware.GetHardwareID();

            return SignAndVerify.EncodeBase64String($"{param.CpuID};{param.BiosId};{param.MotherBoardId}");
        }

        public static CommonParam FromSystemID(string[] ids)
        {
            CommonParam param=new CommonParam();
            param.CpuID = ids[0];
            param.BiosId = ids[1];
            param.MotherBoardId = ids[2];
            return param;
        }
    }
}
