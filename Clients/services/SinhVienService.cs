using ClientLopHoc.proto;
using Clients.Protos;
using Google.Protobuf.WellKnownTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clients.services
{
    internal class SinhVienService
    {   
        public interface IserviceSinhVien
        {
          Task<ListSinhVien>  XemSinhVien();
          Task<BoolResponse> UpdateSinhVien(SinhVien sv);
            Task<BoolResponse> DeleteSinhVien(SinhVienRequest id);
            Task<BoolResponse> AddSinhVien(SinhVien sv);
            Task<SinhVien> SearchSinhVien(MaSinhVien id);

        }
        public class ServiceSinhVien : IserviceSinhVien
        {
            private readonly SvService.SvServiceClient service;
            public ServiceSinhVien(SvService.SvServiceClient repoXem) {  
                
                this.service = repoXem;
            }
            public async Task<ListSinhVien>  XemSinhVien()=> await service.GetAllAsync(new Empty());
            public async Task<BoolResponse> UpdateSinhVien(SinhVien sv) => await service.UpdateAsync(sv);
            public async Task<BoolResponse> AddSinhVien(SinhVien sv) => await service.AddAsync(sv);
            public async Task<BoolResponse> DeleteSinhVien(SinhVienRequest id) => await service.DeleteAsync(id);
            public async Task<SinhVien> SearchSinhVien(MaSinhVien id) => await service.GetByIdAsync(id);
        }
    }
}
