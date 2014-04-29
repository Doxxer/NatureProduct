using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NatureProduct.Models
{
    class EExtenDescribe : IEquatable<EExtenDescribe>
    {
        public int Id { get; set; }
        public string NameE { get; set; }
        public string Describe { get; set; }
        public string Color { get; set; }

        public override string ToString()
        {
            return "NameE: " + NameE + "   Describe: " + Describe;
        }
        public override bool Equals(object obj)
        {
            if (obj == null) return false;
            EExtenDescribe objAsPart = obj as EExtenDescribe;
            if (objAsPart == null) return false;
            else return Equals(objAsPart);
        }
        public override int GetHashCode()
        {
            return Id;
        }
        public bool Equals(EExtenDescribe other)
        {
            if (other == null) return false;
            return (this.NameE.Equals(other.NameE));
        }
    }
}
