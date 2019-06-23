using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VerilogNetlistModel;

namespace Generator
{
    public class NetlistGenerator
    {
        public string GenerateNetlist(Module scheme)
        {
            var textBuilder = new StringBuilder();

            var modulePorts = string.Join(", ", scheme.Ports.Where(p => p.NetType == NetType.Input || p.NetType == NetType.Output).Select(p => p.Identifier));
            textBuilder.Append($"module {scheme.Identifier} ({modulePorts});\n");
            foreach (var port in scheme.Ports)
            {
                textBuilder.AppendLine($"{port.NetType.ToString().ToLower()} {port.Identifier};");
            }

            textBuilder.AppendLine();

            foreach (var element in scheme.Instances)
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
