using System.Collections.Generic;
using System.Linq;

namespace VerilogNetlistModel
{
    public class Module : IIdentifiable
    {
        public string Identifier { get; set; }

        public List<Net> Nets { get; set; }

        public IEnumerable<Net> Ports
        {
            get => Nets.Where(n =>
                n.NetType == NetType.Port ||
                n.NetType == NetType.Input ||
                n.NetType == NetType.Output);
        }

        public List<Instance> Instances { get; set; }

        public Module(string identifier)
        {
            Identifier = identifier;
            Nets = new List<Net>();
            Instances = new List<Instance>();
        }

        public Net SetNetType(string netId, NetType netType)
        {
            var net = Nets.First(p => p.Identifier == netId);
            net.NetType = netType;
            return net;
        }

        public override string ToString() => $"{Identifier} ({string.Join(" ", Ports)}) \n<{string.Join(" ", Nets)}> \n[{string.Join(" \n", Instances)}]";
    }
}
