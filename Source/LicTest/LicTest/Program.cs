using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace LicTest
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string publicKey = @"      <RSAKeyValue><Modulus>sJ54h1tysDfp4Klou2XJWxZPdjsWDbfDvTdU3hYLejmpwIvcLcZpIZytsiKU272HuJcyCAEBxlpqvE5dJdFRXM8AW0wumxOyHhzqnawBxio27lklGkMtpMLtrExgickjTBnVacN+QS21fXP7p/XFY4HKOqF74vhLwCbCdYcMPhE=</Modulus><Exponent>AQAB</Exponent></RSAKeyValue>";
            /*
             hVLbTtwwEP0Va1+R40vuyIm0t4iIpYsUIfHqJmZjNXFSxyHst/HAJ/ELdQKlAlWtH8YzZ+bMmZHm9fmFXXFdTVyLvNpxw8FT26jh8mmQyao2pr9EaJomZ3KdTp8QxZig+5tDUdai5VCqwXBVitUHq/o/a5VayaEWVa4qWXLT6VR1BtQLxtDXHMuH7ai1UOY43PdHfSVPtdDpA28GwdDfk6zId2kBCfQhJZDGLokxwUEESRwFXhBQz4dxSMOQEoqhjzFDM4PttHwUhTwpbkYtAHpHcvXQpWTtx6Hn0sQnJAyt5wdexNCfAnbsheZGqlNxHoxoC6Elb76N7Xc7EcYuDSDB9kG82PU6jFyG/kVit7qrxtKsSyMfbVGn5pE2shuuxTklju9Qhn6HC74MstsfDgAACCgmoZUiCcXWpdhdlLHz9l3gee8PFtv24w/bZ3t7Z+H3YAbvlPw52uuYtW24FG+y/SbbZJltEeFgv01yZUQTeCDjrWzOIAA3XSUaQDwKCiP63i4ICE2Wxm9y6PPdpb8A
             */
            X509Certificate2 certificate = new X509Certificate2();
            certificate.Import("test.pfx", "123456789", X509KeyStorageFlags.PersistKeySet);
            string temp = certificate.GetRSAPublicKey().ToXmlString(false);

            Console.WriteLine(publicKey);
            var privateKey = certificate.GetRSAPrivateKey();

            var privateKey2 = certificate.PrivateKey as RSACryptoServiceProvider;
            privateKey2.ExportParameters(false);
            try
            {
                Console.WriteLine("input:");
                string message = Console.ReadLine();
                Console.WriteLine("sign message...");
                byte[] data = ASCIIEncoding.UTF8.GetBytes(message);
                Console.WriteLine("message bytes");
                Console.WriteLine(BitConverter.ToString(data));
                byte[] signedData;
                using (MemoryStream stream = new MemoryStream(data))
                {

                    signedData = privateKey.SignData(data, HashAlgorithmName.SHA256, RSASignaturePadding.Pkcs1);
                    Console.WriteLine("signed bytes");
                    Console.WriteLine(BitConverter.ToString(signedData));
                }

                RSACryptoServiceProvider key = new RSACryptoServiceProvider();
                
                key.FromXmlString(publicKey);
                Console.WriteLine("verify message...");

                bool result = key.VerifyData(data, SHA256.Create(), signedData);
                Console.WriteLine($"verify message {(result ? "success" : "failed")}");

                Console.WriteLine("encrypt message...");
                string encrypt = $"{message}-encrypted";
                data = ASCIIEncoding.UTF8.GetBytes(encrypt);
                byte[] encryptedData = key.Encrypt(data, true);
                Console.WriteLine($"encrypted message:");
                Console.WriteLine(BitConverter.ToString(encryptedData));
                Console.WriteLine($"decrypt message...");


                byte[] decryptData = privateKey.Decrypt(encryptedData, RSAEncryptionPadding.Pkcs1);
                string decryptMessage = ASCIIEncoding.UTF8.GetString(decryptData);
                Console.WriteLine($"decrypt message:");
                Console.WriteLine($"{decryptMessage}");

                Console.ReadLine();

            }
            catch (Exception ex)
            {
                if (ex is System.Security.Cryptography.CryptographicException)
                {
                    Console.WriteLine($"this key is use for {privateKey2.CspKeyContainerInfo.KeyNumber} only");
                }

                Console.ReadLine();
            }
        }
    }
}
