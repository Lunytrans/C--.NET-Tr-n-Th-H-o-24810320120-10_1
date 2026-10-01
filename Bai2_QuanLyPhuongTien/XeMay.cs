namespace Bai2_QuanLyPhuongTien
{
    // C. Class XeMay kế thừa từ PhuongTien
    public class XeMay : PhuongTien
    {
        // Property bổ sung: DungTichXylanh (int, tính bằng cc)
        public int DungTichXylanh { get; set; }

        public XeMay(string maPT, string tenHang, int namSanXuat, decimal giaGoc, int dungTichXylanh)
            : base(maPT, tenHang, namSanXuat, giaGoc)
        {
            DungTichXylanh = dungTichXylanh;
        }

        public override decimal TinhGiaLanBanh()
        {
            if (DungTichXylanh < 175)
                return GiaGoc + GiaGoc * 0.02m; // Trước bạ 2%
            return GiaGoc + GiaGoc * 0.05m;     // Trước bạ 5%
        }
    }
}
