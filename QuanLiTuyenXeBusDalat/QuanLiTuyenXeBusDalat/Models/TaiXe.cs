namespace QuanLiTuyenXeBusDalat.Models
{
    public class TaiXeVM
    {
        public string TenTaiXe { get; set; }
        public bool GioiTinh { get; set; }
        public DateTime NgaySinh { get; set; }
    }

    public class TaiXe : TaiXeVM
    {
        public Guid MaTaiXe { get; set; }
    }
}
