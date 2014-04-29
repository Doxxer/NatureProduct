using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;


namespace NatureProduct.NetworkConnect
{
    [DataContract]
    public class Good
    {
        [DataMember]
        internal string name;

        [DataMember]
        internal List<EComponent> additives;
    }
}