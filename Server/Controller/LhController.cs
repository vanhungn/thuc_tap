using ProtoBuf.Grpc;
using Server_Shared.model;
using Server_Shared;
using Server.Services;

namespace Server.Controller
{
    public class LhController:ILHController
    {
        private readonly ILopHocService service;
        public LhController(ILopHocService service)
        {
            this.service = service;
        }

        public Task<List<ModelLopHoc>> GetAllLopHoc(CallContext callContext) {
                return Task.FromResult(service.GetAllLopHoc());
            
        }

    }
}
