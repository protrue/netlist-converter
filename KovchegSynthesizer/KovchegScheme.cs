using VerilogObjectModel;

namespace KovchegSynthesizer
{
    public class KovchegScheme
    {
        public ModuleDescription ModuleDescription { get; set; }

        public KovchegScheme(ModuleDescription moduleDescription)
        {
            ModuleDescription = moduleDescription;
        }

        public override string ToString()
        {
            return ModuleDescription.ToString();
        }
    }
}
