namespace NetlistConverter.Analysis.StructureAnalyzers
{
    public class InitialAnalyzer : IStructureAnalyzer
    {
        public bool TryAnalyze(string line, string[] parts, AnalyzerContext context)
        {
            if (!(context.AnalyzerState == AnalyzerState.Default
                  && parts[0] == "initial")) return false;

            return true;
        }
    }
}
