using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOO3
{
    /// <summary>
    /// An abstract class cannot be instantiated.
    /// The Name of an Object and a Quality is unique.
    /// Checking for Objects in a List requires IEquatable and Equal and Hash overrides.
    /// </summary>
    abstract class BaseClass : IEquatable<BaseClass>, IComparable<BaseClass>
    {
        public override string ToString()
        {
            return Name;
        }
        public override bool Equals(object? obj)
        {
            if (obj == null) return false;
            
            BaseClass? objAsPart = obj as BaseClass;
                 
            if (objAsPart == null) return false;
            else return Equals(objAsPart);
        }
        public override int GetHashCode()
        {
            return Name.GetHashCode();
        }
        public bool Equals(BaseClass? other)
        {
            if (other == null) return false;
            return (this.Name.Equals(other.Name));
        }

        // Default comparer for Part type.
        public int CompareTo(BaseClass? comparePart)
        {
            // A null value means that this object is greater.
            if (comparePart == null)
                return 1;

            else
                return this.Name.CompareTo(comparePart.Name);
        }

        public string Name { get; set; } = "BaseObject";

        public string Description ="";
        //this may be the time of instantiation for an SO but is much less likely for an RO for example publication date of a text..
        public DateTime created = DateTime.Now;
        //public DateTime? ended;
        public List<SensualObject> references = new List<SensualObject>();
    }
}
