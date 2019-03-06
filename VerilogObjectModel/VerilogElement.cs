using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VerilogObjectModel
{
    public abstract class VerilogElement
    {
        public string Identifier { get; set; }

        protected VerilogElement(string identifier)
        {
            Identifier = identifier;
        }
    }
}
