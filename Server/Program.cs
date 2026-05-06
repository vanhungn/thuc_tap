using Grpc.AspNetCore.Web;
using Microsoft.Data.SqlClient;
using NHibernate;
using ProtoBuf.Grpc.Server;
using Server.Controller;
using Server.Mappings;
using Server.Repository;
using Server.Repository.ILopHocRepo;
using Server.Repository.ISinhVienRepo;
using Server.Services;

var builder = WebApplication.CreateBuilder(args);

// ✅ AddCodeFirstGrpc thay vì AddGrpc
builder.Services.AddCodeFirstGrpc();

// ✅ CORS đầy đủ headers
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy
            .AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader()
            .WithExposedHeaders(
                "grpc-status",
                "grpc-message",
                "grpc-encoding",
                "grpc-accept-encoding",
                "content-type",
                "x-grpc-web",
                "x-user-agent"
            );
    });
});

// ✅ DB + NHibernate
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
var sessionFactory = NHibernateHelper.CreateSessionFactory(connectionString ?? "");

using (var conn = new SqlConnection(connectionString))
{
    conn.Open();
    Console.WriteLine("✅ CONNECT OK");
}

builder.Services.AddSingleton(sessionFactory);
builder.Services.AddScoped<NHibernate.ISession>(factory =>
{
    var sf = factory.GetRequiredService<ISessionFactory>();
    return sf.OpenSession();
});

// ✅ DI SinhVien
builder.Services.AddScoped<ISearchSinhVien, RepoSinhVien>();
builder.Services.AddScoped<IAddSinhVien, RepoSinhVien>();
builder.Services.AddScoped<IDeleteSinhVien, RepoSinhVien>();
builder.Services.AddScoped<IGetAllSinhVien, RepoSinhVien>();
builder.Services.AddScoped<IUpdateSinhVien, RepoSinhVien>();
builder.Services.AddScoped<IGetChart, RepoSinhVien>();
builder.Services.AddScoped<ISinhVienService, SinhvienService>();

// ✅ DI LopHoc
builder.Services.AddScoped<IGetAllLopHoc, RepoLopHoc>();
builder.Services.AddScoped<ILopHocService, LopHocService>();

var app = builder.Build();

// ✅ Kiểm tra DB khi khởi động
using (var scope = app.Services.CreateScope())
{
    var repo = scope.ServiceProvider.GetRequiredService<IGetAllSinhVien>();
    var data = repo.GetAll();
    Console.WriteLine($"✅ Tổng số sinh viên: {data.Count}");
}

// ✅ Thứ tự pipeline ĐÚNG: Routing → CORS → GrpcWeb → Map
app.UseRouting();
app.UseCors("AllowAll");
app.UseGrpcWeb(new GrpcWebOptions { DefaultEnabled = true });

app.MapGrpcService<SvController>()
   .EnableGrpcWeb()
   .RequireCors("AllowAll");

app.MapGrpcService<LhController>()
   .EnableGrpcWeb()
   .RequireCors("AllowAll");

app.MapGet("/", () => "gRPC Server is running ✅");

app.Run();