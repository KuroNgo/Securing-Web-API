using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuanLiTuyenXeBusDalat.Data
{
    [Table("Tuyen")]
    public class Tuyen
    {
        [Key]
        public int MaTuyen { get; set; }
        public string TenTuyen { get; set; }
        public DateTime ThoiGianBatDau { get; set; }
        public DateTime ThoiGianKetThuc { get; set; }
        public DateTime ThoiGianGianCach { get; set; }
        public string LoTrinhLuotDi { get; set; }
        public string LoTrinhLuotVe { get; set; }
        public string LoaiTuyen { get; set; }
    }
}
