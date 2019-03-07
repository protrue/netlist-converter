using VerilogObjectModel;

namespace QuartusAnalyzer
{
    public class QuartusScheme
    {
        public ModuleDescription ModuleDescription { get; set; }

        public QuartusScheme(ModuleDescription moduleDescription)
        {
            ModuleDescription = moduleDescription;
        }

        public override string ToString()
        {
            return ModuleDescription.ToString();
        }
    }
}
