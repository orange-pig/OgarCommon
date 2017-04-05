using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace OgarCommon
{
    public class SerilaliztionHelper
    {
        public static byte[] SerilalizeObject(object obj)
        {
            byte[] tBytes = null;

            if (obj != null)
            {
                using (MemoryStream memoryStream = new MemoryStream())
                {
                    BinaryFormatter binaryFormatter = new BinaryFormatter();
                    binaryFormatter.Serialize(memoryStream, obj);
                    tBytes = memoryStream.ToArray();
                    memoryStream.Close();
                }
            }

            return tBytes;
        }

        public static object DeserilalizeObject(byte[] bytes)
        {
            object obj = null;

            if (bytes != null)
            {
                using (MemoryStream memoryStream = new MemoryStream(bytes))
                {
                    memoryStream.Position = 0;
                    BinaryFormatter formatter = new BinaryFormatter();
                    obj = formatter.Deserialize(memoryStream);
                    memoryStream.Close();
                }
            }

            return obj;
        }
    }
}
