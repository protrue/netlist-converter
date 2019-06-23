using System;
using System.Linq;
using VerilogNetlistModel;

namespace NetlistConverter.Analysis.StructureAnalyzers
{
    public class PortsAnalyzer : IStructureAnalyzer
    {
        public bool TryAnalyze(string line, string[] parts, AnalyzerContext context)
        {
            if (!(context.AnalyzerState == AnalyzerState.Default
                  && (parts[0] == "input" || parts[0] == "output"))) return false;

            var portId = parts[1].RemoveAll(";");
            Enum.TryParse(parts[0], true, out NetType portType);
            context.Module.Nets.First(p => p.Identifier == portId).NetType = portType;

            return true;
        }
    }
}
