using VerilogObjectModel;

namespace QuartusAnalyzer
{
    public class QuartusScheme
    {
        public ModuleDescription Module { get; set; }

        public override string ToString()
        {
            return Module.ToString();
        }
    }
}
