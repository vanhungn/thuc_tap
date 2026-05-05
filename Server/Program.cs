using Microsoft.Data.SqlClient;
using Server.Controller;
using Server.Repository;
using Server.Repository.ILopHocRepo;
using Server.Repository.ISinhVienRepo;
using Server.Services;
using Server.Mappings;
using NHibernate;

var builder = WebApplication.CreateBuilder(args);

// Add gRPC
builder.Services.AddGrpc();

// CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy
            .AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader()
            .WithExposedHeaders("grpc-status", "grpc-message");
    });
});

// DB
/*builder.Services.AddScoped<SqlConnection>(conn =>
    new SqlConnection(builder.Configuration.GetConnectionString("DefaultConnection")));*/

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
var sessionFactory = NHibernateHelper.CreateSessionFactory(connectionString??"");
using (var conn = new SqlConnection(connectionString))
{
    conn.Open();
    Console.WriteLine("✅ CONNECT OK");
}
builder.Services.AddSingleton(sessionFactory);

builder.Services.AddScoped(factory =>
{
    var sf = factory.GetRequiredService<ISessionFactory>();
    return sf.OpenSession();
});
// DI SinhVien
builder.Services.AddScoped<ISearchSinhVien, RepoSinhVien>();
builder.Services.AddScoped<IAddSinhVien, RepoSinhVien>();
builder.Services.AddScoped<IDeleteSinhVien, RepoSinhVien>();
builder.Services.AddScoped<IGetAllSinhVien, RepoSinhVien>();
builder.Services.AddScoped<IUpdateSinhVien, RepoSinhVien>();
builder.Services.AddScoped<IGetChart, RepoSinhVien>();
builder.Services.AddScoped<ISinhVienService, SinhvienService>();

// DI LopHoc
builder.Services.AddScoped<IGetAllLopHoc, RepoLopHoc>();
builder.Services.AddScoped<ILopHocService, LopHocService>();

var app = builder.Build();

app.UseRouting();
app.UseCors("AllowAll");
app.UseGrpcWeb();

app.MapGrpcService<SinhVienController>()
   .EnableGrpcWeb()        // ✅ THÊM
   .RequireCors("AllowAll");

app.MapGrpcService<LopHocController>()
   .EnableGrpcWeb()        // ✅ THÊM
   .RequireCors("AllowAll");

app.MapGet("/", () => "gRPC running");
app.Run();