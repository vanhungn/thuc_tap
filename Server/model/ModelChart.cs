using ProtoBuf;
namespace Server.model
{
    [ProtoContract]
    public class ModelChart
    {
        [ProtoMember(1)]
        public string Name { get; set; } = "";
        [ProtoMember(2)]
        public int SoLuongSinhVien { get; set; }
    }
}
