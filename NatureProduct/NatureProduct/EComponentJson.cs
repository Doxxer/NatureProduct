using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization.Json;
using System.IO;

namespace NatProductClientNetwork
{
    class EComponentJson
    {
        public static string serializeEComponent(EComponent eComponent)
        {
            MemoryStream mStream = new MemoryStream();
            DataContractJsonSerializer json = new DataContractJsonSerializer(typeof(EComponent));
            json.WriteObject(mStream, eComponent);
            return Encoding.UTF8.GetString(mStream.ToArray());
        }

        public static EComponent deserializeEComponent(string jsonEComponent)
        {
            DataContractJsonSerializer json = new DataContractJsonSerializer(typeof(EComponent));
            return (EComponent)json.ReadObject(new MemoryStream(Encoding.UTF8.GetBytes(jsonEComponent)));
        }
    }
}
