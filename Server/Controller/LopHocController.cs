using Azure.Core;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using Server.Services;
using ServerLopHoc.proto;
namespace Server.Controller
{
    public class LopHocController:SvServiceLopHoc.SvServiceLopHocBase
    {
        private readonly ILopHocService service;
        public LopHocController(ILopHocService service)
        {
            this.service = service;
        }
        public override Task<listLopHoc> GetAllLopHoc(Empty empty, ServerCallContext context )
        {
            var data = service.GetAllLopHoc();
            var result = new listLopHoc();
            result.Items.AddRange(data.Select(x => new LopHoc
            {
                Id = x.Id,
                MaLopHoc = x.MaLopHoc,
                TenLopHoc = x.TenLopHoc,
                MonHoc = x.MonHoc,

                GiaoVien = new GiaoVien
                {
                    Id = x.GiaoVien.Id,
                    MaGiaoVien = x.GiaoVien.MaGiaoVien,
                    Ten = x.GiaoVien.Ten,
                    NgaySinh = Timestamp.FromDateTime(x.GiaoVien.NgaySinh.ToUniversalTime())


                }
            }));
            return Task.FromResult(result);
        }
    }
}
