using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace NatureProduct.NetworkConnect
{
    [DataContract]
    public class EComponent
    {
        [DataMember]
        internal string id;

        [DataMember]
        internal List<string> names;

        [DataMember]
        internal byte severity;

        [DataMember]
        internal string comment;

        [DataMember]
        internal string category;
    }
}
