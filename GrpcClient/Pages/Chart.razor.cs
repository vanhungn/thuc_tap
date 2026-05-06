using AntDesign.Charts;
using Microsoft.AspNetCore.Components;
using Server_Shared;
using Server_Shared.model;
using Title = AntDesign.Charts.Title;

namespace GrpcClient.Pages
{
    public partial class Chart : ComponentBase
    {
        [Inject] public ISVController _client { get; set; } = default!;

        List<ModelChart> chartData = new();

        readonly PieConfig pieConfig = new PieConfig
        {
            ForceFit = true,
            Title = new Title { Visible = true, Text = "Ti le sinh vien theo lop" },
            Radius = 0.8,
            Padding = "auto",
            AngleField = "soLuongSinhVien",
            ColorField = "name",
            Label = new PieLabelConfig { Visible = true },
            Legend = new Legend { Visible = true, Position = "bottom-center" }
        };
        readonly ColumnConfig columnConfig = new ColumnConfig
        {
            ForceFit = true,
            Title = new Title { Visible = true, Text = "So luong sinh vien theo lop" },
            XField = "name",
            YField = "soLuongSinhVien",
            ColorField = "name",
            Legend = new Legend { Visible = true, Position = "top" }
        };
        protected override async Task OnInitializedAsync()
        {
            await Load();
        }

        async Task Load()
        {
            chartData= await _client.GetChart(default);
        }
    }
}