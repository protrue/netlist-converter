using VerilogNetlistModel;

namespace KovchegSynthesizer
{
    public class KovchegScheme
    {
        public Module Module { get; set; }

        public KovchegScheme(Module module)
        {
            Module = module;
        }

        public override string ToString()
        {
            return Module.ToString();
        }
    }
}
