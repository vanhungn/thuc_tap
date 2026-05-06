using ProtoBuf;

namespace Server.model
{
    [ProtoContract]
    public class BoolResponse
    {
        [ProtoMember(1)]
        public bool Success { get; set; }

        [ProtoMember(2)]
        public string Message { get; set; } = "";
    }
}
