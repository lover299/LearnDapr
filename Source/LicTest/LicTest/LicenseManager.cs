using LicTest.Param;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace LicTest
{
    public class LicenseManager
    {

        public static CommonParam GenerateUnsignedParam(CommonParam param)
        {
            CommonParam hardware= Param.Hardware.GetHardwareID();
            param.CopyParamTo(hardware);
            return hardware;
        }

        public static void SignatureParamToFile(CommonParam unSignparam,string filePath,RSA certificate)
        {
            string tempPath=System.IO.Path.GetTempFileName();

            SerializeFile.SerializeToXmlFile(unSignparam, tempPath);
            SignAndVerify.SignXmlFile(tempPath, filePath, certificate);
        }
    }
}
