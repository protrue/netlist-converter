using VerilogNetlistModel;

namespace NetlistConverter.Analysis
{
    public class AnalyzerContext
    {
        public AnalyzerState AnalyzerState { get; set; }

        public Module Module { get; set; }

        public Instance Instance { get; set; }

        public AnalyzerContext()
        {
            AnalyzerState = AnalyzerState.Default;
        }
    }
}
