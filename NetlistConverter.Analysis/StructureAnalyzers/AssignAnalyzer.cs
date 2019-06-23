using System.Linq;

namespace NetlistConverter.Analysis.StructureAnalyzers
{
    public class AssignAnalyzer : IStructureAnalyzer
    {
        public bool TryAnalyze(string line, string[] parts, AnalyzerContext context)
        {
            if (!(context.AnalyzerState == AnalyzerState.Default
                  && parts[0] == "assign")) return false;

            var leftNet = context.Module.Nets.First(n => n.Identifier == parts[1]);
            var rightPart = line.SubstringBetween("=", ";").RemoveFirst("\\").Trim();
            var rightNet = context.Module.Nets.FirstOrDefault(n => n.Identifier == rightPart);
            
            if (rightNet != null)
            {
                leftNet.Identifier = rightNet.Identifier;
                context.Module.Nets.Remove(rightNet);
            }
            else
            {
                leftNet.Value = rightPart;
            }

            return true;
        }
    }
}
