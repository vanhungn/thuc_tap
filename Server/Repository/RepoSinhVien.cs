using Microsoft.Data.SqlClient;
using Server.model;
using System.Collections.Generic;
using NHibernate;
using NHibernate.Linq;


namespace Server.Repository
{
    public class RepoSinhVien: ISinhVienRepo.IGetChart, ISinhVienRepo.IGetAllSinhVien,ISinhVienRepo.IAddSinhVien,ISinhVienRepo.IDeleteSinhVien,ISinhVienRepo.IUpdateSinhVien,ISinhVienRepo.ISearchSinhVien
    {

        private readonly NHibernate.ISession _session;

        public RepoSinhVien(NHibernate.ISession session)
        {
            _session = session;
        }

        public List<ModelSinhVien> GetAll()
        {
            try
            {
                return _session.Query<ModelSinhVien>()
                               .Fetch(x => x.LopHoc)
                               .ThenFetch(x => x.GiaoVien)
                               .OrderByDescending(x => x.Ten)
                               .ToList();
            }
            catch (Exception ex)
            {
                Console.WriteLine("🔥 NH ERROR: " + ex.ToString());
                throw;
            }
        }
        public bool AddSinhVien(ModelSinhVien sv)
        {
            using var tran = _session.BeginTransaction();
            try
            {
                // Lưu sinh viên
                var id = (int)_session.Save(sv);

                // cập nhật MaSv
                sv.MaSinhVien = "SV" + id;
                _session.Update(sv);

                tran.Commit();
                return true;
            }
            catch (Exception)
            {
                tran.Rollback();
                throw;
            }
        }
        public bool DeleteSinhVien(int id)
        {
            using var tran = _session.BeginTransaction();
            try
            {
                var sv = _session.Get<ModelSinhVien>(id);
                if (sv == null) return false;

                _session.Delete(sv);
                tran.Commit();
                return true;
            }
            catch (Exception ex)
            {
                tran.Rollback();
                Console.WriteLine("🔥 NH ERROR: " + ex);
                throw;
            }
        }
        public bool UpdateSinhVien(ModelSinhVien sv)
        {
            using var tran = _session.BeginTransaction();
            try
            {
                _session.Update(sv);
                tran.Commit();
                return true;
            }
            catch (Exception ex)
            {
                tran.Rollback();
                if (ex.InnerException is SqlException sqlEx)
                {
                    // check tên constraint
                    if (sqlEx.Message.Contains("CK_NgaySinh"))
                    {
                        throw new Exception("Ngày sinh không hợp lệ!");
                    }
                }

                Console.WriteLine("🔥 NH ERROR: " + ex);
                throw;
            }
        }
        public ModelSinhVien GetByIdSinhVien(string masv)
        {
            try
            {
                return _session.Query<ModelSinhVien>()
                               .Fetch(x => x.LopHoc)
                               .ThenFetch(x => x.GiaoVien)
                               .FirstOrDefault(x => x.MaSinhVien == masv);
            }
            catch (Exception ex)
            {
                Console.WriteLine("🔥 NH ERROR: " + ex);
                throw;
            }
        }
        public List<ModelChart> GetChart()
        {
            try
            {
                var data = (from l in _session.Query<ModelLopHoc>()
                            join sv in _session.Query<ModelSinhVien>()
                            on l.Id equals sv.LopHoc.Id into g
                            select new ModelChart
                            {
                                Name = l.TenLopHoc,
                                SoLuongSinhVien = g.Count()
                            }).ToList();

                return data;
            }
            catch (Exception ex)
            {
                Console.WriteLine("🔥 NH ERROR: " + ex);
                throw;
            }
        }
    }
}
