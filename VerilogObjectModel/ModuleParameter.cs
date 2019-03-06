namespace VerilogObjectModel
{
    public class ModuleParameter : VerilogElement
    {
        public string Value { get; set; }

        public ModuleParameter(string identifier, string value) : base(identifier)
        {
            Value = value;
        }

        public override string ToString() => $"{Identifier} = {Value}";
    }
}
