using Server_Shared.model;

namespace Server.Repository.ISinhVienRepo
{
    public interface ISearchSinhVien
    {
        ModelSinhVien GetByIdSinhVien(string masv);
    }
}
