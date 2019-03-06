using System.Collections.Generic;
using System.Linq;

namespace VerilogObjectModel
{
    public class ModuleDescription : VerilogElement
    {
        public List<Net> Nets { get; set; }

        public IEnumerable<Net> Ports
        {
            get => Nets.Where(n =>
                n.NetType == NetType.Port ||
                n.NetType == NetType.Input ||
                n.NetType == NetType.Output);
        }

        public List<ModuleInstantiation> ModuleInstantiations { get; set; }

        public ModuleDescription(string identifier) : base(identifier)
        {
            Nets = new List<Net>();
            ModuleInstantiations = new List<ModuleInstantiation>();
        }
        
        public Net SetNetType(string netId, NetType netType)
        {
            var net = Nets.First(p => p.Identifier == netId);
            net.NetType = netType;
            return net;
        }
        
        public override string ToString() => $"{Identifier} ({string.Join(" ", Ports)}) \n<{string.Join(" ", Nets)}> \n[{string.Join(" \n", ModuleInstantiations)}]";
    }
}
