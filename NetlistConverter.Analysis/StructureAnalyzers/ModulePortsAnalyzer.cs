using VerilogNetlistModel;

namespace NetlistConverter.Analysis.StructureAnalyzers
{
    public class ModulePortsAnalyzer : IStructureAnalyzer
    {
        public bool TryAnalyze(string line, string[] parts, AnalyzerContext context)
        {
            if (context.AnalyzerState != AnalyzerState.ScanningModulePorts)
                return false;

            if (parts[0].Contains(";"))
                context.AnalyzerState = AnalyzerState.Default;

            var portId = parts[0].RemoveAll(",", ";", ")").Trim();

            context.Module.Nets.Add(new Net(portId));

            return true;
        }
    }
}
