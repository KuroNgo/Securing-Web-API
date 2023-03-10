using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuanLiTuyenXeBusDalat.Data
{
    [Table("DonViQuanLiXe")]
    public class DonViQuanLiXe
    {
        [Key]
        public int MaDonVi { get; set; }
        public string TenDonVi { get; set; }
        public string DiaChi { get; set; }
        public int MyProperty { get; set; }
    }
}
