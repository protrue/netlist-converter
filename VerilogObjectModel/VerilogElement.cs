namespace VerilogObjectModel
{
    public abstract class VerilogElement
    {
        public string Identifier { get; set; }

        protected VerilogElement(string identifier)
        {
            Identifier = identifier;
        }
    }
}
