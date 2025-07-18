﻿//
// @Copyright 2025 Robin Baines
// Licensed under the MIT license. See MITLicense.txt file in the project root for details.
//

namespace OOOCL
{
    /// <summary>
    /// An abstract class cannot be instantiated.
    /// The Name of an Object and a Quality is unique.
    /// Checking for Objects in a List requires IEquatable and Equal and Hash overrides.
    /// </summary>
   public abstract class BaseClass : System.IEquatable<BaseClass>, IComparable<BaseClass>
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

        public string Description ="";
        public DateTime created = DateTime.Now;
        public SensualObject? SOParent;  //is the owner of this SQ.
    }
}
