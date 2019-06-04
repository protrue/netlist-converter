using System;
using System.Linq;

namespace QuartusAnalyzer
{
    public static class Analyzer
    {
        private static string[] PreProcess(string text)
        {
            text = text.RemoveAll("\t");
            var lines = text.Split(new[] { Environment.NewLine, "\n" }, StringSplitOptions.RemoveEmptyEntries).ToList();
            lines = lines.Select(l => l.Trim().Replace("~", "_")).ToList();
            
            lines.RemoveAll(l => l.StartsWith("//") || l.StartsWith("`"));

            return lines.ToArray();
        }

        private static QuartusScheme PostProcess(QuartusScheme quartusScheme)
        {
            foreach (var net in quartusScheme.ModuleDescription.Nets)
            {
                net.Identifier = net.Identifier.Replace("~", "_");
                net.Identifier = net.Identifier.Replace(".", "_");
            }

            foreach (var moduleModuleInstantiation in quartusScheme.ModuleDescription.ModuleInstantiations)
            {
                moduleModuleInstantiation.ModuleDescriptionIdentifier = moduleModuleInstantiation.ModuleDescriptionIdentifier.Replace("~", "_");
                moduleModuleInstantiation.ModuleDescriptionIdentifier = moduleModuleInstantiation.ModuleDescriptionIdentifier.Replace(".", "_");
                foreach (var port in moduleModuleInstantiation.Ports)
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
            
            return quartusScheme;
        }

        public static QuartusScheme Analyze(string text)
        {
            var lines = PreProcess(text);

            var context = new AnalyzerContext();

            foreach (var line in lines)
            {
                var parts = line.Split(' ');

                if (parts[0] == "endmodule")
                    break;

                foreach (var lineParser in ParsingRules.ParsingRulesDictionary[context.AnalyzerState])
                    if (lineParser(line, parts, context))
                        break;
            }

            var result = new QuartusScheme(context.ModuleDescription);

            result = PostProcess(result);

            return result;
        }
    }
}
