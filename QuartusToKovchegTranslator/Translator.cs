using QuartusAnalyzer;
using KovchegSynthesizer;

namespace QuartusToKovchegTranslator
{
    public static class Translator
    {
        public static string LastQuartusText { get; private set; }
        public static QuartusScheme LastQuartusScheme { get; private set; }
        public static KovchegScheme LastKovchegScheme { get; private set; }
        public static string LastKovchegText { get; private set; }

        public static string Translate(string text)
        {
            LastQuartusText = text;
            LastQuartusScheme = Analyzer.Analyze(LastQuartusText);
            LastKovchegScheme = Synthesizer.SynthesizeScheme(LastQuartusScheme);
            LastKovchegText = Synthesizer.SynthesizeText(LastKovchegScheme);

            return LastKovchegText;
        }
    }
}
