using System.Text;
using Bai2_QuanLyPhuongTien;

Console.OutputEncoding = Encoding.UTF8;

// TC01: Kiểm tra Validation Năm sản xuất
Console.WriteLine("=== TC01: Khởi tạo Ô tô có NamSanXuat = 1850 ===");
try
{
    OTo otoLoi = new OTo("OT00", "Toyota", 1850, 1_000_000_000m, 5, 2.0);
}
catch (ArgumentException ex)
{
    Console.WriteLine($"ArgumentException: {ex.Message}");
}

// TC02: Kiểm tra Tính Giá Lăn Bánh Ô tô
Console.WriteLine("\n=== TC02: Ô tô 5 chỗ, GiaGoc = 1,000,000,000 VNĐ ===");
OTo oto = new OTo("OT01", "Toyota", 2023, 1_000_000_000m, 5, 2.0);
Console.WriteLine($"Giá lăn bánh: {oto.TinhGiaLanBanh():N0} VNĐ");

// TC03: Kiểm tra Tính Giá Lăn Bánh Xe máy
Console.WriteLine("\n=== TC03: Xe máy 150cc, GiaGoc = 50,000,000 VNĐ ===");
XeMay xeMay = new XeMay("XM01", "Honda", 2024, 50_000_000m, 150);
Console.WriteLine($"Giá lăn bánh: {xeMay.TinhGiaLanBanh():N0} VNĐ");

// TC04: Kiểm tra Đa hình List<PhuongTien>
Console.WriteLine("\n=== TC04: Đa hình List<PhuongTien> ===");
List<PhuongTien> list = new List<PhuongTien> { oto, xeMay };
foreach (PhuongTien pt in list)
{
    Console.WriteLine($"{pt.GetType().Name} - {pt.MaPT}: {pt.TinhGiaLanBanh():N0} VNĐ");
}

// TC05: Kiểm tra Tìm Giá Lăn Bánh Max
Console.WriteLine("\n=== TC05: FindMaxGiaLanBanh() ===");
QuanLyPhuongTien ql = new QuanLyPhuongTien();
ql.AddPhuongTien(oto);
ql.AddPhuongTien(xeMay);
ql.DisplayAll();
PhuongTien? max = ql.FindMaxGiaLanBanh();
Console.WriteLine($"Giá lăn bánh cao nhất: {max?.GetInfo()} | {max?.TinhGiaLanBanh():N0} VNĐ");
