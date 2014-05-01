using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization.Json;
using System.IO;


namespace NatureProduct.NetworkConnect
{
    class GoodJson
    {
        public static string serializeGood(Good good)
        {
            MemoryStream mStream = new MemoryStream();
            DataContractJsonSerializer json = new DataContractJsonSerializer(typeof(Good));
            json.WriteObject(mStream, good);
            return Encoding.UTF8.GetString(mStream.ToArray(), 0, mStream.ToArray().Length);
        }

        public static Good deserializeGood(string jsonGood)
        {
            DataContractJsonSerializer json = new DataContractJsonSerializer(typeof(Good));
            return (Good)json.ReadObject(new MemoryStream(Encoding.UTF8.GetBytes(jsonGood)));
        }
    }
}
