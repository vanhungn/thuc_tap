using baitap1.reponsitory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace baitap1.service
{
    internal class serviceSinhVien
    {
        public interface ISinhVienService
        {
            List<SinhVien> GetAll(string sort);
            bool them(SinhVien sv);
            bool sua(SinhVien sv);
            bool xoa(int id);
        }
       
        public class SinhVienService : ISinhVienService
        {
            private readonly repositorySinhVien repo;

            public SinhVienService(repositorySinhVien repo)
            {
                this.repo = repo;
            }

            public List<SinhVien> GetAll(string sort)
            {
                return repo.Xem(sort);
            }
            public bool them( SinhVien sv)
            {
                return repo.Them(sv);
            }
            public bool sua(SinhVien sv)
            {
                return repo.Sua(sv);
            }
            public bool xoa(int id) {
                return repo.Xoa(id);
            }
        }
    }
}
