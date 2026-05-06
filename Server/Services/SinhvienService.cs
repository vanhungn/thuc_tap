using Server.Repository;
using Server.Services;
using Server_Shared.model;
using Server.Repository.ISinhVienRepo;

namespace Server.Services
{
    public class SinhvienService:ISinhVienService
    {
        private readonly IGetAllSinhVien repo;
        private readonly IAddSinhVien repoAdd;
        private readonly IDeleteSinhVien repoDelete;
        private readonly IUpdateSinhVien repoUpdate;
        private readonly ISearchSinhVien repoSearch;
        private readonly IGetChart repoChart;
        public SinhvienService(IGetChart repoChart,IGetAllSinhVien repo,IAddSinhVien repoAdd, IDeleteSinhVien repoDelete,IUpdateSinhVien repoUpdate,ISearchSinhVien repoSearch)
        {
            this.repo = repo;
            this.repoAdd = repoAdd;
            this.repoDelete = repoDelete;
            this.repoUpdate = repoUpdate;
            this.repoSearch = repoSearch;
            this.repoChart = repoChart; 

        }
        public List<ModelSinhVien> GetAll() => repo.GetAll();
        public bool AddSinhVien(ModelSinhVien model) => repoAdd.AddSinhVien(model);
        public bool DeleteSinhVien(int id) => repoDelete.DeleteSinhVien(id);
        public bool UpdateSinhVien(ModelSinhVien model) => repoUpdate.UpdateSinhVien(model);
        public ModelSinhVien GetByIdSinhVien(string masv) => repoSearch.GetByIdSinhVien(masv);
        public List<ModelChart> GetAllChart() => repoChart.GetChart();
    }
}
