using LicenseVerify;
using LicTest.Param;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LicenseGenerator
{
    public partial class LicenseGenerator : Form
    {
        string licenseName = "license.lic";
        public LicenseGenerator()
        {
            InitializeComponent();
        }

        private void buttonGenerate_Click(object sender, EventArgs e)
        {
            string[] ids = SignAndVerify.DecodeBase64String(textBoxId.Text.Trim()).Split(';');
            if (ids.Length!=3)
            {
                MessageBox.Show("硬件ID（指纹）错误！");
            }
            else
            {
               CommonParam param=  SystemInfo.FromSystemID(ids);
                param.CruxMessage = textBoxMesage.Text;
                X509Certificate2 certificate = new X509Certificate2();
                certificate.Import("test.pfx", textBoxPassword.Text, X509KeyStorageFlags.PersistKeySet);
                var privateKey = certificate.GetRSAPrivateKey();
                LicTest.LicenseManager.SignatureParamToFile(param, licenseName, privateKey);
            }
        }

        private void buttonVerify_Click(object sender, EventArgs e)
        {
            if (Checker.Check(licenseName))
            {
                MessageBox.Show("Success");
                CommonParam tmp = Checker.GetCurrentParam(licenseName);
                MessageBox.Show(tmp?.CruxMessage);
            }
            else
            {
                MessageBox.Show("Failed!!!!!!");
            }
        }

        private void buttonGetID_Click(object sender, EventArgs e)
        {
          textBoxId.Text=SystemInfo.GetSystemID();
        }
    }
}
