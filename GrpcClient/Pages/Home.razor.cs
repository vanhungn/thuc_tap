using Google.Protobuf.WellKnownTypes;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using Server_Shared;
using Server_Shared.model;

namespace GrpcClient.Pages
{
    public partial class Home : ComponentBase
    {
        [Inject] public ISVController _client { get; set; } = default!;
        [Inject] public ILHController _clientLopHoc { get; set; } = default!;
        [Inject] public IJSRuntime JS { get; set; } = default!;

        List<ModelSinhVien> list = new();
        List<ModelSinhVien> listselect = new();
        List<ModelLopHoc> listLopHoc = new();

        int chooseMenu;
        bool boolMenu;
        bool hideForm = false;
        bool? createOrUpdate = true;

        ModelSinhVien model = new()
        {
            MaSinhVien = "",
            Ten = "",
            DiaChi = "",
            NgaySinh = DateTime.Now,
            LopHoc = new ModelLopHoc
            {
                GiaoVien = new ModelGiaoVien()
            }
        };

        // ================= INIT =================
        protected override async Task OnInitializedAsync()
        {
            await Load();
        }

        async Task Load()
        {
            list = await _client.GetAll(default);
            listselect = list;
            listLopHoc = await _clientLopHoc.GetAllLopHoc(default);

            boolMenu = false;
            chooseMenu = 0;
        }

        // ================= MENU =================
        void Menu(int id)
        {
            chooseMenu = id;
            boolMenu = !boolMenu;
        }

        // ================= ADD =================
        void HandleClickThem()
        {
            hideForm = true;
            createOrUpdate = true;

            model = new ModelSinhVien
            {
                LopHoc = new ModelLopHoc
                {
                    GiaoVien = new ModelGiaoVien()
                }
            };

            Menu(0);
        }

        // ================= EDIT =================
        void HandleClickSua(int id)
        {
            hideForm = true;
            createOrUpdate = false;

            Menu(0);

            var selected = list.First(x => x.Id == id);

            model = new ModelSinhVien
            {
                Id = selected.Id,
                MaSinhVien = selected.MaSinhVien,
                Ten = selected.Ten,
                DiaChi = selected.DiaChi,
                NgaySinh = selected.NgaySinh,
                LopHoc = new ModelLopHoc
                {
                    Id = selected.LopHoc.Id,
                    MaLopHoc = selected.LopHoc.MaLopHoc,
                    TenLopHoc = selected.LopHoc.TenLopHoc,
                    MonHoc = selected.LopHoc.MonHoc,
                    GiaoVien = new ModelGiaoVien
                    {
                        Id = selected.LopHoc.GiaoVien.Id,
                        MaGiaoVien = selected.LopHoc.GiaoVien.MaGiaoVien,
                        Ten = selected.LopHoc.GiaoVien.Ten,
                        NgaySinh = selected.LopHoc.GiaoVien.NgaySinh
                    }
                }
            };
        }

        // ================= DELETE =================
        async Task HandlerClickXoa(int id)
        {
            var res = await _client.Delete(new DeleteRequest { Id = id}, default);

            if (res.Success)
            {
                await JS.InvokeVoidAsync("alert", "Xóa thành công");
                await Load();
            }
        }

        // ================= SEARCH =================
        async Task HandlerSearch(string masv)
        {
            if (string.IsNullOrEmpty(masv))
            {
                list = await _client.GetAll(default);
            }
            else
            {
                var res = await _client.GetById(new GetByIdRequest {Masv=masv }, default);

                list = new List<ModelSinhVien> { res };
            }
        }

        // ================= RESET =================
        async Task HandleReset()
        {
            model = new ModelSinhVien
            {
                LopHoc = new ModelLopHoc
                {
                    GiaoVien = new ModelGiaoVien()
                }
            };
        }

        // ================= SUBMIT =================
        async Task HandleValidSubmit()
        {
            try
            {
                var selectedLop = listLopHoc.FirstOrDefault(x => x.Id == model.LopHoc.Id);

                if (selectedLop == null)
                {
                    await JS.InvokeVoidAsync("alert", "Bạn chưa chọn lớp!");
                    return;
                }

                model.LopHoc = selectedLop;

                bool result = createOrUpdate == true
                    ? await Create()
                    : await Update();
                Console.WriteLine(result);
                if (result)
                {
                    await JS.InvokeVoidAsync(
                        "alert",
                        createOrUpdate == true ? "Thêm thành công!" : "Sửa thành công!"
                    );

                    await Load();
                }
                else
                {
                    await JS.InvokeVoidAsync("alert", "Sinh vien phai lon hon 10 tuoi");
                }
            }
            catch (Exception ex)
            {
                await JS.InvokeVoidAsync("alert", ex.Message);
            }
        }

        // ================= CREATE =================
        public async Task<bool> Create()
        {
            var res = await _client.Add(model, default);
            return res.Success;
        }

        // ================= UPDATE =================
        public async Task<bool> Update()
        {
            var res = await _client.Update(model, default);

            Console.WriteLine($"Success: {res.Success}, Message: {res.Message}");

            return res.Success;
        }
    }
}