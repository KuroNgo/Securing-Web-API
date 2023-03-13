using System.ComponentModel.DataAnnotations;

namespace QuanLiTuyenXeBusDalat.Models
{
    // Cái chỗ này là để người dùng nhập zào
    public class TaiXeVM
    {
        public string TenTaiXe { get; set; }
        public bool GioiTinh { get; set; }
        public DateTime NgaySinh { get; set; }
        public string DiaChi { get; set; }
        public string QueQuan { get; set; }
        public DateTime NgayBDHopDong { get; set; }
        public double Luong { get; set; }
        public string BangLai { get; set; }
    }

    // Cái chỗ này khi mà người dùng thêm thành công thì cái mã sẽ tự tăng +1
    public class TaiXe : TaiXeVM
    {
        public Guid MaTaiXe { get; set; }
    }
}
