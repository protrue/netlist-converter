using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KovchegSynthesizer
{
    public class SynthesisContext
    {
        private int _instanseCounter;
        private int _netCounter;

        public int InstanceCounter => _instanseCounter++;

        public int NetCounter => _netCounter++;

        public SynthesisContext()
        {
            _instanseCounter= 0;
            _netCounter = 0;
        }
    }
}
