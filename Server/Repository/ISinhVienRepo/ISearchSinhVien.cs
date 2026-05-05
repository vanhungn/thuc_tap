using Server.model;

namespace Server.Repository.ISinhVienRepo
{
    public interface ISearchSinhVien
    {
        ModelSinhVien GetByIdSinhVien(string masv);
    }
}
