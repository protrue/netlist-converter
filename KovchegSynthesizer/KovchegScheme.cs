using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using VerilogObjectModel;

namespace KovchegSynthesizer.KovchegElements
{
    public class KovchegScheme
    {
        public ModuleDescription ModuleDescription { get; set; }

        public KovchegScheme(ModuleDescription moduleDescription)
        {
            ModuleDescription = moduleDescription;
        }

        public override string ToString()
        {
            return ModuleDescription.ToString();
        }
    }
}
