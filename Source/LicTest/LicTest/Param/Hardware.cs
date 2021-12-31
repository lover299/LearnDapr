using Hardware.Info;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LicTest.Param
{
    public class Hardware
    {
        public static CommonParam GetHardwareID()
        {
            CommonParam param = new CommonParam();
            HardwareInfo info = new HardwareInfo();
            info.RefreshAll();
            param.CpuID = CreateMD5(info.CpuList[0].ProcessorId);
            param.BiosId = CreateMD5(info.BiosList[0].SerialNumber);
            param.MotherBoardId = CreateMD5(info.MotherboardList[0].SerialNumber);
            return param;
        }
        public static string CreateMD5(string input)
        {
            // Use input string to calculate MD5 hash
            using (System.Security.Cryptography.MD5 md5 = System.Security.Cryptography.MD5.Create())
            {
                byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(input);
                byte[] hashBytes = md5.ComputeHash(inputBytes);

                // Convert the byte array to hexadecimal string
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < hashBytes.Length; i++)
                {
                    sb.Append(hashBytes[i].ToString("X2"));
                }
                return sb.ToString();
            }
        }
    }
}
