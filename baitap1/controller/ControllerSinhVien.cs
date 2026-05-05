using baitap1.interfaceSinhVien;
using baitap1.reponsitory;
using baitap1.service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static baitap1.service.serviceSinhVien;

namespace baitap1.controller
{
    internal class ControllerSinhVien:XemSinhVien,UpdateSinhVien,DeleteSinhVien,SortSinhVien,AddSinhVien,SearchSinhVien,LoadData
    {
         private List<SinhVien> ds;
        private List<LopHoc> lopHocs;
        SinhVienService repoSv;
        serviceLopHoc.LopHocService repoLh;
        public ControllerSinhVien( SinhVienService repoSv, serviceLopHoc.LopHocService repoLh)
        {
            this.repoSv = repoSv;
            this.repoLh = repoLh;
        }

        public void loadData()
        {
            load("");
        }
        void load(string sort)
        {
            ds = repoSv.GetAll(sort);
            lopHocs = repoLh.GetAll();
        }
        public void Xem()
        {
            for (int i = 0; i < ds.Count; i++)
            {
                Console.WriteLine($"Id: {ds[i].id}");
                Console.WriteLine($"Ma sinh vien: {ds[i].MaSinhVien}");
                Console.WriteLine($"Ten sinh vien: {ds[i].Ten}");
                Console.WriteLine($"Ngay sinh: {ds[i].NgaySinh}");
                Console.WriteLine($"Dia chi: {ds[i].DiaChi}");
               
                Console.WriteLine("----------------------------------------------------------");
            }
        }
        public bool CheckMaSv(string masv)
        {
            for (int i = 0; i < ds.Count; i++)
            {
                if (ds[i].MaSinhVien == masv) return false;
            }
            return true;
        }
        public bool CheckAge(DateTime age)
        {
            if (DateTime.Now.Year - age.Year < 10) return false;
            return true;
        }
        public SinhVien NhapDuLieu(string feature = "")
        {

            SinhVien newSinhVien = new SinhVien();
            newSinhVien.LopHoc = new LopHoc();


            

            Console.WriteLine("Nhap ten sinh vien");
            newSinhVien.Ten = Console.ReadLine();

            DateTime newAge;

            Console.WriteLine("Nhap ngay sinh vd: 2004/01/17");

            while (!DateTime.TryParse(Console.ReadLine(), out newAge) || !CheckAge(newAge))
            {
                Console.WriteLine("Ngay sinh khong hop le hoac tuoi < 10 hoac chua dung dinh dang ngay sinh. Moi nhap lai:");
            }

            newSinhVien.NgaySinh = newAge;
            Console.WriteLine("Nhap dia chi");
            newSinhVien.DiaChi = Console.ReadLine();

            for (int i = 0; i < lopHocs.Count; i++)
            {

                Console.WriteLine($"Nhap ma lop index({i}): {lopHocs[i].MaLopHoc}");

            }

            Console.WriteLine("Nhap ma lop theo index lop hoc");
            int maLopHoc;
            while (!int.TryParse(Console.ReadLine(), out maLopHoc))
            {
                Console.WriteLine("Ban phai nhap so! Moi nhap lai:");
            }
            for (int i = 0; i < lopHocs.Count; i++)
            {
                if (lopHocs[i].MaLopHoc == lopHocs[maLopHoc].MaLopHoc)
                {
                    newSinhVien.LopHoc = lopHocs[i];
                }
            }
            Console.WriteLine("----------------------------------------------------------");
            return newSinhVien;

        }
        public void Them()
        {
            Console.WriteLine("Nhap ma sinh vien:");
            string masv = Console.ReadLine();
            bool CheckMasv = CheckMaSv(masv);
                while (!CheckMasv)
                {
                    Console.WriteLine("Ma sinh vien bi trung moi ban nhap lai");
                    masv = Console.ReadLine();
                    CheckMasv = CheckMaSv(masv);
                }
            SinhVien sinhVien = NhapDuLieu("Them");
            
            SinhVien sv = new SinhVien() { 
                MaSinhVien= masv,
                Ten = sinhVien.Ten,
                NgaySinh = sinhVien.NgaySinh,
                DiaChi = sinhVien.DiaChi,
                LopHoc = sinhVien.LopHoc,
            };
            if (repoSv.them(sv))
            {
                load("");
                Console.WriteLine("----------------------------------------------------------");
                Xem();
            }
        }
        public void Sua()
        {
            Xem();
            Console.WriteLine("Nhap du lieu nhan vien can sua");
            Console.WriteLine("Nhap idSv");
            int id;
            while(!int.TryParse(Console.ReadLine(), out id)){
                Console.WriteLine("Id nhan vien phai la so");
            }
            SinhVien LayDLsv = NhapDuLieu("");
            SinhVien sv = new SinhVien() { 
                id = id,
                MaSinhVien = LayDLsv.MaSinhVien,
                Ten = LayDLsv.Ten,
                DiaChi = LayDLsv.DiaChi,
                NgaySinh = LayDLsv.NgaySinh,
                LopHoc = LayDLsv.LopHoc,
            };

           
            if(repoSv.sua(sv))
            {
                Console.WriteLine("Sửa sinh viên thành công");
                load("");
                Xem();
            }
           
        }
        public void Xoa()
        {
            Xem();
            Console.WriteLine("Nhập id sinh viên");
            int id;
            while (!int.TryParse(Console.ReadLine(), out id))
            {
                Console.WriteLine("Ban phai nhap so! Moi nhap lai:"); ;
            }
            if (repoSv.xoa(id))
            {
                Console.WriteLine("Xóa sinh viên thành công");
            }
        }
        public void XapXep()
        {

            Console.WriteLine("1 a->z");
            Console.WriteLine("2 z->a");
            Console.WriteLine("Chọn 1 or 2");

            int chon;
            if (int.TryParse(Console.ReadLine(), out chon))
            {
                Console.WriteLine("Ban vui long chon 1 or 2");
            }
            while (chon < 1 || chon > 2)
            {
                Console.WriteLine("Chọn 1 or 2");
                chon = int.Parse(Console.ReadLine());
            }

            string sort = chon == 1 ? "ASC" : "DESC";
            load(sort);
            Console.WriteLine("----------------------------------------------------------");
            Xem();
        }
        public void TimKiem()
        {
            Console.WriteLine("Nhap ma sinh vien can tim");

            int id;
            if (!int.TryParse(Console.ReadLine(), out id))
            {
                Console.WriteLine("Ban phai nhap so! Moi nhap lai:"); ;
            }
            SinhVien sv = ds.Find(x => x.id == id);
            if (sv == null) { Console.WriteLine("Khong tim thay sinh vien"); return; }
            Console.WriteLine($"Ma sinh vien: {sv.MaSinhVien}");
            Console.WriteLine($"Ten sinh vien: {sv.Ten}");
            Console.WriteLine($"Ngay sinh: {sv.NgaySinh}");
            Console.WriteLine($"Dia chi: {sv.DiaChi}");
            Console.WriteLine($"Lop hoc: {sv.LopHoc.TenLopHoc}");
            Console.WriteLine("----------------------------------------------------------");
        }

    }
  
}
