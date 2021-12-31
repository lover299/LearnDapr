using LicTest.Param;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace LicenseVerify
{
    public class Checker
    {
        public static bool Check(string licenseFile)
        {
            string publicKey = @"      <RSAKeyValue><Modulus>sJ54h1tysDfp4Klou2XJWxZPdjsWDbfDvTdU3hYLejmpwIvcLcZpIZytsiKU272HuJcyCAEBxlpqvE5dJdFRXM8AW0wumxOyHhzqnawBxio27lklGkMtpMLtrExgickjTBnVacN+QS21fXP7p/XFY4HKOqF74vhLwCbCdYcMPhE=</Modulus><Exponent>AQAB</Exponent></RSAKeyValue>";
            RSACryptoServiceProvider key = new RSACryptoServiceProvider();

            key.FromXmlString(publicKey);
           return SignAndVerify.VerifyXmlFile(licenseFile, key);
        }

        public static CommonParam GetCurrentParam(string licenseFile)
        {
            if (!Check(licenseFile))
            {
                Trace.WriteLine("licenseFile check failed!");
                return null;
            }
            CommonParam param = SerializeFile.DeserializeXmlFromFile<CommonParam>(licenseFile);
            CommonParam currentHardware= Hardware.GetHardwareID();
            int goal = 0;
            //3 curren hardwareid ,2ok will be ok
            if (param.CpuID.Equals(currentHardware.CpuID))
            {
                goal++;
            }
            if (param.BiosId.Equals(currentHardware.BiosId))
            {
                goal++;
            }
            if (param.MotherBoardId.Equals(currentHardware.MotherBoardId))
            {
                goal++;
            }
            if (goal>=2)
            {
                return param;
            }
            else
            {
                Trace.WriteLine("licenseFile userID  failed!");
                return null;
            }
        }

    }
}
