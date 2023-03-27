using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QuanLiTuyenXeBusDalat.Data;
using QuanLiTuyenXeBusDalat.Models;

namespace QuanLiTuyenXeBusDalat.Controllers
{
    
    [Route("api/{v:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1.0")]
    public class XeV1Controller : ControllerBase
    {
        private readonly MyDBContext _context;
        public XeV1Controller(MyDBContext context)
        {
            _context = context;
        }
        [HttpGet]
        public IActionResult GetAll()
        {
            try
            {
                var dsXe = _context.Xes.ToList();
                return Ok(dsXe);
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpGet("id")]
        [Authorize]
        public IActionResult GetById(int id)
        {
            //SingleOrDefault: trả về giá trị mặc định của kiểu dữ liệu của danh sách nếu danh sách trống hoặc không tìm thấy bất kỳ phần tử nào thỏa mãn điều kiện
            //hoặc có nhiều hơn một phần tử thỏa mãn điều kiện.
            var dsXe = _context.Xes.SingleOrDefault(xe => xe.MaXe == id);
            if (dsXe != null)
            {
                return Ok(dsXe);
            }
            else
            {
                return NotFound();
            }
        }

        // Thêm 
        [HttpPost]
        [Authorize]
        //Phải cấu hình mới thực hiện thực được lệnh authorize
        // Phải đăng nhập mới được làm
        public IActionResult CreateNew(XeModel xeModel)
        {

            var xe = new Models.Xe
            {
                // Vì MaXe mình dang đặt cho nó là idnetity nên nó sẽ tự động tăng
                // Không cần khai báo vào trong này
                BienSo = xeModel.BienSo,
                LoaiXe = xeModel.LoaiXe,
                SoGhe = xeModel.SoGhe,
                CongSuat = xeModel.CongSuat,
                NgaySX = xeModel.NgaySX,
                ChuKyBaoHanh = xeModel.ChuKyBaoHanh
            };
            _context.Add(xe);
            return Ok(new
            {
                Success = true,
                Data = xe
            });

        }

        [HttpPut("{id}")]
        [Authorize]
        public IActionResult EditXe(int id, Models.Xe xeEdit)
        {
            try
            {
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
                return Ok();
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
                return Ok();
            }
            catch 
            {
                return BadRequest();
            }
        }
    }
}
