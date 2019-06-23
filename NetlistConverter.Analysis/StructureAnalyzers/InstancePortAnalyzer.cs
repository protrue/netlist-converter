using System.Linq;
using VerilogNetlistModel;

namespace NetlistConverter.Analysis.StructureAnalyzers
{
    public class InstancePortAnalyzer : IStructureAnalyzer
    {
        public bool TryAnalyze(string line, string[] parts, AnalyzerContext context)
        {
            if (context.AnalyzerState != AnalyzerState.ScanningInstancePorts)
                return false;

            if (line.Contains(";"))
            {
                context.Module.Instances.Add(context.Instance);
                context.AnalyzerState = AnalyzerState.Default;
            }

            var portIdentifier = line.SubstringBetween(".", "(");
            var port = new Net(portIdentifier);
            string portValue;

            if (line.Contains("{") && line.Contains("}"))
            {
                portValue = line.SubstringBetween("{", "}");
            }
            else
            {
                portValue = line.SubstringBetween("(", ")").RemoveFirst("\\").TrimEnd();
                if (portValue.Length > 0 && portValue[0] == '!')
                {
                    port.IsConnectedNetNegated = true;
                    portValue = portValue.Remove(0, 1);
                }
            }

            var net = context.Module.Nets.FirstOrDefault(n => n.Identifier == portValue);
            if (net != null)
            {
                port.ConnectedNet = net;
            }
            else
            {
                port.Value = portValue;
            }

            context.Instance.Ports.Add(port);

            return true;
        }
    }
}
