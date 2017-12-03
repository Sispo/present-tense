using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PresentTense
{
    public class PresentTenseWordType
    {
        public string name { get; private set; }
        public string stackID { get; private set; }

        public PresentTenseWordType(string name, string stackID)
        {
            this.name = name;
            this.stackID = stackID;
        }

        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }

            var type = (PresentTenseWordType)obj;

            return stackID == type.stackID;
        }

        public override int GetHashCode()
        {
            return stackID.GetHashCode() ^
                name.GetHashCode();
        }
    }
}
