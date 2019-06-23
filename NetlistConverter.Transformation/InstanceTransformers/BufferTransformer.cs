using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VerilogNetlistModel;

namespace NetlistConverter.Converter.InstanceTransformers
{
    public class BufferTransformer : IInstanceTransformer
    {
        public List<Instance> TryTransform(Instance instance, TransformationContext context)
        {
            if (!instance.ModuleIdentifier.Contains("io_ibuf") &&
                !instance.ModuleIdentifier.Contains("io_obuf"))
                return null;

            var input = instance.Ports.First(p => p.Identifier == "i").ConnectedNet;
            var output = instance.Ports.First(p => p.Identifier == "o").ConnectedNet;
            
            var result = new List<Instance>();

            var bufferInput = input;
            if (instance.Ports.First(p => p.Identifier == "i").IsConnectedNetNegated)
            {
                var inverter = new Instance("INV", "inv_" + context.GetNextInstanceNumber(this));
                inverter.Ports.Add(new Net("I", NetType.Input, new Net(input.Identifier, NetType.Wire)));
                var inverterOutput = new Net("n_" + context.GetNextNetNumber(), NetType.Wire);
                inverter.Ports.Add(new Net("O", NetType.Input, inverterOutput));
                bufferInput = inverterOutput;
                result.Add(inverter);
            }

            var buffer = new Instance("BUF", "buf_" + context.GetNextInstanceNumber(this));
            buffer.Ports.Add(new Net("I", NetType.Input, bufferInput));
            buffer.Ports.Add(new Net("O", NetType.Output, new Net(output.Identifier, NetType.Wire)));
            result.Add(buffer);

            return result;
        }
    }
}
