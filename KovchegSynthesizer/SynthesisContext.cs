namespace KovchegSynthesizer
{
    public class SynthesisContext
    {
        private int _instanceCounter;
        private int _netCounter;

        public int InstanceCounter => _instanceCounter++;

        public int NetCounter => _netCounter++;

        public SynthesisContext()
        {
            _instanceCounter= 0;
            _netCounter = 0;
        }
    }
}
