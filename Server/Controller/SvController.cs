using Server_Shared;
using Server_Shared.model;
using ProtoBuf.Grpc;
using Server.Services;

namespace Server.Controller
{
    public class SvController : ISVController
    {
        private readonly ISinhVienService service;
        public SvController(ISinhVienService service)
        {
            this.service = service;
        }

        public Task<List<ModelSinhVien>> GetAll(CallContext context)
        {
            try
            {
                var result = service.GetAll();
                Console.WriteLine($"✅ GetAll OK: {result.Count} bản ghi");
                return Task.FromResult(result);
            }
            catch (Exception ex)
            {
                Console.WriteLine("❌ LỖI GetAll:");
                Console.WriteLine(ex.ToString());
                throw;
            }
        }

        public Task<List<ModelChart>> GetChart(CallContext context)
        {
            try
            {
                return Task.FromResult(service.GetAllChart());
            }
            catch (Exception ex)
            {
                Console.WriteLine("❌ LỖI GetChart: " + ex);
                throw;
            }
        }

        public Task<BoolResponse> Add(ModelSinhVien request, CallContext context)
        {
            try
            {
                var result = service.AddSinhVien(request);
                return Task.FromResult(new BoolResponse { Success = result });
            }
            catch (Exception ex)
            {
                Console.WriteLine("❌ LỖI Add: " + ex);
                return Task.FromResult(new BoolResponse { Success = false });
            }
        }

        public Task<BoolResponse> Update(ModelSinhVien request, CallContext context)
        {
            try
            {
                var result = service.UpdateSinhVien(request);
                return Task.FromResult(new BoolResponse { Success = result });
            }
            catch (Exception ex)
            {
                Console.WriteLine("❌ LỖI Update: " + ex);
                return Task.FromResult(new BoolResponse { Success = false });
            }
        }

        public Task<BoolResponse> Delete(DeleteRequest request, CallContext context)
        {
            try
            {
                var result = service.DeleteSinhVien(request.Id);
                return Task.FromResult(new BoolResponse { Success = result });
            }
            catch (Exception ex)
            {
                Console.WriteLine("❌ LỖI Delete: " + ex);
                return Task.FromResult(new BoolResponse { Success = false });
            }
        }

        public Task<ModelSinhVien?> GetById(GetByIdRequest request, CallContext context)
        {
            try
            {
                var result = service.GetByIdSinhVien(request.Masv);
                return Task.FromResult<ModelSinhVien?>(result);
            }
            catch (Exception ex)
            {
                Console.WriteLine("❌ LỖI GetById: " + ex);
                throw;
            }
        }
    }
}