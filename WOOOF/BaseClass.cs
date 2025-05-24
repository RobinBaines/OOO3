using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WOOOF
{
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

            // Default comparer for type.
            public int CompareTo(BaseClass? compareClass)
            {
                // A null value means that this object is greater.
                if (compareClass == null)
                    return 1;

                else
                    return this.Name.CompareTo(compareClass.Name);
            }


            public string Name { get; set; } = "BaseObject";

            public string Description = "";
            public DateTime created = DateTime.Now;
        }
}
