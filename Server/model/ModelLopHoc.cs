namespace Server.model
{
    public class ModelLopHoc
    {
        public virtual int Id { get; set; }
        public virtual string MaLopHoc { get; set; } = "";
        public virtual string TenLopHoc { get; set; } = "";
        public virtual string MonHoc { get; set; } = "";
        public virtual ModelGiaoVien GiaoVien { get; set; }= new ModelGiaoVien();
    }
}
