using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace baitap1.reponsitory
{
    internal class repositoryLopHoc
    {
        private SqlConnection conn;
        public repositoryLopHoc(SqlConnection conn)
        {
            this.conn = conn;
        }

        public List<LopHoc> Xem()
        {
           
            try
            {
                List<LopHoc> lopHocs = new List<LopHoc>();
                conn.Open();
                            
                string query = @"select * from lop_hoc join giao_vien on lop_hoc.idGv = giao_vien.GvId";
                SqlCommand cmd = new SqlCommand(query, conn);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    GiaoVien gv = new GiaoVien
                    {
                        id = Convert.ToInt32(reader["GvId"]),
                        MaGiaoVien = reader["MaGv"].ToString(),
                        Ten = reader["Ten"].ToString(),
                        NgaySinh = Convert.ToDateTime(reader["NgaySinh"])
                    };

                    LopHoc lh = new LopHoc
                    {
                        id = Convert.ToInt32(reader["LopId"]),
                        MaLopHoc = reader["MaLh"].ToString(),
                        TenLopHoc = reader["TenLop"].ToString(),
                        MonHoc = reader["MonHoc"].ToString(),
                        GiaoVien = gv
                    };
                    lopHocs.Add(lh);
                }
                return lopHocs;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                conn.Close();
            }
           
        }
    }
}
