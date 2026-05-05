using baitap1.controller;
using baitap1.interfaceSinhVien;
using baitap1.reponsitory;
using baitap1.service;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using static baitap1.service.serviceSinhVien;



namespace baitap1
{
    internal class Program
    {

     
        static void Main()
        {
            SqlConnection conn = new SqlConnection("Data Source = LAPTOP-NG3J2HSN\\KTEAM; Initial Catalog = thuctap; Integrated Security=True");
            var repo = new repositorySinhVien(conn);
            var serviceSinhVien = new SinhVienService(repo);
            var repo2 = new repositoryLopHoc(conn);
            var serviceLopHoc = new serviceLopHoc.LopHocService(repo2);
            var ctlSinhVIen = new ControllerSinhVien( serviceSinhVien, serviceLopHoc);
            ctlSinhVIen.loadData();


            bool thoat = true;
            while (thoat)
            {
                Console.WriteLine("Menu chuc nang");
                Console.WriteLine("1 Xem danh sach sinh vien");
                Console.WriteLine("2 Them sinh vien");
                Console.WriteLine("3 Sua thong tin sinh vien");
                Console.WriteLine("4 Xoa sinh vien");
                Console.WriteLine("5 Xap xep du lieu sinh vien theo ten");
                Console.WriteLine("6 Tim kiem sinh vien theo ma so sinh vien");
                Console.WriteLine("7 Thoat chuong trinh");
                Console.WriteLine("Chon chuc nang theo so");
                Console.WriteLine("----------------------------------------------------------");
                int chon;
                while (!int.TryParse(Console.ReadLine(), out chon))
                {
                    Console.WriteLine("Nhap so!");
                }
                switch (chon)
                {
                    case 1: { ctlSinhVIen.Xem(); break; };
                    case 2: { ctlSinhVIen.Them(); break; };
                    case 3: { ctlSinhVIen.Sua(); break; };
                    case 4: { ctlSinhVIen.Xoa(); break; };
                    case 5: { ctlSinhVIen.XapXep(); break; };
                    case 6: { ctlSinhVIen.TimKiem(); break; };
                    case 7: { thoat=false; break; };
                    default:
                        {
                            Console.WriteLine("Chon tu 1 -> 6");
                            break;
                        }

                } 
            }

        }
      
   
    }
}
