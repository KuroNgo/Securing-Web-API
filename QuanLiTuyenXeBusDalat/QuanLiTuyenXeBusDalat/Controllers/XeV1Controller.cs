using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using Microsoft.EntityFrameworkCore;
using QuanLiTuyenXeBusDalat.Data;
using QuanLiTuyenXeBusDalat.Models;

namespace QuanLiTuyenXeBusDalat.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableRateLimiting("Api")]
    //[Route("api/{v:apiVersion}/[controller]")]
    //[ApiController]
    //[ApiVersion("1.0")]
    public class XeV1Controller : ControllerBase
    {
        private readonly MyDBContext _context;
        public XeV1Controller(MyDBContext context)
        {
            _context = context;
        }
        [HttpGet]
        [Authorize]
        public IActionResult GetAll()
        {
            try
            {
                var Tuyen = _context.Xes.Include(t => t.Tuyen).ToList();
                var dsXe = _context.Xes
                     .Select(x => new
                     {
                         MaXe=x.MaXe,
                         BienSo = x.BienSo,
                         LoaiXe = x.LoaiXe,
                         SoGhe = x.SoGhe,
                         CongSuat = x.CongSuat,
                         ChuKyBaoHanh = x.ChuKyBaoHanh,
                         NgaySX = x.NgaySX,
                         Tuyen=x.Tuyen
                     });

                return Ok(dsXe);
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpGet("id")]
        public IActionResult GetById(int id)
        {
            var query = from xe in _context.Xes
                        join tuyen in _context.tuyens on xe.MaTuyen
                        equals tuyen.MaTuyen
                        where xe.MaTuyen == id
                        select new
                        {
                            xe.MaXe,
                            xe.MaTuyen,
                            xe.BienSo,
                            xe.ChuKyBaoHanh,
                            xe.NgaySX,
                            xe.LoaiXe,
                            xe.CongSuat
                        };
            var result = query.FirstOrDefault();
            if(result == null)
            {
                return NotFound();
            }
            return Ok(new ApiResponse
            {
                Success = true,
                Data = result
            });
        }

        // Thêm 
        [HttpPost]
        [Authorize]
        //Phải cấu hình mới thực hiện thực được lệnh authorize
        // Phải đăng nhập mới được làm
        public IActionResult CreateNew(XeVM XeModel)
        {
            var maTuyen = _context.tuyens.FirstOrDefault(t => t.MaTuyen == XeModel.MaTuyen);
            if (maTuyen == null)
            {
                return NotFound("Tuyen Id not found");
            }
            var xe = new Data.Xe
            {
                MaXe=0,
                MaTuyen=XeModel.MaTuyen,
                // Vì MaXe mình dang đặt cho nó là identity nên nó sẽ tự động tăng
                // Không cần khai báo vào trong này
                BienSo = XeModel.BienSo,
                LoaiXe = XeModel.LoaiXe,
                SoGhe = XeModel.SoGhe,
                CongSuat = XeModel.CongSuat,
                NgaySX = XeModel.NgaySX,
                ChuKyBaoHanh = XeModel.ChuKyBaoHanh
            };
            _context.Xes.Add(xe);
            _context.SaveChanges();
            return Ok(new
            {
                Success = true,
                Data = xe
            });

        }

        [HttpPut("{id}")]
   
        public IActionResult EditXe(int id, Models.Xe xeEdit)
        {
            try
            {
                var maTuyen = _context.tuyens.FirstOrDefault(t => t.MaTuyen == xeEdit.MaTuyen);
                var xe = _context.Xes.SingleOrDefault(xe => xe.MaXe == id);
                if(xe == null)
                {
                    return NotFound();
                }
                if (id != xe.MaXe)
                {
                    return BadRequest();
                }
                //Update 
                xe.BienSo = xeEdit.BienSo;
                xe.LoaiXe = xeEdit.LoaiXe;
                xe.CongSuat = xeEdit.CongSuat;
                xe.ChuKyBaoHanh = xeEdit.ChuKyBaoHanh;
                xe.SoGhe = xeEdit.SoGhe;
                xe.NgaySX = xeEdit.NgaySX;
                xe.MaTuyen = xeEdit.MaTuyen;
                return Ok(new ApiResponse
                {
                    Success = true,
                    Message = "cập nhật thành công xe có id là " + xe.MaXe
                });
            }
            catch 
            {
                return BadRequest();
            }
        }

        [HttpDelete("{id}")]
        [Authorize]
        public IActionResult Delete(int id)
        {
            try
            {
                var xe = _context.Xes.SingleOrDefault(xe => xe.MaXe == id);
                if(xe == null)
                {
                    return NotFound();
                }
                _context.Remove(xe);
                _context.SaveChanges();
                return Ok(new ApiResponse
                {
                    Success=true,
                    Message="Đã xóa xe có id là"+xe.MaXe
                });
            }
            catch 
            {
                return BadRequest();
            }
        }
    }
}
