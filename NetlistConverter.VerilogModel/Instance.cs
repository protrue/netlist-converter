using System.Collections.Generic;

namespace VerilogNetlistModel
{
    public class Instance : IIdentifiable
    {
        public string Identifier { get; set; }

        public string ModuleIdentifier { get; set; }

        public List<Net> Ports { get; set; }

        public List<Parameter> Parameters { get; set; }
        
        public Instance(string moduleIdentifier, string identifier)
        {
            ModuleIdentifier = moduleIdentifier;
            Identifier = identifier;
            Ports = new List<Net>();
            Parameters = new List<Parameter>();
        }

        public override string ToString() => $"{ModuleIdentifier} {Identifier} ({string.Join(" ", Ports)}) [{string.Join(" ", Parameters)}]";
    }
}
