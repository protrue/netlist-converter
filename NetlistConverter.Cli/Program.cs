using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Generator;
using NetlistConverter.Analysis;
using NetlistConverter.Converter;

namespace Cli
{
    class Program
    {
        static void WriteUsage()
        {
            Console.WriteLine("Usage: convert <input path> <output path>");
        }

        static void Main(string[] args)
        {
            WriteUsage();
            while (true)
            {
                var line = Console.ReadLine();
                var parts = line.Split(new[] {" "}, StringSplitOptions.RemoveEmptyEntries);
                if (parts.Length > 2 && parts[0].ToLower() == "convert" && File.Exists(parts[1]) && Uri.IsWellFormedUriString(parts[2], UriKind.RelativeOrAbsolute))
                {
                    var quartusNetlist = File.ReadAllText(parts[1]);
                    var analyzer = new NetlistAnalyzer();
                    var quartusScheme = analyzer.Analyze(quartusNetlist);
                    var transformer = new NetlistTransformer();
                    var kovchegScheme = transformer.Transform(quartusScheme);
                    var generator = new NetlistGenerator();
                    var kovchegNetlist = generator.GenerateNetlist(kovchegScheme);
                    File.WriteAllText(parts[2], kovchegNetlist);
                    Console.WriteLine("Success!");
                }
                else
                {
                    WriteUsage();
                }
            }
        }
    }
}
