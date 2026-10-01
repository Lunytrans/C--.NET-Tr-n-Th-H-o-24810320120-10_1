namespace Bai2_QuanLyPhuongTien
{
    // D. Class QuanLyPhuongTien (Quản lý Tập hợp)
    public class QuanLyPhuongTien
    {
        private readonly List<PhuongTien> _danhSach = new List<PhuongTien>();

        // 1. Thêm phương tiện mới
        public void AddPhuongTien(PhuongTien pt)
        {
            _danhSach.Add(pt);
        }

        // 2. In ra danh sách toàn bộ phương tiện kèm Giá lăn bánh
        public void DisplayAll()
        {
            foreach (PhuongTien pt in _danhSach)
            {
                Console.WriteLine($"{pt.GetInfo()} | Giá lăn bánh: {pt.TinhGiaLanBanh():N0} VNĐ");
            }
        }

        // 3. Tìm và trả về phương tiện có Giá lăn bánh cao nhất (Đa hình)
        public PhuongTien? FindMaxGiaLanBanh()
        {
            PhuongTien? max = null;
            foreach (PhuongTien pt in _danhSach)
            {
                if (max == null || pt.TinhGiaLanBanh() > max.TinhGiaLanBanh())
                    max = pt;
            }
            return max;
        }

        // 4. Tìm danh sách phương tiện theo tên hãng (dùng LINQ)
        public List<PhuongTien> SearchByName(string keyword)
        {
            return _danhSach
                .Where(pt => pt.TenHang.Contains(keyword, StringComparison.OrdinalIgnoreCase))
                .ToList();
        }
    }
}
