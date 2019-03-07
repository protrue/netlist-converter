using System.Collections.Generic;

namespace VerilogObjectModel
{
    public class ModuleInstantiation : VerilogElement
    {
        public string ModuleDescriptionIdentifier { get; set; }

        public List<Net> Ports { get; set; }

        public List<ModuleParameter> Parameters { get; set; }

        public ModuleInstantiation(string moduleDescriptionIdentifier, string identifier) : base(identifier)
        {
            ModuleDescriptionIdentifier = moduleDescriptionIdentifier;
            Identifier = identifier;
            Ports = new List<Net>();
            Parameters = new List<ModuleParameter>();
        }

        public override string ToString() => $"{ModuleDescriptionIdentifier} {Identifier} ({string.Join(" ", Ports)}) [{string.Join(" ", Parameters)}]";
    }
}
