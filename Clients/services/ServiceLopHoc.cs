using ClientLopHoc.proto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clients.services
{
    internal class ServiceLopHoc
    {
        public interface ILopHocService
        {
            Task<listLopHoc> XemLopHoc();
        }
        public class LopHocService:ILopHocService
        {
            private readonly SvServiceLopHoc.SvServiceLopHocClient service;
            public LopHocService(SvServiceLopHoc.SvServiceLopHocClient service)
            {
                this.service = service;
            }
            public async Task<listLopHoc> XemLopHoc() => await service.GetAllLopHocAsync(new Google.Protobuf.WellKnownTypes.Empty());

        }
    }
}
