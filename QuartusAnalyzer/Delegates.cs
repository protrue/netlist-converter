using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuartusAnalyzer
{
    public delegate bool LineParser(string line, string[] parts, AnalyzerContext context);
}
