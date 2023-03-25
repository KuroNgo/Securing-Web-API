using QuanLiTuyenXeBusDalat.Data;
using QuanLiTuyenXeBusDalat.Models;

namespace QuanLiTuyenXeBusDalat.Services
{
    public class DonViQuanLiXeRepositoryInMemory : IDonViQuanLiXe
    {
        static List<DonViQuanLiXeVM> donViQuanLiXes = new List<DonViQuanLiXeVM>
        {
            // TenDonVi, DiaCHi,SDT,Email
            new DonViQuanLiXeVM
            {
                MaDonVi=0,
                TenDonVi = "Công ty cp vt ôtô Lâm Đồng",
                DiaChi="Lâm Đồng",
                SoDienThoai= "0263.3822120",
                Email=""
            },

            new DonViQuanLiXeVM
            {
                MaDonVi=1,
                TenDonVi = "Công ty TNHH Phương Trang Đà Lạt",
                DiaChi="Lâm Đồng",
                SoDienThoai= "0263.3585858",
                Email=""
            },
           
            new DonViQuanLiXeVM
            {
                MaDonVi=2,
                TenDonVi = "Airport Shuttle Bus",
                DiaChi="Lâm Đồng",
                SoDienThoai= "0263.3822120",
                Email=""
            },

        };
        public DonViQuanLiXeVM Add(DonViQuanLiXeModel donViQuanLiXeModel)
        {
            var donViQuanLiXeVM = new DonViQuanLiXeVM
            {
                TenDonVi = donViQuanLiXeModel.TenDonVi,
                DiaChi =donViQuanLiXeModel.DiaChi,
                SoDienThoai=donViQuanLiXeModel.SoDienThoai,
                Email=donViQuanLiXeModel.Email
            };
           donViQuanLiXes.Add(donViQuanLiXeVM);
           return donViQuanLiXeVM;
        }

        public DonViQuanLiXeVM GetByName(string name)
        {
            return donViQuanLiXes.SingleOrDefault(donViQuanLiXes => donViQuanLiXes.TenDonVi == name);

        }

        public List<DonViQuanLiXeVM> GetDonViQuanLyXes()
        {
            return donViQuanLiXes;
        }

        public void Remove(string name)
        {
            throw new NotImplementedException();
        }

        public void Update(DonViQuanLiXeVM donViQuanLiXeVMs)
        {
            throw new NotImplementedException();
        }
    }
}
