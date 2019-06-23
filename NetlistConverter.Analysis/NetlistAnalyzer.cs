using System;
using System.Linq;
using System.Reflection.Emit;
using NetlistConverter.Analysis.StructureAnalyzers;
using VerilogNetlistModel;

namespace NetlistConverter.Analysis
{
    public class NetlistAnalyzer
    {
        public readonly IStructureAnalyzer[] StructureAnalyzers;

        public NetlistAnalyzer()
        {
            StructureAnalyzers = new IStructureAnalyzer[]
            {
                new AssignAnalyzer(),
                new DefparamAnalyzer(),
                new InitialAnalyzer(),
                new InstanceAnalyzer(),
                new InstancePortAnalyzer(),
                new ModuleHeadlineAnalyzer(),
                new ModulePortsAnalyzer(),
                new NetAnalyzer(),
                new PortsAnalyzer(),
            };
        }

        private string[] PreProcess(string text)
        {
            text = text.RemoveAll("\t");
            var lines = text.Split(new[] { Environment.NewLine, "\n" }, StringSplitOptions.RemoveEmptyEntries).ToList();
            lines = lines.Select(l => l.Trim().Replace("~", "_")).ToList();

            lines.RemoveAll(l => l.StartsWith("//") || l.StartsWith("`"));

            return lines.ToArray();
        }

        private Module PostProcess(Module scheme)
        {
            foreach (var net in scheme.Nets)
            {
                net.Identifier = net.Identifier.Replace("~", "_");
                net.Identifier = net.Identifier.Replace(".", "_");
            }

            foreach (var instance in scheme.Instances)
            {
                instance.ModuleIdentifier = instance.ModuleIdentifier.Replace("~", "_");
                instance.ModuleIdentifier = instance.ModuleIdentifier.Replace(".", "_");

                foreach (var port in instance.Ports)
                {
                    port.Identifier = port.Identifier.Replace("~", "_");
                    port.Identifier = port.Identifier.Replace(".", "_");

                    if (port.ConnectedNet != null)
                    {
                        port.ConnectedNet.Identifier = port.ConnectedNet.Identifier.Replace("~", "_");
                        port.ConnectedNet.Identifier = port.ConnectedNet.Identifier.Replace(".", "_");
                    }
                }
            }

            return scheme;
        }

        public Module Analyze(string netlist)
        {
            var lines = PreProcess(netlist);
            var context = new AnalyzerContext();
            
            foreach (var line in lines)
            {
                var parts = line.Split(' ');

                if (parts[0] == "endmodule")
                    break;

                foreach (var structureAnalyzer in StructureAnalyzers)
                {
                    var analyzeSuccess = structureAnalyzer.TryAnalyze(line, parts, context);

                    if (analyzeSuccess) break;
                }
                
            }

            var result = PostProcess(context.Module);

            return result;
        }
    }
}
