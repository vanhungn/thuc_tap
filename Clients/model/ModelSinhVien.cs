namespace Clients.model
{
    public class ModelSinhVien:ModelNguoi
    {
        public int Id { get; set; }
        public string MaSinhVien { get; set; } = "";

        public string DiaChi { get; set; } = "";
        public ModelLopHoc LopHoc { get; set; }= new ModelLopHoc();
    }
}
