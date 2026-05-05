using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace baitap1.reponsitory
{
    internal class repositorySinhVien
    {
        private SqlConnection conn;

        public repositorySinhVien(SqlConnection conn)
        {
            this.conn = conn;
        }
      
        public List<SinhVien> Xem(string sort)
        {
          
            try
            {
                List<SinhVien> sinhViens = new List<SinhVien>();
                conn.Open();
                string query = @"
                                SELECT *
                                FROM sinh_vien sv
                                JOIN lop_hoc lh ON sv.idLh = lh.LopId
                                JOIN giao_vien gv ON lh.idGv = gv.GvId
                                ";

                if (!string.IsNullOrEmpty(sort))
                {
                    query += " ORDER BY sv.TenSv " + sort;
                }
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@SORT", sort);
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

                    SinhVien sv = new SinhVien
                    {
                        id = Convert.ToInt32(reader["SvId"]),
                        MaSinhVien = reader["MaSv"].ToString(),
                        Ten = reader["TenSv"].ToString(),
                        NgaySinh = Convert.ToDateTime(reader["NgaySinhSv"]),
                        DiaChi = reader["DiaChi"].ToString(),
                        LopHoc = lh
                    };
                    sinhViens.Add(sv);
                }
                return sinhViens;

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
        public bool Them(SinhVien sv )
        {
            try
            {
                conn.Open();
                string query = @"insert into sinh_vien(MaSv,TenSv,NgaySinhSv,DiaChi,idLh) values
                                    (@MaSv,@TenSv,@NgaySinhSv,@DiaChi,@idLh)
                                ";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@MaSv", sv.MaSinhVien);
                cmd.Parameters.AddWithValue("@TenSv",sv.Ten);
                cmd.Parameters.AddWithValue("@NgaySinhSv", sv.NgaySinh);
                cmd.Parameters.AddWithValue("@DiaChi", sv.DiaChi);
                cmd.Parameters.AddWithValue("@idLh", sv.LopHoc.id);
                if(cmd.ExecuteNonQuery() > 0)
                {
                    return true;
                }
                return false;
            }catch (Exception)
            {
                throw;
            }
            finally
            {
                conn.Close ();
            }
        }
        public bool Sua(SinhVien sv)
        {
            try
            {
                conn.Open ();
                string query = @"update sinh_vien set TenSv=@TenSv, NgaySinhSv=@NgaySinhSv, DiaChi=@DiaChi, idLh=@idLh where SvId = @SvId";
                SqlCommand cmd= new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@TenSv",sv.Ten);
                cmd.Parameters.AddWithValue("@NgaySinhSv", sv.NgaySinh);
                cmd.Parameters.AddWithValue("@DiaChi", sv.DiaChi);
                cmd.Parameters.AddWithValue("@idLh", sv.LopHoc.id);
                cmd.Parameters.AddWithValue("@SvId", sv.id);
                if (cmd.ExecuteNonQuery() > 0)
                {
                    return true;
                }
                return false;
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
        public bool Xoa(int id)
        {
            try
            {
                conn.Open();
                string query = @"delete sinh_vien where SvId = @SvId";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@SvId", id);
                if (cmd.ExecuteNonQuery() > 0)
                {
                    return true;
                }
                return false;
            }
            catch(Exception)
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
