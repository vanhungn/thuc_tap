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
    [Service(name: "SVController")]
    public interface ISVController
    {
        Task<List<ModelSinhVien>> GetAll(CallContext context);

        Task<List<ModelChart>> GetChart(CallContext context);

        Task<BoolResponse> Add(ModelSinhVien request, CallContext context);

        Task<BoolResponse> Update(ModelSinhVien request, CallContext context);

        Task<BoolResponse> Delete(DeleteRequest request, CallContext context);

        Task<ModelSinhVien?> GetById(GetByIdRequest request, CallContext context);
    }
}
