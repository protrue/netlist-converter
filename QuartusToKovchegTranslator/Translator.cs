using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QuartusAnalyzer;
using KovchegSynthesizer;

namespace QuartusToKovchegTranslator
{
    public static class Translator
    {
        public static string LastListing;

        public static string Translate(string text)
        {
            var quartusScheme = Analyzer.Analyze(text);
            var kovchegScheme = Synthesizer.SynthesizeScheme(quartusScheme);
            var kovchegText = Synthesizer.SynthesizeText(kovchegScheme);

            LastListing = $"{quartusScheme}\n\n{kovchegScheme}\n\n{kovchegText}";

            return kovchegText;
        }
    }
}
