using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Clients.interfaceSinhVien;
using ClientLopHoc.proto;
using Clients.model;
using Clients.Protos;
using Google.Protobuf.WellKnownTypes;
using Clients.services;

namespace Clients.controller
{
    internal class ClientSinhVien:XemSinhVien,AddSinhVien,UpdateSinhVien,LoadData,DeleteSinhVien,SearchSinhVien
    {
        private listLopHoc _listLopHoc = new listLopHoc();
        private ListSinhVien _listSinhVien = new ListSinhVien();
        private readonly SinhVienService.IserviceSinhVien serviceSV;
        private readonly ServiceLopHoc.ILopHocService serviceLH;
        public ClientSinhVien(SinhVienService.IserviceSinhVien sv, ServiceLopHoc.ILopHocService lh)
        {
          this.serviceSV=sv;
            this.serviceLH=lh;
        }
      
        public bool CheckAge(DateTime age)
        {
            if (DateTime.Now.Year - age.Year < 10) return false;
            return true;
        }
        public  async Task loadData()
        {

           await load();
        }
        async Task load()
        {
            _listSinhVien = await serviceSV.XemSinhVien();
            _listLopHoc = await serviceLH.XemLopHoc();
        }
        public ModelSinhVien NhapDuLieu()
        {
            
            var lopHocs = _listLopHoc.Items.Select(x => new ModelLopHoc
            {
                Id = x.Id,
                TenLopHoc = x.TenLopHoc,
                MaLopHoc = x.MaLopHoc,
                MonHoc = x.MonHoc,
                GiaoVien = new ModelGiaoVien
                {
                    Id = x.GiaoVien.Id,
                    MaGiaoVien = x.GiaoVien.MaGiaoVien,
                    Ten = x.GiaoVien.Ten,
                    NgaySinh = x.GiaoVien.NgaySinh.ToDateTime()
                }
            }).ToList();
            ModelSinhVien newSinhVien = new ModelSinhVien();
            newSinhVien.LopHoc = new ModelLopHoc();




            Console.WriteLine("Nhap ten sinh vien");
            newSinhVien.Ten = Console.ReadLine()??"";

            DateTime newAge;

            Console.WriteLine("Nhap ngay sinh vd: 2004/01/17");

            while (!DateTime.TryParse(Console.ReadLine(), out newAge) || !CheckAge(newAge))
            {
                Console.WriteLine("Ngay sinh khong hop le hoac tuoi < 10 hoac chua dung dinh dang ngay sinh. Moi nhap lai:");
            }

            newSinhVien.NgaySinh = newAge;
            Console.WriteLine("Nhap dia chi");
            newSinhVien.DiaChi = Console.ReadLine()??"";

            for (int i = 0; i < lopHocs.Count; i++)
            {

                Console.WriteLine($"Nhap ma lop index({i}): {lopHocs[i].MaLopHoc}");

            }

            Console.WriteLine("Nhap ma lop theo index lop hoc");
            int maLopHoc;
        
            while (!int.TryParse(Console.ReadLine(), out maLopHoc)|| maLopHoc < 0 || maLopHoc > lopHocs.Count-1)
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
        public void Xem()
        {
            var list = _listSinhVien.Items.Select(x => new Clients.model.ModelSinhVien
            {
                Id = x.Id,
                MaSinhVien = x.MaSinhVien,
                Ten = x.Ten,
                DiaChi = x.DiaChi,
                NgaySinh = x.NgaySinh.ToDateTime(),

                LopHoc = new Clients.model.ModelLopHoc
                {
                    Id = x.LopHoc.Id,
                    MaLopHoc = x.LopHoc.MaLopHoc,
                    TenLopHoc = x.LopHoc.TenLopHoc,
                    MonHoc = x.LopHoc.MonHoc,

                    GiaoVien = new Clients.model.ModelGiaoVien
                    {
                        Id = x.LopHoc.GiaoVien.Id,
                        MaGiaoVien = x.LopHoc.GiaoVien.MaGiaoVien,
                        Ten = x.LopHoc.GiaoVien.Ten,
                        NgaySinh = x.LopHoc.GiaoVien.NgaySinh.ToDateTime()
                    }
                }
            }).ToList();

            foreach (var i in list)
            {
                Console.WriteLine("=================================");
                Console.WriteLine($"ID: {i.Id}");
                Console.WriteLine($"MaSV: {i.MaSinhVien}");
                Console.WriteLine($"Ten: {i.Ten}");
                Console.WriteLine($"DiaChi: {i.DiaChi}");

                if (i.LopHoc != null)
                {
                    Console.WriteLine($"Lop: {i.LopHoc.TenLopHoc}");
                    Console.WriteLine($"Mon: {i.LopHoc.MonHoc}");

                    if (i.LopHoc.GiaoVien != null)
                    {
                        Console.WriteLine($"GV: {i.LopHoc.GiaoVien.Ten}");
                    }
                }
            }
        }

        public async Task AddSinhVien()
        {

            ModelSinhVien sinhVien = NhapDuLieu();

            SinhVien sv = new SinhVien
            {
                
                Id = sinhVien.Id,
                DiaChi = sinhVien.DiaChi,
                MaSinhVien =sinhVien.MaSinhVien,
                Ten = sinhVien.Ten,
                NgaySinh = Timestamp.FromDateTime(sinhVien.NgaySinh.ToUniversalTime()),
                LopHoc = new Clients.Protos.LopHoc
                {
                    Id = sinhVien.LopHoc.Id,
                    MaLopHoc = sinhVien.LopHoc.MaLopHoc,
                    TenLopHoc = sinhVien.LopHoc.TenLopHoc,
                    MonHoc = sinhVien.LopHoc.MonHoc,

                    GiaoVien = new Clients.Protos.GiaoVien
                    {
                        Id = sinhVien.LopHoc.GiaoVien.Id,
                        MaGiaoVien = sinhVien.LopHoc.GiaoVien.MaGiaoVien,
                        Ten = sinhVien.LopHoc.GiaoVien.Ten,
                        NgaySinh = Timestamp.FromDateTime(sinhVien.LopHoc.GiaoVien.NgaySinh.ToUniversalTime())


                    }
                }
            };
            var result = await serviceSV.AddSinhVien(sv);
            if (result.Success)
            {
                Console.WriteLine("----------------------------------------------------------");
                await load();
            }
        }
        public async Task Sua()
        {
            Xem();
            Console.WriteLine("Nhap du lieu nhan vien can sua");
            Console.WriteLine("Nhap idSv");
            int id;
            while (!int.TryParse(Console.ReadLine(), out id))
            {
                Console.WriteLine("Id nhan vien phai la so");
            }
            ModelSinhVien sinhVien = NhapDuLieu();

            SinhVien sv = new SinhVien
            {

                Id = id,
                DiaChi = sinhVien.DiaChi,
                MaSinhVien = sinhVien.MaSinhVien,
                Ten = sinhVien.Ten,
                NgaySinh = Timestamp.FromDateTime(sinhVien.NgaySinh.ToUniversalTime()),
                LopHoc = new Clients.Protos.LopHoc
                {
                    Id = sinhVien.LopHoc.Id,
                    MaLopHoc = sinhVien.LopHoc.MaLopHoc,
                    TenLopHoc = sinhVien.LopHoc.TenLopHoc,
                    MonHoc = sinhVien.LopHoc.MonHoc,

                    GiaoVien = new Clients.Protos.GiaoVien
                    {
                        Id = sinhVien.LopHoc.GiaoVien.Id,
                        MaGiaoVien = sinhVien.LopHoc.GiaoVien.MaGiaoVien,
                        Ten = sinhVien.LopHoc.GiaoVien.Ten,
                        NgaySinh = Timestamp.FromDateTime(sinhVien.LopHoc.GiaoVien.NgaySinh.ToUniversalTime())


                    }
                }
            };

            var result = await serviceSV.UpdateSinhVien(sv);
            if (result.Success)
            {
                Console.WriteLine("Sua nhan vien thanh cong");
              await  load();
            }

        }
        public async Task Xoa()
        {
            Xem();
            Console.WriteLine("Nhập id sinh viên");
            int id;
            while (!int.TryParse(Console.ReadLine(), out id))
            {
                Console.WriteLine("Ban phai nhap so! Moi nhap lai:"); ;
            }
            var result = await serviceSV.DeleteSinhVien(new SinhVienRequest()
            {
                Id = id
            });
            if (result.Success)
            {
                Console.WriteLine("Xóa sinh viên thành công");
               await load();
            }
        }
        public async Task TimKiem()
        {
            Console.WriteLine("Nhap ma sinh vien can tim");

            string masv = Console.ReadLine()??"";
          
            SinhVien sv = await serviceSV.SearchSinhVien(new MaSinhVien() { 
                Masv = masv
            }) ;
            if (sv.Id == 0) { Console.WriteLine("Khong tim thay sinh vien"); return; }

            Console.WriteLine($"Ma sinh vien: {sv.MaSinhVien}");
            Console.WriteLine($"Ten sinh vien: {sv.Ten}");
            Console.WriteLine($"Ngay sinh: {sv.NgaySinh}");
            Console.WriteLine($"Dia chi: {sv.DiaChi}");
            Console.WriteLine($"Lop hoc: {sv.LopHoc.TenLopHoc}");
            Console.WriteLine("----------------------------------------------------------");
        }
    }
}
