using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VerilogNetlistModel;

namespace NetlistConverter.Converter.InstanceTransformers
{
    public class DffeasTransformer : IInstanceTransformer
    {
        public List<Instance> TryTransform(Instance instance, TransformationContext context)
        {
            if (!instance.ModuleIdentifier.Contains("dffeas")) return null;

            var fdc = new Instance("FDC", "fdc_" + context.GetNextInstanceNumber(this));

            var clock = instance.Ports.First(p => p.Identifier == "clk").ConnectedNet.Identifier;
            var clr = instance.Ports.First(p => p.Identifier == "clrn").ConnectedNet.Identifier;
            var dataInput = instance.Ports.First(p => p.Identifier == "d").ConnectedNet.Identifier;
            var dataOutput = instance.Ports.First(p => p.Identifier == "q").ConnectedNet.Identifier;

            fdc.Ports.Add(new Net("C", NetType.Input, new Net("clock", NetType.Wire)));
            fdc.Ports.Add(new Net("CLR", NetType.Input, new Net(clr, NetType.Wire)));
            fdc.Ports.Add(new Net("D", NetType.Input, new Net(dataInput, NetType.Wire)));
            fdc.Ports.Add(new Net("Q", NetType.Output, new Net(dataOutput, NetType.Wire)));

            return new List<Instance> { fdc };
        }
    }
}
