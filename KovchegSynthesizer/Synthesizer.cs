using System.Linq;
using System.Text;
using QuartusAnalyzer;
using VerilogNetlistModel;

namespace KovchegSynthesizer
{
    public static class Synthesizer
    {
        private static void PreProcess(QuartusScheme quartusScheme)
        {
            quartusScheme.Module.Nets.RemoveAll(n =>
                n.NetType != NetType.Input &&
                n.NetType != NetType.Output &&
                n.NetType != NetType.Wire ||
                n.Identifier == "gnd" ||
                n.Identifier == "vcc");
        }

        public static KovchegScheme SynthesizeScheme(QuartusScheme quartusScheme)
        {
            PreProcess(quartusScheme);

            var context = new SynthesisContext();

            var kovchegScheme = new KovchegScheme(new Module(quartusScheme.Module.Identifier));

            kovchegScheme.Module.Nets.Add(new Net("gnd", NetType.Input));
            kovchegScheme.Module.Nets.Add(new Net("vcc", NetType.Input));

            foreach (var modulePort in quartusScheme.Module.Ports)
                kovchegScheme.Module.Nets.Add(new Net(modulePort.Identifier, modulePort.NetType));

            foreach (var instance in quartusScheme.Module.Instances)
                foreach (var converter in SynthesisRules.SynthesisRulesList)
                {
                    var conversionResult = converter(instance, context);
                    if (conversionResult == null) continue;
                    kovchegScheme.Module.Instances.AddRange(conversionResult);
                    break;
                }

            return kovchegScheme;
        }

        public static string SynthesizeText(KovchegScheme scheme)
        {
            var textBuilder = new StringBuilder();

            var modulePorts = string.Join(", ", scheme.Module.Ports.Where(p => p.NetType == NetType.Input || p.NetType == NetType.Output).Select(p => p.Identifier));
            textBuilder.Append($"module {scheme.Module.Identifier} ({modulePorts});\n");
            foreach (var port in scheme.Module.Ports)
            {
                textBuilder.AppendLine($"{port.NetType.ToString().ToLower()} {port.Identifier};");
            }

            textBuilder.AppendLine();

            foreach (var element in scheme.Module.Instances)
            {
                var instancePorts = string.Join(", ",
                    element.Ports.Select(p => $".{p.Identifier}({p.ConnectedNet.Identifier})"));
                textBuilder.AppendLine($"{element.ModuleIdentifier} {element.Identifier} ({instancePorts});");
            }

            textBuilder.AppendLine("\nendmodule");

            return textBuilder.ToString();
        }
    }
}
