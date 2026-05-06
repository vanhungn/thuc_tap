using ProtoBuf;
namespace Server.model
{
    [ProtoContract]
    [ProtoInclude(100, typeof(ModelSinhVien))]
    [ProtoInclude(100,typeof(ModelGiaoVien))]
    public class ModelNguoi
    {

        [ProtoMember(1)]
        public virtual string Ten { get; set; } = "";
        [ProtoMember(2)]
        public virtual DateTime NgaySinh { get; set; }
    }
}
