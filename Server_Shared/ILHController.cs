using ProtoBuf.Grpc;
using ProtoBuf.Grpc.Configuration;
using Server_Shared.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server_Shared
{
    [Service(name:"LHController")]
    public interface ILHController
    {
        Task<List<ModelLopHoc>> GetAllLopHoc(CallContext context);
    }
}
