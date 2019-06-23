namespace NetlistConverter.Analysis
{
    public interface IStructureAnalyzer
    {
        bool TryAnalyze(string line, string[] parts, AnalyzerContext context);
    }
}
