using Microsoft.Data.SqlClient;
using Server.model;
using NHibernate;
using NHibernate.Linq;
namespace Server.Repository
{
    public class RepoLopHoc:ILopHocRepo.IGetAllLopHoc
    {
        private readonly NHibernate.ISession _session;

        public RepoLopHoc(NHibernate.ISession session)
        {
            _session = session;
        }
        public List<ModelLopHoc> GetAllLopHoc()
        {
            try
            {
                return _session.Query<ModelLopHoc>()
                               .Fetch(x => x.GiaoVien)
                               .ToList();
            }
            catch (Exception ex)
            {
                Console.WriteLine("🔥 NH ERROR: " + ex);
                throw;
            }
        }
    }
}
