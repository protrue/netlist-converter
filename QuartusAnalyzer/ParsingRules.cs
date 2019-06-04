using System;
using System.Collections.Generic;
using System.Linq;
using VerilogObjectModel;

namespace QuartusAnalyzer
{
    public static class ParsingRules
    {
        public static Dictionary<AnalyzerState, List<LineParser>> ParsingRulesDictionary;

        static ParsingRules()
        {
            ParsingRulesDictionary = new Dictionary<AnalyzerState, List<LineParser>>
            {
                { AnalyzerState.Default, new List<LineParser>
                {
                    (line, parts, context) =>
                    {
                        if (!(parts[0] == "module" && !line.Contains(";"))) return false;

                        context.ModuleDescription = new ModuleDescription(parts[1]);
                        context.AnalyzerState = AnalyzerState.ScanningModuleDescriptionPorts;
                        return true;
                    },

                    (line, parts, context) =>
                    {
                        if (!(parts[0] == "input" || parts[0] == "output")) return false;

                        var portId = parts[1].RemoveAll(";");
                        Enum.TryParse(parts[0], true, out NetType portType);
                        context.ModuleDescription.Nets.First(p => p.Identifier == portId).NetType = portType;

                        return true;
                    },

                    (line, parts, context) =>
                    {
                        var netTypeNames = Enum.GetNames(typeof(NetType)).Select(x => x.ToLower());

                        if (!netTypeNames.Contains(parts[0])) return false;

                        Enum.TryParse(parts[0], true, out NetType netType);
                        var net = new Net(parts[1].RemoveFirst("\\").RemoveAll(";"), netType);
                        context.ModuleDescription.Nets.Add(net);

                        return true;
                    },

                    (line, parts, context) =>
                    {
                        if (parts[0] != "assign") return false;

                        var rightPart = line.SubstringBetween("=", ";").RemoveFirst("\\").Trim();
                        var rightPartNet = context.ModuleDescription.Nets.FirstOrDefault(n => n.Identifier == rightPart);
                        var net = context.ModuleDescription.Nets.First(n => n.Identifier == parts[1]);

                        if (rightPartNet != null)
                        {
                            net.Identifier = rightPartNet.Identifier;
                            context.ModuleDescription.Nets.Remove(rightPartNet);
                        }
                        else
                            net.Value = rightPart;

                        return true;
                    },

                    (line, parts, context) =>
                    {
                        if (parts[0] != "initial") return false;

                        return true;
                    },

                    (line, parts, context) =>
                    {
                        if (parts[0] != "defparam") return false;
                        var instantiationId = parts[1].RemoveFirst("\\");
                        var paramId = parts[2].RemoveFirst(".");
                        var paramValue = parts[4].RemoveAll(";");
                        var instantiation = context.ModuleDescription.ModuleInstantiations
                            .First(i => i.Identifier == instantiationId);
                        instantiation.Parameters.Add(new ModuleParameter(paramId, paramValue));
                        
                        return true;
                    },

                    (line, parts, context) =>
                    {
                        if (!(line.Contains("(") && !line.Contains(")"))) return false;

                        context.ModuleInstantiation = new ModuleInstantiation(parts[0], parts[1].RemoveAll("\\").RemoveAll("("));
                        context.AnalyzerState = AnalyzerState.ScanningModuleInstantiationPorts;
                        return true;
                    }
                } },

                { AnalyzerState.ScanningModuleDescriptionPorts, new List<LineParser>
                {
                    (line, parts, context) =>
                    {
                        if (parts[0].Contains(";"))
                            context.AnalyzerState = AnalyzerState.Default;

                        var portId = parts[0].RemoveAll(",", ";", ")").Trim();

                        context.ModuleDescription.Nets.Add(new Net(portId));

                        return true;
                    }
                } },

                {AnalyzerState.ScanningModuleInstantiationPorts, new List<LineParser>
                {
                    ((line, parts, context) =>
                    {
                        if (line.Contains(";"))
                        {
                            context.ModuleDescription.ModuleInstantiations.Add(context.ModuleInstantiation);
                            context.AnalyzerState = AnalyzerState.Default;
                        }

                        var portId = line.SubstringBetween(".", "(");
                        var port = new Net(portId);
                        string portArgument;
                        if (line.Contains("{") && line.Contains("}"))
                            portArgument = line.SubstringBetween("{", "}");
                        else
                        {
                            portArgument = line.SubstringBetween("(", ")").RemoveFirst("\\").TrimEnd();
                            if (portArgument.Length > 0 && portArgument[0] == '!')
                            {
                                port.IsConnectedNetNegated = true;
                                portArgument = portArgument.Remove(0, 1);
                            }
                        }

                        var net = context.ModuleDescription.Nets.FirstOrDefault(n => n.Identifier == portArgument);
                        if (net != null)
                            port.ConnectedNet = net;
                        else
                            port.Value = portArgument;

                        context.ModuleInstantiation.Ports.Add(port);

                        return true;
                    })
                } }
            };
        }
    }
}
