using QuanLiTuyenXeBusDalat.Models;

namespace QuanLiTuyenXeBusDalat.Services
{
    public class TaiKhoanRepositoryInMemory : ITaiKhoanRepository
    {
        static List<LoginModel> taiKhoans = new List<LoginModel>
        {
            new LoginModel
            {
                UserName="admin",
                Password="123"
            }
        };
        public LoginModel Add(LoginModel taiKhoanModel)
        {
            var taiKhoan = new LoginModel
            {
                UserName = taiKhoanModel.UserName,
                Password = taiKhoanModel.Password
            };
            taiKhoans.Add(taiKhoan);
            return taiKhoan;
        }
    }
}
