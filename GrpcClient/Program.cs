using Grpc.Net.Client;
using Grpc.Net.Client.Web;
using GrpcClient;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using ProtoBuf.Grpc.Client;
using Server_Shared;
using AntDesign;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

GrpcClientFactory.AllowUnencryptedHttp2 = true;

builder.Services.AddScoped(sp =>
{
    var httpHandler = new GrpcWebHandler(GrpcWebMode.GrpcWeb, new HttpClientHandler());
    return GrpcChannel.ForAddress("https://localhost:7019", new GrpcChannelOptions
    {
        HttpHandler = httpHandler
    });
});

builder.Services.AddScoped<ISVController>(sp =>
{
    var channel = sp.GetRequiredService<GrpcChannel>();
    return channel.CreateGrpcService<ISVController>();
});

builder.Services.AddScoped<ILHController>(sp =>
{
    var channel = sp.GetRequiredService<GrpcChannel>();
    return channel.CreateGrpcService<ILHController>();
});

// ✅ Chỉ dùng AntDesign
builder.Services.AddAntDesign();

builder.Services.AddScoped(sp =>
    new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

await builder.Build().RunAsync();