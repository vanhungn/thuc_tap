using FluentNHibernate.Mapping;
using Server.model;

public class LopHocMap : ClassMap<ModelLopHoc>
{
    public LopHocMap()
    {
        Table("lop_hoc");

        Id(x => x.Id)
            .Column("LopId")
            .GeneratedBy.Identity();

        Map(x => x.TenLopHoc)
            .Column("TenLop");

        Map(x => x.MonHoc)
            .Column("MonHoc");

        Map(x => x.MaLopHoc)
            .Column("MaLh");

        // khóa ngoại tới giáo viên
        References(x => x.GiaoVien)
            .Column("idGv");
    }
}