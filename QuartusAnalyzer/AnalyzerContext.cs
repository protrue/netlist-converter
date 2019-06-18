using VerilogNetlistModel;

namespace QuartusAnalyzer
{
    public class AnalyzerContext
    {
        public AnalyzerState AnalyzerState { get; set; }

        public Module Module { get; set; }

        public Instance Instance { get; set; }

        public AnalyzerContext()
        {
            AnalyzerState = AnalyzerState.Default;
            Module = null;
            Instance = null;
        }
    }
}
