namespace Clients.model
{
    public class ModelLopHoc
    {
        public  int Id { get; set; }
        public string MaLopHoc { get; set; } = "";
        public string TenLopHoc { get; set; } = "";
        public string MonHoc { get; set; } = "";
        public ModelGiaoVien GiaoVien { get; set; }= new ModelGiaoVien();
    }
}
