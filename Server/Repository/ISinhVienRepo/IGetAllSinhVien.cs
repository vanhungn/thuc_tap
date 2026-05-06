using Server_Shared.model;

namespace Server.Repository.ISinhVienRepo
{
    public interface IGetAllSinhVien
    {
        List<ModelSinhVien> GetAll();
    }
}
