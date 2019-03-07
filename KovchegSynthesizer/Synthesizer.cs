using System.Linq;
using System.Text;
using QuartusAnalyzer;
using VerilogObjectModel;

namespace KovchegSynthesizer
{
    public static class Synthesizer
    {
        private static void PreProcess(QuartusScheme quartusScheme)
        {
            quartusScheme.ModuleDescription.Nets.RemoveAll(n =>
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

            var kovchegScheme = new KovchegScheme(new ModuleDescription(quartusScheme.ModuleDescription.Identifier));

            kovchegScheme.ModuleDescription.Nets.Add(new Net("gnd", NetType.Input));
            kovchegScheme.ModuleDescription.Nets.Add(new Net("vcc", NetType.Input));

            foreach (var modulePort in quartusScheme.ModuleDescription.Ports)
                kovchegScheme.ModuleDescription.Nets.Add(new Net(modulePort.Identifier, modulePort.NetType));

            foreach (var instance in quartusScheme.ModuleDescription.ModuleInstantiations)
                foreach (var converter in SynthesisRules.SynthesisRulesList)
                {
                    var conversionResult = converter(instance, context);
                    if (conversionResult == null) continue;
                    kovchegScheme.ModuleDescription.ModuleInstantiations.AddRange(conversionResult);
                    break;
                }

            return kovchegScheme;
        }

        public static string SynthesizeText(KovchegScheme scheme)
        {
            var textBuilder = new StringBuilder();

            var modulePorts = string.Join(", ", scheme.ModuleDescription.Ports.Where(p => p.NetType == NetType.Input || p.NetType == NetType.Output).Select(p => p.Identifier));
            textBuilder.Append($"module {scheme.ModuleDescription.Identifier} ({modulePorts});\n");
            foreach (var port in scheme.ModuleDescription.Ports)
            {
                textBuilder.AppendLine($"{port.NetType.ToString().ToLower()} {port.Identifier};");
            }

            textBuilder.AppendLine();

            foreach (var element in scheme.ModuleDescription.ModuleInstantiations)
            {
                var instancePorts = string.Join(", ",
                    element.Ports.Select(p => $".{p.Identifier}({p.ConnectedNet.Identifier})"));
                textBuilder.AppendLine($"{element.ModuleDescriptionIdentifier} {element.Identifier} ({instancePorts});");
            }

            textBuilder.AppendLine("\nendmodule");

            return textBuilder.ToString();
        }
    }
}
