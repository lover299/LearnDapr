using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace LicTest.Param
{
    [Serializable]
    public class CommonParam
    {
        public string CruxMessage { get; set; }
        [XmlAttribute("ID1")]
        public string CpuID { get; set; }
        [XmlAttribute("ID2")]
        public string BiosId { get; set; }
        [XmlAttribute("ID3")]
        public string MotherBoardId { get; set; }

       public void CopyParamTo( CommonParam target)
        {
            target.CruxMessage = CruxMessage;    
        }
    }
}
