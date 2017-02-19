using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ESharp
{
    public class GeneralFile
    {
        public readonly string fullName;
        public readonly string list;

        public GeneralFile(string fullName, string list)
        {
            this.fullName = fullName;
            this.list = list;
        }
    }
}
