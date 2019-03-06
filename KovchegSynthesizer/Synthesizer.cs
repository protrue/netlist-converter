using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using KovchegSynthesizer.KovchegElements;
using QuartusAnalyzer;
using VerilogObjectModel;

namespace KovchegSynthesizer
{
    public static class Synthesizer
    {
        public static bool[][] GenerateAllVariableValues(int variablesCount)
        {
            var rowsCount = (int)Math.Pow(2, variablesCount);
            var result = new bool[rowsCount][];

            for (var i = 0; i < rowsCount; i++)
                result[i] = new bool[variablesCount];

            var k = 1;
            for (var j = 3; j >= 0; j--)
            {
                var currentValue = false;
                for (var i = 0; i < 16; i++)
                {
                    result[i][j] = currentValue;
                    if ((i + 1) % k == 0) currentValue = !currentValue;
                }
                k *= 2;
            }

            return result;
        }

        public static bool[][] GetNormalFormVariableRows(bool[] function, bool isConjunctive)
        {
            var variableRows = GenerateAllVariableValues((int)Math.Log(function.Length, 2));
            var result = new List<bool[]>();

            for (var i = 0; i < function.Length; i++)
                if (isConjunctive && !function[i] || !isConjunctive && function[i])
                    result.Add(variableRows[i]);

            return result.ToArray();
        }

        //private static List<LibraryElement> CreateFromCombinationalCell(ModuleInstantiation combCell)
        //{
        //    var lutMaskAsString = combCell.Parameters.First(p => p.Identifier == "lut_mask").Value;
        //    var lutMask = ParseVerilogNumber(lutMaskAsString);
        //    var lutMaskAsBoolArray =
        //        Convert.ToString(lutMask, 2)
        //            .Select(c => c == '1')
        //            .Reverse()
        //            .ToArray();

        //    var function = new bool[16];
        //    for (var i = 0; i < lutMaskAsBoolArray.Length; i++)
        //        function[i] = lutMaskAsBoolArray[i];
        //    function = function.Reverse().ToArray();

        //    var isConjunctive = function.Count(v => v == false) < function.Count(v => v == true);
        //    var normalForm = GetNormalFormVariableRows(function, isConjunctive);

        //    var inversionNeeded = new bool[4];

        //    foreach (var row in normalForm)
        //    {
        //        for (var j = 0; j < row.Length; j++)
        //        {
        //            inversionNeeded[j] = isConjunctive && row[j] || inversionNeeded[j];
        //            inversionNeeded[j] = !isConjunctive && !row[j] || inversionNeeded[j];
        //        }
        //    }

        //    var result = new List<LibraryElement>();

        //    var netIdenifiers = new[] {"datad", "datac", "datab", "dataa"};

        //    for (var i = 0; i < inversionNeeded.Length; i++)
        //        if (inversionNeeded[i])
        //        {
        //            var input = combCell.Ports.First(x => x.Identifier == netIdenifiers[i]).ConnectedNet.Identifier;
        //            var element = new LibraryElement("INV", "inv_" + input + _globalInstanceCounter++);
        //            element.Ports.Add(new Net("I", NetType.Input, new Net(input, NetType.Wire)));
        //            element.Ports.Add(new Net("O", NetType.Output, new Net("n_" + _globalNetCounter++, NetType.Wire)));
        //            result.Add(element);
        //        }

        //    //if (inversionNeeded[0])
        //    //{
        //    //    var input = combCell.Ports.First(x => x.Identifier == "dataa").ConnectedNet.Identifier;
        //    //    var element = new LibraryElement("INV", "inv_" + input + _globalInstanceCounter++);
        //    //    element.Ports.Add(new Net("I", NetType.Input, new Net(input, NetType.Wire)));
        //    //    element.Ports.Add(new Net("O", NetType.Output, new Net("n_" + _globalNetCounter++, NetType.Wire)));
        //    //    result.Add(element);
        //    //}

        //    //if (inversionNeeded[1])
        //    //{
        //    //    var input = combCell.Ports.First(x => x.Identifier == "datab").ConnectedNet.Identifier;
        //    //    var element = new LibraryElement("INV", "inv_" + input + _globalInstanceCounter++);
        //    //    element.Ports.Add(new Net("I", NetType.Input, new Net(input, NetType.Wire)));
        //    //    element.Ports.Add(new Net("O", NetType.Output, new Net("n_" + _globalNetCounter++, NetType.Wire)));
        //    //    result.Add(element);
        //    //}

        //    //if (inversionNeeded[2])
        //    //{
        //    //    var input = combCell.Ports.First(x => x.Identifier == "datac").ConnectedNet.Identifier;
        //    //    var element = new LibraryElement("INV", "inv_" + input + _globalInstanceCounter++);
        //    //    element.Ports.Add(new Net("I", NetType.Input, new Net(input, NetType.Wire)));
        //    //    element.Ports.Add(new Net("O", NetType.Output, new Net("n_" + _globalNetCounter++, NetType.Wire)));
        //    //    result.Add(element);
        //    //}

        //    //if (inversionNeeded[3])
        //    //{
        //    //    var input = combCell.Ports.First(x => x.Identifier == "datad").ConnectedNet.Identifier;
        //    //    var element = new LibraryElement("INV", "inv_" + input + _globalInstanceCounter++);
        //    //    element.Ports.Add(new Net("I", NetType.Input, new Net(input, NetType.Wire)));
        //    //    element.Ports.Add(new Net("O", NetType.Output, new Net("n_" + _globalNetCounter++, NetType.Wire)));
        //    //    result.Add(element);
        //    //}

        //    var currentLevelElements = new List<LibraryElement>();

        //    for (int i = 0; i < normalForm.Length; i++)
        //    {
        //        var instancePorts = new string[4];

        //        for (var j = 0; j < 4; j++)
        //            instancePorts[j] = combCell.Ports.First(x => x.Identifier == netIdenifiers[j]).ConnectedNet.Identifier;

        //        LibraryElement element;

        //        if (isConjunctive)
        //        {
        //            element = new LibraryElement("OR4", "or4_" + _globalInstanceCounter++);

        //            //var instancePortA = combCell.Ports.First(x => x.Identifier == "dataa").ConnectedNet.Identifier;
        //            //var instancePortB = combCell.Ports.First(x => x.Identifier == "datab").ConnectedNet.Identifier;
        //            //var instancePortC = combCell.Ports.First(x => x.Identifier == "datac").ConnectedNet.Identifier;
        //            //var instancePortD = combCell.Ports.First(x => x.Identifier == "datad").ConnectedNet.Identifier;

        //            //if (normalForm[i][0])
        //            //    element.Ports.Add(new Net("I0", NetType.Input, result.First(e => e.Identifier.Contains(instancePortA)).Ports
        //            //        .First(p => p.Type == NetType.Output).ConnectedNet));
        //            //else
        //            //    element.Ports.Add(new Net("I0", NetType.Input, new Net(instancePortA, NetType.Wire)));

        //            //if (normalForm[i][1])
        //            //    element.Ports.Add(new Net("I1", NetType.Input, result.First(e => e.Identifier.Contains(instancePortB)).Ports
        //            //        .First(p => p.Type == NetType.Output).ConnectedNet));
        //            //else
        //            //    element.Ports.Add(new Net("I1", NetType.Input, new Net(instancePortB, NetType.Wire)));

        //            //if (normalForm[i][2])
        //            //    element.Ports.Add(new Net("I2", NetType.Input, result.First(e => e.Identifier.Contains(instancePortC)).Ports
        //            //        .First(p => p.Type == NetType.Output).ConnectedNet));
        //            //else
        //            //    element.Ports.Add(new Net("I2", NetType.Input, new Net(instancePortC, NetType.Wire)));

        //            //if (normalForm[i][3])
        //            //    element.Ports.Add(new Net("I3", NetType.Input, result.First(e => e.Identifier.Contains(instancePortD)).Ports
        //            //        .First(p => p.Type == NetType.Output).ConnectedNet));
        //            //else
        //            //    element.Ports.Add(new Net("I3", NetType.Input, new Net(instancePortD, NetType.Wire)));

        //            element.Ports.Add(new Net("O", NetType.Output, new Net(element.Identifier + "_" + _globalNetCounter++, NetType.Wire)));
        //            currentLevelElements.Add(element);
        //        }
        //        else
        //        {
        //            element = new LibraryElement("AND4", "and4_" + _globalInstanceCounter++);

        //            //var instancePortA = combCell.Ports.First(x => x.Identifier == "dataa").ConnectedNet.Identifier;
        //            //var instancePortB = combCell.Ports.First(x => x.Identifier == "datab").ConnectedNet.Identifier;
        //            //var instancePortC = combCell.Ports.First(x => x.Identifier == "datac").ConnectedNet.Identifier;
        //            //var instancePortD = combCell.Ports.First(x => x.Identifier == "datad").ConnectedNet.Identifier;

        //            //if (!normalForm[i][0])
        //            //    element.Ports.Add(new Net("I0", NetType.Input, result.First(e => e.Identifier.Contains(instancePortA)).Ports
        //            //        .First(p => p.Type == NetType.Output).ConnectedNet));
        //            //else
        //            //    element.Ports.Add(new Net("I0", NetType.Input, new Net(instancePortA, NetType.Wire)));

        //            //if (!normalForm[i][1])
        //            //    element.Ports.Add(new Net("I1", NetType.Input, result.First(e => e.Identifier.Contains(instancePortB)).Ports
        //            //        .First(p => p.Type == NetType.Output).ConnectedNet));
        //            //else
        //            //    element.Ports.Add(new Net("I1", NetType.Input, new Net(instancePortA, NetType.Wire)));

        //            //if (!normalForm[i][2])
        //            //    element.Ports.Add(new Net("I2", NetType.Input, result.First(e => e.Identifier.Contains(instancePortC)).Ports
        //            //        .First(p => p.Type == NetType.Output).ConnectedNet));
        //            //else
        //            //    element.Ports.Add(new Net("I2", NetType.Input, new Net(instancePortA, NetType.Wire)));

        //            //if (!normalForm[i][3])
        //            //    element.Ports.Add(new Net("I3", NetType.Input, result.First(e => e.Identifier.Contains(instancePortD)).Ports
        //            //        .First(p => p.Type == NetType.Output).ConnectedNet));
        //            //else
        //            //    element.Ports.Add(new Net("I3", NetType.Input, new Net(instancePortA, NetType.Wire)));
        //        }

        //        for (var j = 0; j < 4; j++)
        //            if (normalForm[i][j])
        //                element.Ports.Add(new Net("I0", NetType.Input, result.First(e => e.Identifier.Contains(instancePorts[j])).Ports
        //                    .First(p => p.Type == NetType.Output).ConnectedNet));
        //            else
        //                element.Ports.Add(new Net("I0", NetType.Input, new Net(instancePorts[j], NetType.Wire)));

        //        element.Ports.Add(new Net("O", NetType.Output, new Net(element.Identifier + "_" + _globalNetCounter++, NetType.Wire)));
        //        currentLevelElements.Add(element);
        //    }

        //    if (currentLevelElements.Count == 1)
        //    {
        //        result.AddRange(currentLevelElements);
        //    }

        //    while (currentLevelElements.Count != 1)
        //    {
        //        var nextLevelElements = new List<LibraryElement>();
        //        if (currentLevelElements.Count % 2 == 1)
        //        {
        //            nextLevelElements.Add(currentLevelElements.Last());
        //            currentLevelElements.RemoveAt(currentLevelElements.Count - 1);
        //        }

        //        for (var i = 0; i < currentLevelElements.Count; i += 2)
        //        {
        //            LibraryElement element;

        //            if (isConjunctive)
        //                element = new LibraryElement("AND2", "and2_" + _globalInstanceCounter++);
        //            else
        //                element = new LibraryElement("OR2", "or2_" + _globalInstanceCounter++);

        //            element.Ports.Add(new Net("I0", NetType.Input, currentLevelElements[i].Ports.First(p => p.Type == NetType.Output).ConnectedNet));
        //            element.Ports.Add(new Net("I1", NetType.Input, currentLevelElements[i + 1].Ports.First(p => p.Type == NetType.Output).ConnectedNet));
        //            element.Ports.Add(new Net("O", NetType.Output, new Net("n_" + _globalNetCounter++, NetType.Wire)));
        //            nextLevelElements.Add(element);
        //        }

        //        result.AddRange(currentLevelElements);
        //        currentLevelElements = new List<LibraryElement>(nextLevelElements);
        //    }

        //    result.Last().Ports.First(p => p.Identifier == "O").ConnectedNet = new Net(
        //        combCell.Ports.First(p => p.Identifier == "combout").ConnectedNet
        //            .Identifier, NetType.Wire);

        //    return result;
        //}

        //private static LibraryElement CreateFromDffeas(ModuleInstantiation dffeas)
        //{
        //    var fdc = new LibraryElement("FDC", "fdc_" + _globalInstanceCounter++);

        //    var clock = dffeas.Ports.First(p => p.Identifier == "clk").ConnectedNet.Identifier;
        //    var clr = dffeas.Ports.First(p => p.Identifier == "clrn").ConnectedNet.Identifier;
        //    var dataInput = dffeas.Ports.First(p => p.Identifier == "d").ConnectedNet.Identifier;
        //    var dataOutput = dffeas.Ports.First(p => p.Identifier == "q").ConnectedNet.Identifier;

        //    fdc.Ports.Add(new Net("C", NetType.Input, new Net("clock", NetType.Wire)));
        //    fdc.Ports.Add(new Net("CLR", NetType.Input, new Net(clr, NetType.Wire)));
        //    fdc.Ports.Add(new Net("D", NetType.Input, new Net(dataInput, NetType.Wire)));
        //    fdc.Ports.Add(new Net("Q", NetType.Output, new Net(dataOutput, NetType.Wire)));

        //    return fdc;
        //}

        //private static LibraryElement CreateFromBuffer(ModuleInstantiation buffer)
        //{
        //    var input = buffer.Ports.First(p => p.Identifier == "i").ConnectedNet.Identifier;
        //    var output = buffer.Ports.First(p => p.Identifier == "o").ConnectedNet.Identifier;

        //    var kovchegBuffer = new LibraryElement("BUF", "buf_" + _globalInstanceCounter++);

        //    kovchegBuffer.Ports.Add(new Net("I", NetType.Input, new Net(input, NetType.Wire)));
        //    kovchegBuffer.Ports.Add(new Net("O", NetType.Output, new Net(output, NetType.Wire)));

        //    return kovchegBuffer;
        //}

        //private static List<LibraryElement> SynthesizeFrom(ModuleInstantiation instance)
        //{
        //    var result = new List<LibraryElement>();

        //    if (instance.ModuleIdentifier.Contains("lcell_comb"))
        //    {
        //        return CreateFromCombinationalCell(instance);
        //    }

        //    if (instance.ModuleIdentifier.Contains("dffeas"))
        //    {
        //        return new List<LibraryElement>(new[] { CreateFromDffeas(instance) });
        //    }

        //    if (instance.ModuleIdentifier.Contains("io_obuf") || instance.ModuleIdentifier.Contains("io_ibuf"))
        //    {
        //        return new List<LibraryElement>(new[] { CreateFromBuffer(instance) });
        //    }

        //    return null;
        //}

        private static void PreProcess(QuartusScheme quartusScheme)
        {
            quartusScheme.Module.Nets.RemoveAll(n =>
                n.NetType != NetType.Input &&
                n.NetType != NetType.Output &&
                n.NetType != NetType.Wire ||
                n.Identifier == "gnd" ||
                n.Identifier == "vcc");
        }

        public static KovchegScheme SynthesizeScheme(QuartusScheme quartusScheme)
        {
            PreProcess(quartusScheme);

            var context = new SynthesisContext();

            var kovchegScheme = new KovchegScheme(new ModuleDescription(quartusScheme.Module.Identifier));

            kovchegScheme.ModuleDescription.Nets.Add(new Net("gnd", NetType.Input));
            kovchegScheme.ModuleDescription.Nets.Add(new Net("vcc", NetType.Input));

            foreach (var modulePort in quartusScheme.Module.Ports)
                kovchegScheme.ModuleDescription.Nets.Add(new Net(modulePort.Identifier, modulePort.NetType));

            foreach (var instance in quartusScheme.Module.ModuleInstantiations)
                foreach (var converter in SynthesisRules.SynthesisRulesList)
                {
                    var conversionResult = converter(instance, context);
                    if (conversionResult == null) continue;
                    kovchegScheme.ModuleDescription.ModuleInstantiations.AddRange(conversionResult);
                    break;
                }

            return kovchegScheme;
        }

        public static string SynthesizeText(KovchegScheme scheme)
        {
            var textBuilder = new StringBuilder();

            var modulePorts = string.Join(", ", scheme.ModuleDescription.Ports.Where(p => p.NetType == NetType.Input || p.NetType == NetType.Output).Select(p => p.Identifier));
            textBuilder.Append($"module {scheme.ModuleDescription.Identifier} ({modulePorts});\n");
            foreach (var port in scheme.ModuleDescription.Ports)
            {
                textBuilder.AppendLine($"{port.NetType.ToString().ToLower()} {port.Identifier};");
            }

            textBuilder.AppendLine();

            foreach (var element in scheme.ModuleDescription.ModuleInstantiations)
            {
                var instancePorts = string.Join(", ",
                    element.Ports.Select(p => $".{p.Identifier}({p.ConnectedNet.Identifier})"));
                textBuilder.AppendLine($"{element.ModuleDescriptionIdentifier} {element.Identifier} ({instancePorts});");
            }

            textBuilder.AppendLine("\nendmodule");

            return textBuilder.ToString();
        }
    }
}
