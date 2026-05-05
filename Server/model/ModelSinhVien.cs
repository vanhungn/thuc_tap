namespace Server.model
{
    public class ModelSinhVien:ModelNguoi
    {
        public virtual int Id { get; set; }
        public virtual string MaSinhVien { get; set; } = "";

        public virtual string DiaChi { get; set; } = "";
        public virtual ModelLopHoc LopHoc { get; set; }= new ModelLopHoc();
    }
}
