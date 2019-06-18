using System;
using System.Collections.Generic;
using System.Linq;
using VerilogNetlistModel;

namespace KovchegSynthesizer
{
    public class SynthesisRules
    {
        public static List<Converter> SynthesisRulesList { get; }

        static SynthesisRules()
        {
            SynthesisRulesList = new List<Converter>()
            {
                (instance, context) =>
                {
                    if (!instance.ModuleIdentifier.Contains("dffeas")) return null;

                    var fdc = new Instance("FDC", "fdc_" + context.InstanceCounter);

                    var clock = instance.Ports.First(p => p.Identifier == "clk").ConnectedNet.Identifier;
                    var clr = instance.Ports.First(p => p.Identifier == "clrn").ConnectedNet.Identifier;
                    var dataInput = instance.Ports.First(p => p.Identifier == "d").ConnectedNet.Identifier;
                    var dataOutput = instance.Ports.First(p => p.Identifier == "q").ConnectedNet.Identifier;

                    fdc.Ports.Add(new Net("C", NetType.Input, new Net("clock", NetType.Wire)));
                    fdc.Ports.Add(new Net("CLR", NetType.Input, new Net(clr, NetType.Wire)));
                    fdc.Ports.Add(new Net("D", NetType.Input, new Net(dataInput, NetType.Wire)));
                    fdc.Ports.Add(new Net("Q", NetType.Output, new Net(dataOutput, NetType.Wire)));

                    return new List<Instance> {fdc};
                },

                (instance, context) =>
                {
                    if (!instance.ModuleIdentifier.Contains("io_ibuf") &&
                        !instance.ModuleIdentifier.Contains("io_obuf"))
                        return null;

                    var input = instance.Ports.First(p => p.Identifier == "i").ConnectedNet;
                    var output = instance.Ports.First(p => p.Identifier == "o").ConnectedNet;

                    var kovchegBuffer = new Instance("BUF", "buf_" + context.InstanceCounter);

                    var result = new List<ModuleInstantiation>();

                    var bufferInput = input;
                    if (instance.Ports.First(p => p.Identifier == "i").IsConnectedNetNegated)
                    {
                        var inverter = new ModuleInstantiation("INV", "inv_" + context.InstanceCounter);
                        inverter.Ports.Add(new Net("I", NetType.Input, new Net(input.Identifier, NetType.Wire)));
                        var inverterOutput = new Net("n_" + context.NetCounter, NetType.Wire);
                        inverter.Ports.Add(new Net("O", NetType.Input, inverterOutput));
                        bufferInput = inverterOutput;
                        result.Add(inverter);
                    }
                    
                    kovchegBuffer.Ports.Add(new Net("I", NetType.Input, bufferInput));
                    kovchegBuffer.Ports.Add(new Net("O", NetType.Output, new Net(output.Identifier, NetType.Wire)));
                    result.Add(kovchegBuffer);
                    
                    return result;
                },

                (instance, context) =>
                {
                    if (!instance.ModuleIdentifier.Contains("lcell_comb")) return null;

                    var lutMaskAsString = instance.Parameters.First(p => p.Identifier == "lut_mask").Value;
                    var lutMask = Tools.ParseVerilogNumber(lutMaskAsString);
                    var lutMaskAsBoolArray =
                        Convert.ToString(lutMask, 2)
                            .Select(c => c == '1')
                            .Reverse()
                            .ToArray();

                    var function = new bool[16];
                    for (var i = 0; i < lutMaskAsBoolArray.Length; i++)
                        function[i] = lutMaskAsBoolArray[i];
                    function = function.Reverse().ToArray();

                    var isConjunctive = function.Count(v => v == false) < function.Count(v => v == true);
                    var normalForm = BooleanHelper.GetNormalFormVariableRows(function, isConjunctive)
                        .Select(r => r.Reverse().ToArray()).ToArray();

                    var inversionNeeded = new bool[4];

                    foreach (var row in normalForm)
                        for (var j = 0; j < row.Length; j++)
                            inversionNeeded[j] = !(isConjunctive && row[j] || !isConjunctive && !row[j]) || inversionNeeded[j];

                    var result = new List<Instance>();

                    //var netIdenifiers = new[] {"datad", "datac", "datab", "dataa"};
                    var netIdenifiers = new[] {"dataa", "datab", "datac", "datad"};

                    for (var i = 0; i < inversionNeeded.Length; i++)
                        if (inversionNeeded[i])
                        {
                            var input = instance.Ports.First(x => x.Identifier == netIdenifiers[i]).ConnectedNet
                                .Identifier;
                            var element = new Instance("INV", "inv_" + input + context.InstanceCounter);
                            element.Ports.Add(new Net("I", NetType.Input, new Net(input, NetType.Wire)));
                            element.Ports.Add(new Net("O", NetType.Output,
                                new Net("n_" + context.NetCounter, NetType.Wire)));
                            result.Add(element);
                        }

                    var currentLevelElements = new List<Instance>();

                    for (var i = 0; i < normalForm.Length; i++)
                    {
                        var instancePorts = new string[4];

                        for (var j = 0; j < 4; j++)
                            instancePorts[j] = instance.Ports.First(x => x.Identifier == netIdenifiers[j]).ConnectedNet.Identifier;

                        var element = isConjunctive ?
                            new Instance("OR4", "or4_" + context.InstanceCounter) :
                            new Instance("AND4", "and4_" + context.InstanceCounter);

                        for (var j = 0; j < 4; j++)
                            if (!(normalForm[i][j] && isConjunctive || !normalForm[i][j] && !isConjunctive))
                                element.Ports.Add(new Net($"I{j}", NetType.Input,
                                    result.First(e => e.Identifier.Contains(instancePorts[j]))
                                        .Ports
                                        .First(p => p.NetType == NetType.Output).ConnectedNet));
                            else
                                element.Ports.Add(new Net($"I{j}", NetType.Input,
                                    new Net(instancePorts[j], NetType.Wire)));

                        element.Ports.Add(new Net("O", NetType.Output,
                            new Net(element.Identifier + "_" + context.NetCounter, NetType.Wire)));
                        currentLevelElements.Add(element);
                    }

                    if (currentLevelElements.Count == 1)
                    {
                        result.AddRange(currentLevelElements);
                    }

                    while (currentLevelElements.Count != 1)
                    {
                        var nextLevelElements = new List<Instance>();

                        if (currentLevelElements.Count % 2 == 1)
                        {
                            nextLevelElements.Add(currentLevelElements.Last());
                            currentLevelElements.RemoveAt(currentLevelElements.Count - 1);
                        }

                        for (var i = 0; i < currentLevelElements.Count; i += 2)
                        {
                            var element = isConjunctive ?
                                new Instance("AND2", "and2_" + context.InstanceCounter) :
                                new Instance("OR2", "or2_" + context.InstanceCounter);

                            element.Ports.Add(new Net("I0", NetType.Input,
                                currentLevelElements[i].Ports.First(p => p.NetType == NetType.Output).ConnectedNet));
                            element.Ports.Add(new Net("I1", NetType.Input,
                                currentLevelElements[i + 1].Ports.First(p => p.NetType == NetType.Output).ConnectedNet));
                            element.Ports.Add(new Net("O", NetType.Output, new Net("n_" + context.NetCounter, NetType.Wire)));

                            nextLevelElements.Add(element);
                        }

                        result.AddRange(currentLevelElements);
                        //if (currentLevelElements.Count == 0) break;
                        currentLevelElements = new List<Instance>(nextLevelElements);
                    }

                    result.AddRange(currentLevelElements);

                    result.Last().Ports.First(p => p.Identifier == "O").ConnectedNet =
                        new Net(instance.Ports.First(p => p.Identifier == "combout").ConnectedNet.Identifier, NetType.Wire);

                    return result;
                }
            };
        }
    }
}