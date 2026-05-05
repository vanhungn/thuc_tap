using System.ComponentModel.DataAnnotations;

namespace GrpcClient.model
{
    public class ModelLopHoc
    {
        [Required(ErrorMessage = "Vui lòng chọn lop hoc")]
        public int Id { get; set; }

        public string MaLopHoc { get; set; } = "";

       
        public string TenLopHoc { get; set; } = "";
        public string MonHoc { get; set; } = "";
        public ModelGiaoVien GiaoVien { get; set; }= new ModelGiaoVien();
    }
}
