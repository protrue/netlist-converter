namespace VerilogNetlistModel
{
    public class Parameter : IIdentifiable
    {
        public string Identifier { get; set; }

        public string Value { get; set; }

        public Parameter(string identifier, string value)
        {
            Identifier = identifier;
            Value = value;
        }

        public override string ToString() => $"{Identifier} = {Value}";
    }
}
