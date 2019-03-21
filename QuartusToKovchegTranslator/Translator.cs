using System;
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

            try
            {
                LastQuartusScheme = Analyzer.Analyze(LastQuartusText);
            }
            catch (Exception exception)
            {
                throw new Exception("Ошибка при разборе файла схемы из Quartus", exception);
            }

            try
            {
                LastKovchegScheme = Synthesizer.SynthesizeScheme(LastQuartusScheme);
                LastKovchegText = Synthesizer.SynthesizeText(LastKovchegScheme);
            }
            catch (Exception exception)
            {
                throw new Exception("Ошибка при конвертации схемы", exception);
            }
            
            return LastKovchegText;
        }
    }
}
