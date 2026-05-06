using ProtoBuf;
namespace Server.model
{
    [ProtoContract]
    public class ModelSinhVien:ModelNguoi
    {
        [ProtoMember(3)]
        public virtual int Id { get; set; }
        [ProtoMember(4)]
        public virtual string MaSinhVien { get; set; } = "";
        [ProtoMember(5)]

        public virtual string DiaChi { get; set; } = "";
        [ProtoMember(6)]
        public virtual ModelLopHoc LopHoc { get; set; }= new ModelLopHoc();
    }
}
