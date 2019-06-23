using VerilogNetlistModel;

namespace NetlistConverter.Analysis.StructureAnalyzers
{
    public class ModuleHeadlineAnalyzer : IStructureAnalyzer
    {
        public bool TryAnalyze(string line, string[] parts, AnalyzerContext context)
        {
            if (!(context.AnalyzerState == AnalyzerState.Default
                  && parts[0] == "module"
                  && !line.Contains(";"))) return false;

            context.Module = new Module(parts[1]);
            context.AnalyzerState = AnalyzerState.ScanningModulePorts;
            return true;
        }
    }
}
