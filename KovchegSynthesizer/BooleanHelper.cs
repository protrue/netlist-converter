using System;
using System.Collections.Generic;

namespace KovchegSynthesizer
{
    public class BooleanHelper
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
    }
}
