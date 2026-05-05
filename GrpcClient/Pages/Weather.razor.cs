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
        protected BarChart<double>? barChart;

        protected List<ModelChart> danhSachLop = new();

        private bool _isLoaded = false;

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender && !_isLoaded)
            {
                _isLoaded = true;

                var res = await Client.GetChartAsync(new Empty());

                danhSachLop = res.Items.Select(x => new ModelChart
                {
                    Name = x.Name,
                    SoLuongSinhVien = x.SoLuongSinhVien
                }).ToList();

                var labels = danhSachLop.Select(x => x.Name).ToList();
                var data = danhSachLop.Select(x => (double)x.SoLuongSinhVien).ToList();

                await LoadDataChartPie(labels, data);
                await LoadDataChartBar(labels, data);
            }
        }

        async Task LoadDataChartPie(List<string> labels, List<double> data)
        {
            if (pieChart == null) return;


            var dataset = new PieChartDataset<double>
            {
                Data = data,
                BackgroundColor = new List<string> { "#FF6384", "#36A2EB", "#FFCE56" }
            };

            await pieChart.Clear();
            await pieChart.AddLabelsDatasetsAndUpdate(labels, dataset);
        }

        async Task LoadDataChartBar(List<string> labels, List<double> data)
        {
            if (barChart == null) return;

            var dataset = new BarChartDataset<double>
            {
                Label = "Số sinh viên",
                Data = data,
                BackgroundColor = new List<string> { "#36A2EB" }
            };

            await barChart.Clear();
            await barChart.AddLabelsDatasetsAndUpdate(labels, dataset);
        }

       
    }
}