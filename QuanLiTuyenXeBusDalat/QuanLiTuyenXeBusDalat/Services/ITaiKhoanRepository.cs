using QuanLiTuyenXeBusDalat.Models;

namespace QuanLiTuyenXeBusDalat.Services
{
    public interface ITaiKhoanRepository
    {
        LoginModel Add(DonViQuanLiXeModel donViQuanLiXeModel);
        List<DonViQuanLiXeVM> GetDonViQuanLyXes();
        DonViQuanLiXeVM GetByName(string name);
        public void Remove(string name);
        public void Update(DonViQuanLiXeVM donViQuanLiXeVMs);
    }
}
