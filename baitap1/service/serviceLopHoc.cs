using baitap1.reponsitory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace baitap1.service
{
    internal class serviceLopHoc
    {
        public interface ILopHocService
        {
            List<LopHoc> GetAll();
        }
        public class LopHocService : ILopHocService 
        {
            private readonly repositoryLopHoc repo;
            public LopHocService(repositoryLopHoc repo)
            {
                this.repo = repo;
            }
            public List<LopHoc> GetAll()
            {
                return repo.Xem();
            }
        }
    }
}
