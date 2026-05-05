using FluentNHibernate.Mapping;
using Server.model;

public class GiaoVienMap : ClassMap<ModelGiaoVien>
{
    public GiaoVienMap()
    {
        Table("giao_vien");

        Id(x => x.Id)
            .Column("GvId")
            .GeneratedBy.Identity();

        Map(x => x.MaGiaoVien)
            .Column("MaGv");

        Map(x => x.Ten)
            .Column("Ten");

        Map(x => x.NgaySinh)
            .Column("NgaySinh");
    }
}