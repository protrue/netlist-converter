using VerilogNetlistModel;

namespace QuartusAnalyzer
{
    public class QuartusScheme
    {
        public Module Module { get; set; }

        public QuartusScheme(Module module)
        {
            Module = module;
        }

        public override string ToString()
        {
            return Module.ToString();
        }
    }
}
