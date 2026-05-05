using Microsoft.AspNetCore.Components;
using Blazorise.Charts;
using GrpcClient.model;
using grpcClientSinhVien.Protos;
using Google.Protobuf.WellKnownTypes;

namespace GrpcClient.Pages
{
    public partial class Weather : ComponentBase
    {
        [Inject]
        public SvService.SvServiceClient Client { get; set; } = default!;

        protected PieChart<double>? pieChart;

        protected List<ModelChart> danhSachLop = new();

        protected override async Task OnInitializedAsync()
        {
            var res = await Client.GetChartAsync(new Empty());

            danhSachLop = res.Items.Select(x => new ModelChart
            {
                Name = x.Name,
                SoLuongSinhVien = x.SoLuongSinhVien
            }).ToList();
            await LoadData();
        }
          
        async Task LoadData()
        {
            if (pieChart != null)
            {

                var labels = danhSachLop.Select(x => x.Name).ToList();
                var data = danhSachLop.Select(x => (double)x.SoLuongSinhVien).ToList();

                var dataset = new PieChartDataset<double>
                {
                    Data = data,
                    BackgroundColor = new List<string>
                        {
                            "#FF6384",
                            "#36A2EB",
                            "#FFCE56"
                        }
                };

                await pieChart.Clear();
                await pieChart.AddLabelsDatasetsAndUpdate(labels, dataset);
                StateHasChanged();
            }
        }
    }
}