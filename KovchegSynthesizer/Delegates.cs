using System.Collections.Generic;
using VerilogNetlistModel;

namespace KovchegSynthesizer
{
    public delegate List<Instance> Converter(Instance quartusInstance, SynthesisContext context);
}
