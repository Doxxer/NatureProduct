using System;

namespace NatureProduct.Models
{
    class DescribeCateg : IEquatable<DescribeCateg>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Color { get; set; }  

        public override string ToString()
        {
            return  "   Name: " + Name;
        }
        public override bool Equals(object obj)
        {
            if (obj == null) return false;
            DescribeCateg objAsPart = obj as DescribeCateg;
            if (objAsPart == null) return false;
            else return Equals(objAsPart);
        }
        public override int GetHashCode()
        {
            return Id;
        }
        public bool Equals(DescribeCateg other)
        {
            if (other == null) return false;
            return (this.Name.Equals(other.Name));
        }
    }
}
