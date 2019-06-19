namespace VerilogNetlistModel
{
    public class Net : IIdentifiable
    {
        public string Identifier { get; set; }

        public NetType NetType { get; set; }

        public string Value { get; set; }

        public Net ConnectedNet { get; set; }

        public bool IsConnectedNetNegated { get; set; }

        public Net(string identifier, NetType netType = NetType.Unknown, Net connectedNet = null)
        {
            Identifier = identifier;
            NetType = netType;
            ConnectedNet = connectedNet;
        }

        public override string ToString() => $"{NetType} {Identifier} - {ConnectedNet?.Identifier} ({Value})";
    }
}
