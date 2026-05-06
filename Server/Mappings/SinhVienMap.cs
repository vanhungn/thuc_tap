using FluentNHibernate.Mapping;
using Server_Shared.model;

namespace Server.Mappings
{
    public class SinhVienMap:ClassMap<ModelSinhVien>
    {
        public SinhVienMap()
        {
            Table("sinh_vien");
            Id(x=>x.Id).Column("SvId").GeneratedBy.Identity();
            Map(x => x.MaSinhVien).Column("Masv");
            Map(x => x.Ten).Column("TenSv");
            Map(x => x.DiaChi).Column("DiaChi");
            Map(x => x.NgaySinh).Column("NgaySinhSv");
            References(x => x.LopHoc).Column("idLh").Not.Nullable();

        }
    }
}
