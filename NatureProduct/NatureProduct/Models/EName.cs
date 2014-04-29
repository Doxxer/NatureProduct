using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NatureProduct.Models
{
    public class EName : IEquatable<EName>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string NameE { get; set; }
        public string Color { get; set; }

        public override string ToString()
        {
            return "NameE: " + NameE + "   Name: " + Name;
        }
        public override bool Equals(object obj)
        {
            if (obj == null) return false;
            EName objAsPart = obj as EName;
            if (objAsPart == null) return false;
            else return Equals(objAsPart);
        }
        public override int GetHashCode()
        {
            return Id;
        }
        public bool Equals(EName other)
        {
            if (other == null) return false;
            return (this.NameE.Equals(other.NameE));
        }
    }
}
