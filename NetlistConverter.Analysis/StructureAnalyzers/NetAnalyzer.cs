using System;
using System.Collections.Generic;
using System.Linq;
using VerilogNetlistModel;

namespace NetlistConverter.Analysis.StructureAnalyzers
{
    public class NetAnalyzer : IStructureAnalyzer
    {
        public readonly HashSet<string> NetTypeNames;

        public NetAnalyzer()
        {
            NetTypeNames = new HashSet<string>(Enum.GetNames(typeof(NetType)).Select(x => x.ToLower()));
        }

        public bool TryAnalyze(string line, string[] parts, AnalyzerContext context)
        {
            if (!(context.AnalyzerState == AnalyzerState.Default 
                  && NetTypeNames.Contains(parts[0]))) return false;

            Enum.TryParse(parts[0], true, out NetType netType);
            var net = new Net(parts[1].RemoveFirst("\\").RemoveAll(";"), netType);
            context.Module.Nets.Add(net);

            return true;
        }
    }
}
