namespace QuanLiTuyenXeBusDalat.Models
{
    public class TuyenVM
    {
        public string TenTuyen { get; set; }
        public DateTime ThoiGianBatDau { get; set; }
        public DateTime ThoiGianKetThuc { get; set; }
        public DateTime ThoiGianGianCach { get; set; }
        public string LoTrinhLuotDi { get; set; }
        public string LoTrinhLuotVe { get; set; }
        public string LoaiTuyen { get; set; }

    }

    public class Tuyen : TuyenVM
    {
        public int MaTuyen { get; set; }
    }
    public class TuyenModel
    {
        public string TenTuyen { get; set; }
        public DateTime ThoiGianBatDau { get; set; }
        public DateTime ThoiGianKetThuc { get; set; }
        public DateTime ThoiGianGianCach { get; set; }
        public string LoTrinhLuotDi { get; set; }
        public string LoTrinhLuotVe { get; set; }
        public string LoaiTuyen { get; set; }
        public int MaDonVi { get; set; }

    }
}
