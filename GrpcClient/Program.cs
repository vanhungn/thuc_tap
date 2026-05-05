using Blazorise;
using Blazorise.Bootstrap;
using Blazorise.Icons.FontAwesome;
using Grpc.Net.Client;
using Grpc.Net.Client.Web;
using grpcClientSinhVien.Protos;
using grpcClientLopHoc.proto;
using GrpcClient;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

// ⭐ gRPC client DI
builder.Services.AddScoped(sp =>
{
    var httpHandler = new GrpcWebHandler(GrpcWebMode.GrpcWeb, new HttpClientHandler());

    var channel = GrpcChannel.ForAddress("https://localhost:7019", new GrpcChannelOptions
    {
        HttpHandler = httpHandler
    });

    return new SvService.SvServiceClient(channel);
});
builder.Services.AddScoped(sp =>
{
    var httpHandler = new GrpcWebHandler(GrpcWebMode.GrpcWeb, new HttpClientHandler());

    var channel = GrpcChannel.ForAddress("https://localhost:7019", new GrpcChannelOptions
    {
        HttpHandler = httpHandler
    });

    return new SvServiceLopHoc.SvServiceLopHocClient(channel);
});
builder.Services
    .AddBlazorise(options =>
    {
        options.Immediate = true;
    })
    .AddBootstrapProviders()        // không phải AddBootstrap5Providers()
    .AddFontAwesomeIcons();
builder.Services.AddScoped(sp =>
    new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

await builder.Build().RunAsync();
