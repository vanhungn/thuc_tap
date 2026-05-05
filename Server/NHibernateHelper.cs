using NHibernate;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using Server.Mappings;

public class NHibernateHelper
{
    public static ISessionFactory CreateSessionFactory(string connectionString)
    {
        try
        {
            return Fluently.Configure()
                .Database(
                    MsSqlConfiguration.MsSql2012
                        .ConnectionString(connectionString)
                        .Driver<NHibernate.Driver.SqlClientDriver>()   // 🔥 thêm
                        .Dialect<NHibernate.Dialect.MsSql2012Dialect>() // 🔥 thêm
                        .ShowSql()
                )
                .Mappings(m => m.FluentMappings.AddFromAssemblyOf<SinhVienMap>())
                .BuildSessionFactory();
        }
        catch (Exception ex)
        {
            Console.WriteLine("🔥 NHibernate ERROR:");
            Console.WriteLine(ex.ToString()); // 🔥 in lỗi thật
            throw;
        }
    }
}