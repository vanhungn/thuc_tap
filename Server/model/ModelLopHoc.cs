using ProtoBuf;
namespace Server.model
{
    [ProtoContract]
    public class ModelLopHoc
    {
        [ProtoMember(1)]
        public virtual int Id { get; set; }
        [ProtoMember(2)]
        public virtual string MaLopHoc { get; set; } = "";
        [ProtoMember(3)]
        public virtual string TenLopHoc { get; set; } = "";
        [ProtoMember(4)]
        public virtual string MonHoc { get; set; } = "";
        [ProtoMember(5)]
        public virtual ModelGiaoVien GiaoVien { get; set; }= new ModelGiaoVien();
    }
}
