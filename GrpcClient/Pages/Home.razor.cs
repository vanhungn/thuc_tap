using Google.Protobuf.WellKnownTypes;
using GrpcClient.model;
using grpcClientLopHoc.proto;
using grpcClientSinhVien.Protos;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace GrpcClient.Pages
{
    public partial class Home : ComponentBase
    {
        [Inject] public SvService.SvServiceClient _client { get; set; } = default!;
        [Inject] public SvServiceLopHoc.SvServiceLopHocClient _clientLopHoc { get; set; } = default!;
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

        protected override async Task OnInitializedAsync()
        {
            await Load();
        }

        async Task Load()
        {
            list = await GetAll();
            listselect = list;
            listLopHoc = await GetLopHoc();
            boolMenu = false;
            chooseMenu = 0;
        }

        public async Task<List<ModelSinhVien>> GetAll()
        {
            var res = await _client.GetAllAsync(new Empty());
            return res.Items.Select(x => new ModelSinhVien
            {
                Id = x.Id,
                Ten = x.Ten,
                MaSinhVien = x.MaSinhVien,
                DiaChi = x.DiaChi,
                NgaySinh = x.NgaySinh.ToDateTime(),
                LopHoc = new ModelLopHoc
                {
                    Id = x.LopHoc.Id,
                    TenLopHoc = x.LopHoc.TenLopHoc,
                    MaLopHoc = x.LopHoc.MaLopHoc,
                    MonHoc = x.LopHoc.MonHoc,
                    GiaoVien = new ModelGiaoVien
                    {
                        Id = x.LopHoc.GiaoVien.Id,
                        MaGiaoVien = x.LopHoc.GiaoVien.MaGiaoVien,
                        NgaySinh = x.LopHoc.GiaoVien.NgaySinh.ToDateTime(),
                        Ten = x.LopHoc.GiaoVien.Ten,

                    }
                }
            }).ToList();
        }

        public async Task<List<ModelLopHoc>> GetLopHoc()
        {
            var res = await _clientLopHoc.GetAllLopHocAsync(new Empty());
            return res.Items.Select(x => new ModelLopHoc
            {
                Id = x.Id,
                TenLopHoc = x.TenLopHoc,
                MaLopHoc = x.MaLopHoc,
                MonHoc = x.MonHoc,
                GiaoVien = new ModelGiaoVien
                {
                    Id = x.GiaoVien.Id,
                    MaGiaoVien = x.GiaoVien.MaGiaoVien,
                    NgaySinh = x.GiaoVien.NgaySinh.ToDateTime(),
                    Ten = x.GiaoVien.Ten,

                }
            }).ToList();
        }

        void Menu(int id)
        {
            chooseMenu = id;
            boolMenu = !boolMenu;
        }

        void HandleClickThem()
        {
            hideForm = true;
            createOrUpdate = true;
            model = new ModelSinhVien
            {
                LopHoc = new ModelLopHoc { GiaoVien = new ModelGiaoVien() }
            };
            Menu(0);
        }

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
                    TenLopHoc = selected.LopHoc.TenLopHoc,
                    MaLopHoc = selected.LopHoc.MaLopHoc,
                    MonHoc = selected.LopHoc.MonHoc,
                    GiaoVien = new ModelGiaoVien
                    {
                        Id = selected.LopHoc.GiaoVien.Id,
                        MaGiaoVien = selected.LopHoc.GiaoVien.MaGiaoVien,
                        NgaySinh = selected.LopHoc.GiaoVien.NgaySinh,
                        Ten = selected.LopHoc.GiaoVien.Ten,

                    }
                }
            };
            Console.WriteLine(Timestamp.FromDateTime(model.NgaySinh.ToUniversalTime()).ToDateTime());
        }

        async Task HandlerClickXoa(int id)
        {
            var res = await _client.DeleteAsync(new SinhVienRequest { Id = id });
            if (res.Success)
            {
                await JS.InvokeVoidAsync("alert", "Xoa thanh cong");
                await Load();
            }
        }

        async Task HandlerSearch(string masv)
        {
            list = string.IsNullOrEmpty(masv)
                ? await GetAll()
                : await GetDetailSinhVien(masv);
        }

        public async Task<List<ModelSinhVien>> GetDetailSinhVien(string masv)
        {
            var res = await _client.GetByIdAsync(new MaSinhVien { Masv = masv });
            return new List<ModelSinhVien>
            {
                new()
                {
                    Id = res.Id,
                    Ten = res.Ten,
                    MaSinhVien = res.MaSinhVien,
                    DiaChi = res.DiaChi,
                    NgaySinh = res.NgaySinh.ToDateTime(),
                    LopHoc = new ModelLopHoc
                    {
                        Id = res.LopHoc.Id,
                        TenLopHoc = res.LopHoc.TenLopHoc,
                        MaLopHoc = res.LopHoc.MaLopHoc,
                        MonHoc = res.LopHoc.MonHoc,
                       GiaoVien = new ModelGiaoVien
                    {
                        Id = res.LopHoc.GiaoVien.Id,
                        MaGiaoVien = res.LopHoc.GiaoVien.MaGiaoVien,
                        NgaySinh = res.LopHoc.GiaoVien.NgaySinh.ToDateTime(),
                        Ten = res.LopHoc.GiaoVien.Ten,

                    }
                    }
                }
            };
        }
        async Task HandleReset()
        {
            model = new ModelSinhVien
            {
                LopHoc = new ModelLopHoc { GiaoVien = new ModelGiaoVien() }
            };
        }


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

                bool result = createOrUpdate == true ? await Create() : await Update();

                if (result)
                {
                    await JS.InvokeVoidAsync("alert", createOrUpdate == true ? "Thêm thành công!" : "Sửa thành công!");
                    await Load();
                }
                else
                {
                    await JS.InvokeVoidAsync("alert", "Ngay sinh phai lon hon 10 tuoi!");
                }
            }
            catch (Exception ex)
            {
                await JS.InvokeVoidAsync("alert", ex.Message); 
            }
        }

        public async Task<bool> Create()
        {
            var res = await _client.AddAsync(new SinhVien
            {
                Ten = model.Ten,
                DiaChi = model.DiaChi,
                MaSinhVien = model.MaSinhVien,
                NgaySinh = Timestamp.FromDateTime(model.NgaySinh.ToUniversalTime()),
                LopHoc = new grpcClientSinhVien.Protos.LopHoc
                {
                    Id = model.LopHoc.Id,
                    TenLopHoc = model.LopHoc.TenLopHoc,
                    MaLopHoc = model.LopHoc.MaLopHoc,
                    MonHoc = model.LopHoc.MonHoc,
                    GiaoVien = new grpcClientSinhVien.Protos.GiaoVien
                    {
                        Id = model.LopHoc.GiaoVien.Id,
                        MaGiaoVien = model.LopHoc.GiaoVien.MaGiaoVien,
                        NgaySinh =Timestamp.FromDateTime( model.LopHoc.GiaoVien.NgaySinh.ToUniversalTime()),
                        Ten = model.LopHoc.GiaoVien.Ten,

                    }
                },
               

            });
            return res.Success;
        }

        public async Task<bool> Update()
        {
            Console.WriteLine(model);
            var res = await _client.UpdateAsync(new SinhVien
            {
                Id = model.Id,
                Ten = model.Ten,
                DiaChi = model.DiaChi,
                MaSinhVien = model.MaSinhVien,
                NgaySinh = Timestamp.FromDateTime(model.NgaySinh.ToUniversalTime()),
                LopHoc = new grpcClientSinhVien.Protos.LopHoc
                {
                    Id = model.LopHoc.Id,
                    TenLopHoc = model.LopHoc.TenLopHoc,
                    MaLopHoc = model.LopHoc.MaLopHoc,
                    MonHoc = model.LopHoc.MonHoc,
                    GiaoVien = new grpcClientSinhVien.Protos.GiaoVien
                    {
                        Id = model.LopHoc.GiaoVien.Id,
                        MaGiaoVien = model.LopHoc.GiaoVien.MaGiaoVien,
                        NgaySinh = Timestamp.FromDateTime(model.LopHoc.GiaoVien.NgaySinh.ToUniversalTime()),
                        Ten = model.LopHoc.GiaoVien.Ten,

                    }
                }
            });
            Console.WriteLine($"Success: {res.Success}, Message: {res.Message}");
            return res.Success;
        }
    }
}