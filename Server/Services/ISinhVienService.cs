
using Server.model;

namespace Server.Services
{
    public interface ISinhVienService
    {
        List<ModelSinhVien> GetAll();
        bool AddSinhVien(ModelSinhVien sv);
        bool DeleteSinhVien(int id);
        bool UpdateSinhVien(ModelSinhVien sv);
        ModelSinhVien GetByIdSinhVien(string masv);
        List<ModelChart> GetAllChart();
    }
}
