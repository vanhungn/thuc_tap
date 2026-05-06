using ProtoBuf;
namespace Server.model
{
    [ProtoContract]
    public class ModelGiaoVien:ModelNguoi
    {
        [ProtoMember(3)]
        public virtual int Id { get; set; }
        [ProtoMember(4)]
        public virtual string MaGiaoVien { get; set; } = "";
    }
}
