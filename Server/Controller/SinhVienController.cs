using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using Server.Services;
using Server.proto;
using Azure.Core;
using Server.model;
using Microsoft.Data.SqlClient;

namespace Server.Controller
{
    public class SinhVienController:SvService.SvServiceBase
    {
        private readonly ISinhVienService service;
        public SinhVienController(ISinhVienService service)
        {
            this.service = service;
        }
        public override Task<ListSinhVien> GetAll(Empty empty, ServerCallContext context)
        {
            var data = service.GetAll();
            var result = new ListSinhVien();

            result.Items.AddRange(data.Select(x => new  SinhVien
            {
                Id = x.Id,
                Ten = x.Ten,
                MaSinhVien = x.MaSinhVien,
                DiaChi = x.DiaChi,
                NgaySinh = Timestamp.FromDateTime(x.NgaySinh.ToUniversalTime()),

                LopHoc = new LopHoc
                {
                    Id = x.LopHoc.Id,
                    MaLopHoc = x.LopHoc.MaLopHoc,
                    TenLopHoc = x.LopHoc.TenLopHoc,
                    MonHoc = x.LopHoc.MonHoc,

                    GiaoVien = new GiaoVien
                    {
                        Id = x.LopHoc.GiaoVien.Id,
                        MaGiaoVien = x.LopHoc.GiaoVien.MaGiaoVien,
                        Ten = x.LopHoc.GiaoVien.Ten,
                        NgaySinh = Timestamp.FromDateTime(x.LopHoc.GiaoVien.NgaySinh.ToUniversalTime())


                    }
                }
            }));

            return Task.FromResult(result);
        }
        public override Task<ListChart> GetChart(Empty empty, ServerCallContext context)
        {
            var data = service.GetAllChart();
            var resutl = new ListChart();
            resutl.Items.AddRange(data.Select(x=> new chart
            {
                Name = x.Name,
                SoLuongSinhVien =x.SoLuongSinhVien
            }));
            return Task.FromResult(resutl);
        }
        public override Task<BoolResponse> Add(SinhVien request, ServerCallContext context)
        {
            try
            {
               
                ModelSinhVien sv = new ModelSinhVien()
                {
                    Id = request.Id,
                    DiaChi = request.DiaChi,
                    MaSinhVien = request.MaSinhVien,
                    NgaySinh = request.NgaySinh.ToDateTime(),
                    Ten = request.Ten,
                    LopHoc = new model.ModelLopHoc
                    {
                        Id = request.LopHoc.Id,
                        MaLopHoc = request.LopHoc.MaLopHoc,
                        TenLopHoc = request.LopHoc.TenLopHoc,
                        MonHoc = request.LopHoc.MonHoc,
                        GiaoVien = new model.ModelGiaoVien
                        {
                            Id = request.LopHoc.GiaoVien.Id,
                            MaGiaoVien = request.LopHoc.GiaoVien.MaGiaoVien,
                            Ten = request.LopHoc.GiaoVien.Ten,
                            NgaySinh = request.LopHoc.GiaoVien.NgaySinh.ToDateTime()
                        }
                    }
                };

                var result = service.AddSinhVien(sv);

                return Task.FromResult(new BoolResponse
                {
                    Success = result,
                    Message = "Thêm thành công"
                });
            }
            catch (Exception ex)
            {
                string message = "Lỗi dữ liệu";


                if (ex.ToString().Contains("547"))
                {
                    message = "Ngay sinh ko hop le";
                }
                return Task.FromResult(new BoolResponse
                {
                    Success = false,
                    Message = message
                });
            }
        }
        public override  Task<BoolResponse> Update(SinhVien request, ServerCallContext context)
        {

            try
            {
            
            ModelSinhVien sv = new ModelSinhVien()
            {
                DiaChi = request.DiaChi,
                Id = request.Id,
                MaSinhVien = request.MaSinhVien,
                Ten = request.Ten,
                NgaySinh = request.NgaySinh.ToDateTime(),
                LopHoc = new model.ModelLopHoc
                {
                    Id = request.LopHoc.Id,
                    MaLopHoc = request.LopHoc.MaLopHoc,
                    TenLopHoc = request.LopHoc.TenLopHoc,
                    MonHoc = request.LopHoc.MonHoc,

                    GiaoVien = new model.ModelGiaoVien
                    {
                        Id = request.LopHoc.GiaoVien.Id,
                        MaGiaoVien = request.LopHoc.GiaoVien.MaGiaoVien,
                        Ten = request.LopHoc.GiaoVien.Ten,
                        NgaySinh = request.LopHoc.GiaoVien.NgaySinh.ToDateTime()


                    }
                }
            };
            var result =  service.UpdateSinhVien(sv);
            return Task.FromResult(new BoolResponse
            {
                   Success = result,
                Message = ""
            });
            }catch(Exception ex)
            {
                string message = "Lỗi dữ liệu";


                if (ex.ToString().Contains("547"))
                {
                    message = "Ngay sinh ko hop le";
                }
                return Task.FromResult(new BoolResponse
                {
                    Success = false,
                    Message = message
                });
            }
           

        }
        public override  Task<BoolResponse> Delete(SinhVienRequest request, ServerCallContext context)
        {
            var result =  service.DeleteSinhVien(request.Id);
            return Task.FromResult(new BoolResponse
            {
                Success = result,
                Message = "Xóa thành công"
            });
        }
        public override Task<SinhVien> GetById(MaSinhVien request, ServerCallContext context)
        {
            var result = service.GetByIdSinhVien(request.Masv);
            var sv = new SinhVien
            {
                Id = result.Id,
                Ten = result.Ten,
                MaSinhVien = result.MaSinhVien,
                DiaChi = result.DiaChi,
                NgaySinh = Timestamp.FromDateTime(result.NgaySinh.ToUniversalTime()),

                LopHoc = new LopHoc
                {
                    Id = result.LopHoc.Id,
                    MaLopHoc = result.LopHoc.MaLopHoc,
                    TenLopHoc = result.LopHoc.TenLopHoc,
                    MonHoc = result.LopHoc.MonHoc,

                    GiaoVien = new GiaoVien
                    {
                        Id = result.LopHoc.GiaoVien.Id,
                        MaGiaoVien = result.LopHoc.GiaoVien.MaGiaoVien,
                        Ten = result.LopHoc.GiaoVien.Ten,
                        NgaySinh = Timestamp.FromDateTime(result.LopHoc.GiaoVien.NgaySinh.ToUniversalTime())
                    }
                }
            };
            return Task.FromResult(sv);
        }
      
    }
}
