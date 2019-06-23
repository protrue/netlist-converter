using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VerilogNetlistModel;

namespace NetlistConverter.Converter.InstanceTransformers
{
    public class LogicElementTransformer : IInstanceTransformer
    {
        public bool[][] GenerateAllVariableValues(int variablesCount)
        {
            var rowsCount = (int)Math.Pow(2, variablesCount);
            var result = new bool[rowsCount][];

            for (var i = 0; i < rowsCount; i++)
                result[i] = new bool[variablesCount];

            var k = 1;
            for (var j = variablesCount - 1; j >= 0; j--)
            {
                var currentValue = false;
                for (var i = 0; i < rowsCount; i++)
                {
                    result[i][j] = currentValue;
                    if ((i + 1) % k == 0) currentValue = !currentValue;
                }
                k *= 2;
            }

            return result;
        }

        public bool[][] GetNormalFormVariableRows(bool[] function, bool isConjunctive)
        {
            var variableRows = GenerateAllVariableValues((int)Math.Log(function.Length, 2));
            var result = new List<bool[]>();

            for (var i = 0; i < function.Length; i++)
                if (isConjunctive && !function[i] || !isConjunctive && function[i])
                    result.Add(variableRows[i]);

            return result.ToArray();
        }

        public List<Instance> TryTransform(Instance instance, TransformationContext context)
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
            var normalForm = GetNormalFormVariableRows(function, isConjunctive)
                .Select(r => r.Reverse().ToArray()).ToArray();

            var inversionNeeded = new bool[4];

            foreach (var row in normalForm)
                for (var j = 0; j < row.Length; j++)
                    inversionNeeded[j] = !(isConjunctive && row[j] || !isConjunctive && !row[j]) || inversionNeeded[j];

            var result = new List<Instance>();

            //var netIdenifiers = new[] {"datad", "datac", "datab", "dataa"};
            var netIdenifiers = new[] { "dataa", "datab", "datac", "datad" };

            for (var i = 0; i < inversionNeeded.Length; i++)
                if (inversionNeeded[i])
                {
                    var input = instance.Ports.First(x => x.Identifier == netIdenifiers[i]).ConnectedNet
                        .Identifier;
                    var element = new Instance("INV", "inv_" + input + context.GetNextInstanceNumber(this));
                    element.Ports.Add(new Net("I", NetType.Input, new Net(input, NetType.Wire)));
                    element.Ports.Add(new Net("O", NetType.Output,
                        new Net("n_" + context.GetNextNetNumber(), NetType.Wire)));
                    result.Add(element);
                }

            var currentLevelElements = new List<Instance>();

            for (var i = 0; i < normalForm.Length; i++)
            {
                var instancePorts = new string[4];

                for (var j = 0; j < 4; j++)
                    instancePorts[j] = instance.Ports.First(x => x.Identifier == netIdenifiers[j]).ConnectedNet.Identifier;

                var element = isConjunctive ?
                    new Instance("OR4", "or4_" + context.GetNextInstanceNumber(this)) :
                    new Instance("AND4", "and4_" + context.GetNextInstanceNumber(this));

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
                    new Net(element.Identifier + "_" + context.GetNextNetNumber(), NetType.Wire)));
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
                        new Instance("AND2", "and2_" + context.GetNextInstanceNumber(this)) :
                        new Instance("OR2", "or2_" + context.GetNextInstanceNumber(this));

                    element.Ports.Add(new Net("I0", NetType.Input,
                        currentLevelElements[i].Ports.First(p => p.NetType == NetType.Output).ConnectedNet));
                    element.Ports.Add(new Net("I1", NetType.Input,
                        currentLevelElements[i + 1].Ports.First(p => p.NetType == NetType.Output).ConnectedNet));
                    element.Ports.Add(new Net("O", NetType.Output, new Net("n_" + context.GetNextNetNumber(), NetType.Wire)));

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
    }
}
