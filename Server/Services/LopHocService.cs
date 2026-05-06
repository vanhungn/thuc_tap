using Server_Shared.model;
using Server.Repository.ILopHocRepo;

namespace Server.Services
{
    public class LopHocService:ILopHocService
    {
        private readonly IGetAllLopHoc repoGetAll;
        public LopHocService(IGetAllLopHoc repoGetAll)
        {
            this.repoGetAll = repoGetAll;
        }
        public List<ModelLopHoc> GetAllLopHoc() => repoGetAll.GetAllLopHoc(); 
    }
}
