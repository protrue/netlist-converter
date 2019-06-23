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
        static bool IsCorrectPath(string fileName)
        {
            FileInfo fileInfo = null;

            try
            {
                fileInfo = new FileInfo(fileName);
            }
            catch (ArgumentException) { }
            catch (PathTooLongException) { }
            catch (NotSupportedException) { }

            return !ReferenceEquals(fileInfo, null);
        }

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
                var parts = line.Split(new[] { " " }, StringSplitOptions.RemoveEmptyEntries);

                if (parts.Length != 3 || parts[0] != "convert")
                {
                    WriteUsage();
                    continue;
                }

                if (!File.Exists(parts[1]))
                {
                    Console.WriteLine("Input file not exists");
                    continue;
                }

                if (!IsCorrectPath(parts[2]))
                {
                    Console.WriteLine("Invalid output path");
                    continue;
                }

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
        }
    }
}
