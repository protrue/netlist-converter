using VerilogNetlistModel;

namespace NetlistConverter.Analysis.StructureAnalyzers
{
    public class InstanceAnalyzer : IStructureAnalyzer
    {
        public bool TryAnalyze(string line, string[] parts, AnalyzerContext context)
        {
            if (!(context.AnalyzerState == AnalyzerState.Default
                  && parts[0] != "module"
                  && line.Contains("(")
                  && !line.Contains(")"))) return false;

            context.Instance = new Instance(parts[0], parts[1].RemoveAll("\\").RemoveAll("("));
            context.AnalyzerState = AnalyzerState.ScanningInstancePorts;

            return true;
        }
    }
}
