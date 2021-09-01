using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.DataTransferObjects
{
    //sau dung de cac class khac ke thua
    abstract class OwnerDto_Modification
    {
        public abstract string Name { get; set; }
        public abstract DateTime DateOfBirth { get; set; }
        public abstract string Address { get; set; }
    }
}
