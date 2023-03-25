using QuanLiTuyenXeBusDalat.Models;

namespace QuanLiTuyenXeBusDalat.Services
{
    public class TuyenRepositoryInMemory : ITuyenRepository
    {
        // Tạo mảng tỉnh
        static List<TuyenVM> tuyens = new List<TuyenVM>
        {
        //TenTuyen,ThoiGianBatDau,ThoiGianKetThuc,
        //ThoiGianGianCach,LoTrinhLuotDi,LoTrinhLuotVe
        //LoaiTuyen (SoTuyen)
            new TuyenVM
            {
                MaDonVi=0,
                TenTuyen = "Tuyến Đà Lạt – Đức Trọng",
                ThoiGianBatDau = "5h30",
                ThoiGianKetThuc = "19h00",
                ThoiGianGianCach = "15 - 30 phút chuyến",
                LoaiTuyen = "70",
                LoTrinhLuotDi="Bến xe buýt đường Mai Anh Đào, P8, Đà Lạt " +
                "– đường Phù Đổng Thiên Vương – đường Đinh Tiên Hoàng - " +
                "đường Nguyễn Thái Học- đường Lê Đại Hành - " +
                "khu Hoà Bình – Đường 3/2 – đường Trần Phú – " +
                "đường 3/4-QL 20- Bến xe Đức Trọng",
                LoTrinhLuotVe ="Bến xe Đức Trọng -  " +
                "đường 3/4-QL 20 – đường Trần Phú – " +
                "Đường 3/2 - khu Hoà Bình- đường Lê Đại Hành - " +
                "đường Nguyễn Thái Học – đường Đinh Tiên Hoàng – " +
                "đường Phù Đổng Thiên Vương –  " +
                "Bến xe buýt đường Mai Anh Đào, P8, Đà Lạt"
            },
            new TuyenVM
            {
                MaDonVi=1,
                TenTuyen = "Tuyến Đà Lạt – Đơn Dương",
                ThoiGianBatDau = "5h30",
                ThoiGianKetThuc = "19h00",
                ThoiGianGianCach = "15 - 30 phút chuyến",
                LoaiTuyen = "",
                LoTrinhLuotDi="Bến xe buýt đường Mai Anh Đào, P8, Đà Lạt – " +
              "đường Phù Đổng Thiên Vương – " +
              "đường Đinh Tiên Hoàng - " +
              "đường Nguyễn Thái Học - " +
              "đường Lê Đại Hành - " +
              "khu Hoà Bình – " +
              "Đường 3/2 - " +
              "Phan Đình Phùng - " +
              "Xô Viết Nghệ Tĩnh – " +
              "Tùng Lâm – " +
              "khu du lịch Langbiang ",
                LoTrinhLuotVe ="khu du lịch Langbiang - " +
              "Tùng Lâm - " +
              "Xô Viết Nghệ Tĩnh – " +
              "Phan Đình Phùng – " +
              "Đường 3/2 - " +
              "khu Hoà Bình - " +
              "đường Lê Đại Hành – " +
              "đường Nguyễn Thái Học – " +
              "đường Đinh Tiên Hoàng – " +
              "đường Phù Đổng Thiên Vương - " +
              "Bến xe buýt đường Mai Anh Đào, P8, Đà Lạt"
            },
            new TuyenVM
            {
                MaDonVi=1,
                TenTuyen = "Tuyến Đà Lạt – Lạc Dương",
                ThoiGianBatDau = "5h30",
                ThoiGianKetThuc = "19h00",
                ThoiGianGianCach = "15 - 30 phút chuyến",
                LoaiTuyen = "48",
                LoTrinhLuotDi="Bến xe buýt đường Mai Anh Đào, P8, Đà Lạt – " +
                "đường Phù Đổng Thiên Vương – đường Đinh Tiên Hoàng - " +
                "đường Nguyễn Thái Học- đường Lê Đại Hành - " +
                "khu Hoà Bình – Đường 3/2 - Phan Đình Phùng- " +
                "Xô Viết Nghệ Tĩnh – Tùng Lâm – khu du lịch Langbiang ",
                LoTrinhLuotVe ="khu du lịch Langbiang - Tùng Lâm , " +
                "Xô Viết Nghệ Tĩnh – Phan Đình Phùng – " +
                "Đường 3/2 - khu Hoà Bình- đường Lê Đại Hành - " +
                "đường Nguyễn Thái Học – " +
                "đường Đinh Tiên Hoàng – " +
                "đường Phù Đổng Thiên Vương - " +
                "Bến xe buýt đường Mai Anh Đào, P8, Đà Lạt,"
            },
            new TuyenVM
            {
                MaDonVi=1,
                TenTuyen = "Tuyến Đà Lạt – Bảo Lộc",
                ThoiGianBatDau = "5h30",
                ThoiGianKetThuc = "19h00",
                ThoiGianGianCach = "15 - 30 phút chuyến",
                LoaiTuyen = "60",
                LoTrinhLuotDi=" Bến xe buýt đường Mai Anh Đào, P8, Đà Lạt – " +
                "đường Phù Đổng Thiên Vương – " +
                "đường Đinh Tiên Hoàng - " +
                "đường Nguyễn Thái Học - " +
                "đường Lê Đại Hành - " +
                "khu Hoà Bình – " +
                "Đường 3/2 – " +
                "đường Trần Phú – " +
                "đường 3/4 - " +
                "QL 20 – " +
                "đường Thống Nhất (Đức Trọng) – " +
                "Ngã ba Phú Hội – " +
                "QL20 – " +
                "đường Lê Hồng Phong (Bảo Lộc) – " +
                "đường Nguyễn Công Trứ - " +
                "đường 28/3 – " +
                "số 458 Trần Phú Bảo Lộc",
                LoTrinhLuotVe ="số 458 Trần Phú Bảo Lộc – " +
                "đường 28/3 - " +
                "đường Nguyễn Công Trứ – " +
                "đường Lê Hồng Phong (Bảo Lộc) – " +
                "QL20 –  " +
                "Ngã ba Phú Hội – " +
                "đường Thống Nhất (Đức Trọng) - " +
                "QL 20 - " +
                "đường 3/4 - " +
                "đường Trần Phú - " +
                "Đường 3/2 - " +
                "khu Hoà Bình - " +
                "đường Lê Đại Hành - " +
                "đường Nguyễn Thái Học - " +
                "đường Đinh Tiên Hoàng - " +
                "đường Phù Đổng Thiên Vương – " +
                "Bến xe buýt đường Mai Anh Đào, P8, Đà Lạt "
            },

            new TuyenVM
            {
                MaDonVi=1,
                TenTuyen = "Tuyến Đà Lạt – Xuân Trường",
                ThoiGianBatDau = "5h30",
                ThoiGianKetThuc = "19h00",
                ThoiGianGianCach = "15 - 30 phút chuyến",
                LoaiTuyen = "40",
                LoTrinhLuotDi="Bến xe buýt đường Mai Anh Đào, P8, Đà Lạt – " +
                "đường Phù Đổng Thiên Vương – " +
                "đường Đinh Tiên Hoàng - " +
                "đường Nguyễn Thái Học - " +
                "đường Lê Đại Hành - " +
                "khu Hoà Bình – " +
                "Đường 3/2 – " +
                "đường Trần Phú – " +
                "đườngTrần Hưng Đạo – " +
                "QL 20 – " +
                "Thôn Trạm Hành 2",
                LoTrinhLuotVe ="Thôn Trạm Hành 2 - " +
                "QL 20 - " +
                "đườngTrần Hưng Đạo – " +
                "đường Trần Phú – " +
                "Đường 3/2 - " +
                "khu Hoà Bình- " +
                "đường Lê Đại Hành - " +
                "đường Nguyễn Thái Học – " +
                "đường Đinh Tiên Hoàng – " +
                "đường Phù Đổng Thiên Vương-P8 -" +
                "Đà Lạt,Bến xe buýt đường Mai Anh Đào"
            },
            new TuyenVM
            {
                MaDonVi=0,
                TenTuyen = "Tuyến Đà Lạt – Phú Sơn",
                ThoiGianBatDau = "5h30",
                ThoiGianKetThuc = "19h00",
                ThoiGianGianCach = "15 - 30 phút chuyến",
                LoaiTuyen = "",
                LoTrinhLuotDi="Bến xe buýt đường Mai Anh Đào, P8, Đà Lạt – " +
                "đường Phù Đổng Thiên Vương – " +
                "đường Đinh Tiên Hoàng - " +
                "đường Nguyễn Thái Học - " +
                "đường Lê Đại Hành - " +
                "khu Hoà Bình – " +
                "Đường 3/2 – " +
                "đường Hòang Văn Thụ - " +
                "ĐT 725 – " +
                "Tà Nung – " +
                "Nam Ban – " +
                "QL27 – " +
                "Đinh Văn – " +
                "Phú Sơn",
                LoTrinhLuotVe ="Phú Sơn - " +
                "Đinh Văn - " +
                "QL27 – " +
                "Nam Ban – " +
                "Tà Nung - " +
                "ĐT 725 - " +
                "đường Hòang Văn Thụ - " +
                "Đường 3/2 - " +
                "khu Hoà Bình- " +
                "đường Lê Đại Hành - " +
                "đường Nguyễn Thái Học – " +
                "đường Đinh Tiên Hoàng – " +
                "đường Phù Đổng Thiên Vương-P8 -" +
                "Đà Lạt,Bến xe buýt đường Mai Anh Đào"
            },
            new TuyenVM
            {
                MaDonVi=0,
                TenTuyen = "Tuyến Liên nghĩa – Tân Thanh",
                ThoiGianBatDau = "5h30",
                ThoiGianKetThuc = "19h00",
                ThoiGianGianCach = "15 - 30 phút chuyến",
                LoaiTuyen = "",
                LoTrinhLuotDi=" BX Đức Trọng – " +
                "QL 20 – " +
                "thôn Bạch Đằng – " +
                "N’ thôn Hạ QL 27 - " +
                "Đinh Văn – " +
                "Chợ Tân Thanh (Lâm Hà)",
                LoTrinhLuotVe ="Chợ Tân Thanh (Lâm Hà) - " +
                "Đinh Văn - " +
                "N’ thôn Hạ QL 27 – " +
                "thôn Bạch Đằng – " +
                "QL 20 - " +
                "BX Đức Trọng"
            },
            new TuyenVM
            {
                MaDonVi=2,
                TenTuyen = "Tuyến Đà Lạt – Sân bay Liên Khương",
                ThoiGianBatDau = "5h30",
                ThoiGianKetThuc = "19h00",
                ThoiGianGianCach = "15 - 30 phút chuyến",
                LoaiTuyen = "",
                LoTrinhLuotDi="Số 1 Lê Thị Hồng Gấm - Sân bay Liên Khương",
                LoTrinhLuotVe =""
            },
            new TuyenVM
            {
                MaDonVi=1,
                TenTuyen = "Tuyến Đà Lạt – Đại Lào",
                ThoiGianBatDau = "5h30",
                ThoiGianKetThuc = "19h00",
                ThoiGianGianCach = "15 - 30 phút chuyến",
                LoaiTuyen = "47",
                LoTrinhLuotDi="Bến xe nội thành – Khu Hòa Bình – 3/2 – Trần Phú – Ngã tư kim Cúc – 3/4 – đèo Prenn – Qlộ 20 – Ngã ba Finom – thị xã Bảo Lộc – chợ Bảo Lộc – Nguyễn Công Trứ – đường 28/3 – Trần Phú – chợ Đại Lào.",
                LoTrinhLuotVe =""
            },
            new TuyenVM
            {
                MaDonVi=1,
                TenTuyen = "Tuyến Đà Lạt – Lạc Dương",
                ThoiGianBatDau = "5h30",
                ThoiGianKetThuc = "19h00",
                ThoiGianGianCach = "30 - 45 phút chuyến",
                LoaiTuyen = "36",
                LoTrinhLuotDi="Bến xe nội thành – Khu Hòa Bình – 3/2 – Trần Phú – Palace – Hồ Tùng Mậu – Yersin – Quang Trung – Chi Lăng – Mê Linh – chợ Thái Phiên.",
                LoTrinhLuotVe=""
            },
        };
        public TuyenVM Add(TuyenModel tuyenModel)
        {
            var tuyen = new TuyenVM
            {
                MaDonVi = tuyenModel.MaDonVi,
                TenTuyen = tuyenModel.TenTuyen,
                ThoiGianBatDau = tuyenModel.ThoiGianBatDau,
                ThoiGianKetThuc = tuyenModel.ThoiGianKetThuc,
                ThoiGianGianCach = tuyenModel.ThoiGianGianCach,
                LoaiTuyen = tuyenModel.LoaiTuyen,
                LoTrinhLuotDi = tuyenModel.LoTrinhLuotDi,
                LoTrinhLuotVe = tuyenModel.LoTrinhLuotVe
            };
            tuyens.Add(tuyen);
            return tuyen;
        }



        public TuyenVM GetByName(string tenTuyen)
        {
            return tuyens.SingleOrDefault(tuyen => tuyen.TenTuyen == tenTuyen);
        }

        public List<TuyenVM> GetTuyens()
        {
            return tuyens;
        }

        public void Remove(string name)
        {
            var _tuyen = tuyens.SingleOrDefault(tuyen => tuyen.TenTuyen == name);
            tuyens.Remove(_tuyen);
        }

        public void Update(TuyenVM tuyenVM)
        {
            var _tuyen = tuyens.SingleOrDefault(tuyen => tuyen.TenTuyen == tuyen.TenTuyen);
            if (_tuyen != null)
            {
                _tuyen.TenTuyen = tuyenVM.TenTuyen;
            }
        }
    }
}
