using System.Collections.Generic;
using VerilogObjectModel;

namespace KovchegSynthesizer
{
    public delegate List<ModuleInstantiation> Converter(ModuleInstantiation quartusInstance, SynthesisContext context);
}
