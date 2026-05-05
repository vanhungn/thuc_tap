using System.ComponentModel.DataAnnotations;

namespace GrpcClient.model
{
    public class ModelSinhVien:ModelNguoi
    {
        public int Id { get; set; }
        public string MaSinhVien { get; set; } = "";

        [Required(ErrorMessage = "Vui lòng nhập địa chỉ")]
        public string DiaChi { get; set; } = "";

        [Range(1, int.MaxValue, ErrorMessage = "Vui lòng chọn lớp")]
        public int LopHocId
        {
            get => LopHoc?.Id ?? 0;
            set { if (LopHoc != null) LopHoc.Id = value; }
        }
        public ModelLopHoc LopHoc { get; set; }= new ModelLopHoc();
    }
}
