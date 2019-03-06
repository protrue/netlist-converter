using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuartusAnalyzer
{
    public enum AnalyzerState
    {
        Default,
        ScanningModuleDescriptionPorts,
        ScanningModuleInstantiationPorts
    }
}
