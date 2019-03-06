using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using VerilogObjectModel;

namespace QuartusAnalyzer
{
    public static class Analyzer
    {
        private static string[] PreProcess(string text)
        {
            text = text.RemoveAll("\t");
            var lines = text.Split(new[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries).ToList();
            lines.ForEach(l => l.Trim());

            lines.RemoveAll(l => l.StartsWith("//") || l.StartsWith("`"));

            return lines.ToArray();
        }

        private static QuartusScheme PostProcess(QuartusScheme quartusScheme)
        {
            foreach (var net in quartusScheme.Module.Nets)
            {
                net.Identifier = net.Identifier.Replace("~", "_");
                net.Identifier = net.Identifier.Replace(".", "_");
            }

            foreach (var moduleModuleInstantiation in quartusScheme.Module.ModuleInstantiations)
            {
                moduleModuleInstantiation.ModuleDescriptionIdentifier.Replace("~", "_");
                moduleModuleInstantiation.ModuleDescriptionIdentifier.Replace(".", "_");
                foreach (var port in moduleModuleInstantiation.Ports)
                {
                    port.Identifier.Replace("~", "_");
                    port.Identifier.Replace(".", "_");
                    port.ConnectedNet?.Identifier.Replace("~", "_");
                    port.ConnectedNet?.Identifier.Replace(".", "_");
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

                //string portId;
                //Net net;

                //switch (analyzerState)
                //{
                //    case AnalyzerState.Default:
                //        if (parts[0] == "module" && !line.Contains(";"))
                //        {
                //            moduleDescription = new ModuleDescription(parts[1]);
                //            analyzerState = AnalyzerState.ScanningModuleDescriptionPorts;
                //            continue;
                //        }

                //        if (parts[0] == "input" || parts[0] == "output")
                //        {
                //            portId = parts[1].RemoveAll(";");
                //            Enum.TryParse(parts[0], true, out NetType portType);
                //            moduleDescription.Nets.First(p => p.Identifier == portId).NetType = portType;
                //            continue;
                //        }

                //        var netTypeNames = Enum.GetNames(typeof(NetType)).Select(x => x.ToLower());
                //        if (netTypeNames.Contains(parts[0]))
                //        {
                //            Enum.TryParse(parts[0], true, out NetType netType);
                //            net = new Net(parts[1].RemoveFirst("\\").RemoveAll(";"), netType);
                //            moduleDescription.Nets.Add(net);
                //            continue;
                //        }

                //        if (parts[0] == "assign")
                //        {
                //            var rightPart = line.SubstringBetween("=", ";").RemoveFirst("\\").Trim();
                //            var rightPartNet = moduleDescription.Nets.FirstOrDefault(n => n.Identifier == rightPart);
                //            net = moduleDescription.Nets.FirstOrDefault(n => n.Identifier == parts[1]);

                //            if (rightPartNet != null)
                //            {
                //                net.Identifier = rightPartNet.Identifier;
                //                moduleDescription.Nets.Remove(rightPartNet);
                //            }
                //            else
                //                net.Value = rightPart;

                //            continue;
                //        }

                //        if (parts[0] == "initial")
                //            continue;

                //        if (parts[0] == "defparam")
                //        {
                //            var instantiationId = parts[1].RemoveFirst("\\");
                //            var paramId = parts[2].RemoveFirst(".");
                //            var paramValue = parts[4].RemoveAll(";");
                //            var instantiation = moduleDescription.ModuleInstantiations
                //                .First(i => i.Identifier == instantiationId);
                //            instantiation.Parameters.Add(new ModuleParameter(paramId, paramValue));
                //            continue;
                //        }

                //        if (line.Contains("(") && !line.Contains(")"))
                //        {
                //            currentModuleInstantiation = new ModuleInstantiation(parts[0], parts[1].RemoveAll("\\").RemoveAll("("));
                //            analyzerState = AnalyzerState.ScanningModuleInstantiationPorts;
                //            continue;
                //        }

                //        continue;

                //    case AnalyzerState.ScanningModuleDescriptionPorts:
                //        if (parts[0].Contains(";"))
                //            analyzerState = AnalyzerState.Default;

                //        portId = parts[0].RemoveAll(",", ";", ")").Trim();

                //        moduleDescription.Nets.Add(new Net(portId));

                //        continue;

                //    case AnalyzerState.ScanningModuleInstantiationPorts:
                //        if (line.Contains(";"))
                //        {
                //            moduleDescription.ModuleInstantiations.Add(currentModuleInstantiation);
                //            analyzerState = AnalyzerState.Default;
                //        }

                //        portId = line.SubstringBetween(".", "(");
                //        var port = new Net(portId);
                //        string portArgument;
                //        if (line.Contains("{") && line.Contains("}"))
                //            portArgument = line.SubstringBetween("{", "}");
                //        else
                //            portArgument = line.SubstringBetween("(", ")").RemoveFirst("\\").TrimEnd();
                //        net = moduleDescription.Nets.FirstOrDefault(n => n.Identifier == portArgument);
                //        if (net != null)
                //            port.ConnectedNet = net;
                //        else
                //            port.Value = portArgument;

                //        currentModuleInstantiation.Ports.Add(port);

                //        continue;
                //}
            }

            var result = new QuartusScheme { Module = context.ModuleDescription };

            result = PostProcess(result);

            return result;
        }
    }
}
