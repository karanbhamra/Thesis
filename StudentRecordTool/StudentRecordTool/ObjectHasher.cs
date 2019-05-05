using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;

namespace ReceiveJsonSaveToCosmosFunction
{
    public class ObjectHasher
    {
        public static byte[] GetBytes(Object obj)
        {
            if (obj == null)
            {
                return null;
            }

            BinaryFormatter bf = new BinaryFormatter();
            MemoryStream ms = new MemoryStream();
            bf.Serialize(ms, obj);
            return ms.ToArray();
        }
    }
}
