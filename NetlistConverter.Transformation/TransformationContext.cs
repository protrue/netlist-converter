using System.Collections.Generic;

namespace NetlistConverter.Converter
{
    public class TransformationContext
    {
        public Dictionary<IInstanceTransformer, int> Counters { get; }

        public int NetCounter { get; private set; }

        public int GetNextInstanceNumber(IInstanceTransformer transformer)
        {
            if (!Counters.ContainsKey(transformer))
                Counters[transformer] = 0;

            return Counters[transformer]++;
        }

        public int GetNextNetNumber() => NetCounter++;

        public TransformationContext()
        {
            Counters = new Dictionary<IInstanceTransformer, int>();
            NetCounter = 0;
        }
    }
}
