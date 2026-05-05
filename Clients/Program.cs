using Grpc.Net.Client;
using Clients.Protos;
using Google.Protobuf.WellKnownTypes;
using Clients.controller;
using Clients.model;
using ClientLopHoc.proto;
using Clients.services;
using static Clients.services.SinhVienService;

// The port number must match the port of the gRPC server.
using var channel = GrpcChannel.ForAddress("https://localhost:7019");
var client = new SvService.SvServiceClient(channel);
var clientLopHoc = new SvServiceLopHoc.SvServiceLopHocClient(channel);
var GetAllSinhVien = await client.GetAllAsync(new Empty());
var GetAllLopHoc = await clientLopHoc.GetAllLopHocAsync(new Empty());
SinhVienService.IserviceSinhVien ServiceSV = new SinhVienService.ServiceSinhVien(client);
ServiceLopHoc.ILopHocService ServiceLH = new ServiceLopHoc.LopHocService(clientLopHoc);
var ControllerSv = new ClientSinhVien(ServiceSV, ServiceLH);

await ControllerSv.loadData();
bool thoat = true;
while (thoat)
{
    Console.WriteLine("Menu chuc nang");
    Console.WriteLine("1 Xem danh sach sinh vien");
    Console.WriteLine("2 Them sinh vien");
    Console.WriteLine("3 Sua thong tin sinh vien");
    Console.WriteLine("4 Xoa sinh vien");
    Console.WriteLine("5 Tim kiem sinh vien theo ma so sinh vien");
    Console.WriteLine("6 Thoat chuong trinh");
    Console.WriteLine("Chon chuc nang theo so");
    Console.WriteLine("----------------------------------------------------------");
    int chon;
    while (!int.TryParse(Console.ReadLine(), out chon))
    {
        Console.WriteLine("Nhap so!");
    }
    switch (chon)
    {
        case 1: { ControllerSv.Xem(); break; };
        case 2: { await ControllerSv.AddSinhVien(); break; };
        case 3: { await ControllerSv.Sua(); break; };
        case 4: { await ControllerSv.Xoa(); break; };
        case 5: { await ControllerSv.TimKiem(); break; };
        case 6: { thoat = false; break; };
        default:
            {
                Console.WriteLine("Chon tu 1 -> 6");
                break;
            }

    }
}
Console.ReadKey();


