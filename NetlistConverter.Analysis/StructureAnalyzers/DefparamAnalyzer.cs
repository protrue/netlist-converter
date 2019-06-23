using System.Linq;
using VerilogNetlistModel;

namespace NetlistConverter.Analysis.StructureAnalyzers
{
    public class DefparamAnalyzer : IStructureAnalyzer
    {
        public bool TryAnalyze(string line, string[] parts, AnalyzerContext context)
        {
            if (!(context.AnalyzerState == AnalyzerState.Default
                  && parts[0] == "defparam")) return false;

            var instanceIdentifier = parts[1].RemoveFirst("\\");
            var parameterIdentifier = parts[2].RemoveFirst(".");
            var parameterValue = parts[4].RemoveAll(";");

            var instance = context
                            .Module
                            .Instances
                            .First(i => i.Identifier == instanceIdentifier);

            instance.Parameters.Add(new Parameter(parameterIdentifier, parameterValue));

            return true;
        }
    }
}
