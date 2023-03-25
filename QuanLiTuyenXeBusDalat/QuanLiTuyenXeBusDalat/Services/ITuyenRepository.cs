using QuanLiTuyenXeBusDalat.Models;

namespace QuanLiTuyenXeBusDalat.Services
{
    public interface ITuyenRepository
    {
        TuyenVM Add(TuyenModel tuyenModel);
        List<TuyenVM> GetTuyens();
        TuyenVM GetByName(string name);
        public void Remove(string name);
        public void Update(TuyenVM tuyenVM);
    }
}
