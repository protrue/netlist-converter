using NetlistConverter.Converter.InstanceTransformers;
using VerilogNetlistModel;

namespace NetlistConverter.Converter
{
    public class NetlistTransformer
    {
        public readonly IInstanceTransformer[] Transformers;

        public NetlistTransformer()
        {
            Transformers = new IInstanceTransformer[]
            {
                new BufferTransformer(),
                new DffeasTransformer(),
                new LogicElementTransformer(),
            };
        }

        private void PreProcess(Module quartusScheme)
        {
            quartusScheme.Nets.RemoveAll(n =>
                n.NetType != NetType.Input &&
                n.NetType != NetType.Output &&
                n.NetType != NetType.Wire ||
                n.Identifier == "gnd" ||
                n.Identifier == "vcc");

            foreach (var net in quartusScheme.Nets)
            {
                net.Identifier = net.Identifier.Replace("~", "_");
                net.Identifier = net.Identifier.Replace(".", "_");
            }

            foreach (var instance in quartusScheme.Instances)
            {
                instance.ModuleIdentifier = instance.ModuleIdentifier.Replace("~", "_");
                instance.ModuleIdentifier = instance.ModuleIdentifier.Replace(".", "_");

                foreach (var port in instance.Ports)
                {
                    port.Identifier = port.Identifier.Replace("~", "_");
                    port.Identifier = port.Identifier.Replace(".", "_");

                    if (port.ConnectedNet != null)
                    {
                        port.ConnectedNet.Identifier = port.ConnectedNet.Identifier.Replace("~", "_");
                        port.ConnectedNet.Identifier = port.ConnectedNet.Identifier.Replace(".", "_");
                    }
                }
            }
        }

        public Module Transform(Module scheme)
        {
            PreProcess(scheme);
            
            var transformedScheme = new Module(scheme.Identifier);
            transformedScheme.Nets.Add(new Net("gnd", NetType.Input));
            transformedScheme.Nets.Add(new Net("vcc", NetType.Input));

            foreach (var modulePort in scheme.Ports)
                transformedScheme.Nets.Add(new Net(modulePort.Identifier, modulePort.NetType));

            var context = new TransformationContext();

            foreach (var instance in scheme.Instances)
                foreach (var transformer in Transformers)
                {
                    var transformedInstance = transformer.TryTransform(instance, context);
                    if (transformedInstance != null)
                    {
                        transformedScheme.Instances.AddRange(transformedInstance);
                        break;
                    }
                }

            return transformedScheme;
        }
    }
}
