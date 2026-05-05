using System.ComponentModel.DataAnnotations;

namespace GrpcClient.model
{
    public class ModelNguoi
    {
        [Required(ErrorMessage = "Vui lòng nhập tên")]
        public string Ten { get; set; } = "";
        [Required(ErrorMessage = "Vui lòng chọn ngày sinh")]
        public DateTime NgaySinh { get; set; }
    }
}
