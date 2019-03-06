using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VerilogObjectModel;

namespace KovchegSynthesizer
{
    public delegate List<ModuleInstantiation> Converter(ModuleInstantiation quartusInstance, SynthesisContext context);
}
