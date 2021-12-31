using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace LicTest.Param
{
    public class SerializeFile
    {
        public static bool SerializeToXmlFile<T>(T data, string path) where T : class
        {
            bool result = false;
            try
            {
                using (FileStream fileStream = new FileStream(path, FileMode.Create))
                {
                    XmlSerializer serializer = new XmlSerializer(typeof(T));
                    serializer.Serialize(fileStream, data);
                    fileStream.Flush();
                    result = true;
                }
            }
            catch (Exception ex)
            {
                Trace.TraceError($" Serialize failed {path} ({ex.ToString()})");
            }
            return result;
        }

        public static T DeserializeXmlFromFile<T>(string path) where T : class
        {
            T result = default;
            try
            {
                using (FileStream fileStream = new FileStream(path, FileMode.Open))
                {
                    XmlSerializer serializer = new XmlSerializer(typeof(T));
                    result = serializer.Deserialize(fileStream) as T;
                }
            }
            catch (Exception ex)
            {
                Trace.TraceError($"Deserialize failed {path} ({ex.ToString()})");
            }
            return result;
        }
    }
}
