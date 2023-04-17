using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QuanLiTuyenXeBusDalat.Data;
using QuanLiTuyenXeBusDalat.Models;
using System.ComponentModel.DataAnnotations;

namespace QuanLiTuyenXeBusDalat.Controllers
{
    // THông tin của bảng này được lấy từ database

    [Route("api/{v:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1.0")]
    //[DisableCors]
    public class Tuyen2V1Controller : ControllerBase
    {
        private readonly MyDBContext _context;

        public Tuyen2V1Controller(MyDBContext myDBContext)
        {
            _context=myDBContext;
        }

        [HttpGet]
        [Authorize]
        //Interface dùng để trả về cho các action
        public IActionResult GetAll()
        {
            // Trả về danh sách các hàng hóa
            try
            {
                var dsLoai = _context.tuyens.ToList();
                return Ok(dsLoai);
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpGet("{id}")]
        public IActionResult GetByID(int id)
        {
            var dsLoai = _context.tuyens.SingleOrDefault(loai =>
             loai.MaTuyen == id);
            if (dsLoai != null)
            {
                return Ok(dsLoai);
            }
            else
            {
                return NotFound();
            }
        }

        //Insert
        [HttpPost]
        public IActionResult Create([FromBody] TuyenVM tuyenVM)
        {
            var MaDonVi = _context.donViQuanLiXes.FirstOrDefault(dvql => dvql.MaDonVi == tuyenVM.MaDonVi);
            
            if (MaDonVi == null)
            {
                return NotFound("DonViQuanLiXe not found");
            }

            var tuyen = new Data.Tuyen
            {
                MaTuyen=0,
                TenTuyen = tuyenVM.TenTuyen,
                ThoiGianBatDau = tuyenVM.ThoiGianBatDau,
                ThoiGianKetThuc = tuyenVM.ThoiGianKetThuc,
                ThoiGianGianCach = tuyenVM.ThoiGianGianCach,
                LoTrinhLuotDi = tuyenVM.LoTrinhLuotDi,
                LoTrinhLuotVe = tuyenVM.LoTrinhLuotVe,
                LoaiTuyen = tuyenVM.LoaiTuyen,
            };
            _context.tuyens.Add(tuyen);
            _context.SaveChanges();
            return Ok(new
            {
                Success = true,
                Data = tuyen
            });
        }

        [HttpPut("{id}")]
        public IActionResult Edit(int id, Models.Tuyen tuyenEdit)
        {
            try
            {
                //LINQ [Object] Query
                var tuyen = _context.tuyens.SingleOrDefault(t => t.MaTuyen == id);
                if (tuyen == null) { return NotFound(); }
                if (id != tuyen.MaTuyen) { return BadRequest(); }
                //Update
                tuyen.TenTuyen = tuyenEdit.TenTuyen;
                tuyen.ThoiGianBatDau = tuyenEdit.ThoiGianBatDau;
                tuyen.ThoiGianKetThuc = tuyenEdit.ThoiGianKetThuc;
                tuyen.ThoiGianGianCach = tuyenEdit.ThoiGianGianCach;
                tuyen.LoTrinhLuotDi = tuyenEdit.LoTrinhLuotDi;
                tuyen.LoTrinhLuotVe = tuyenEdit.LoTrinhLuotVe;
                tuyen.LoaiTuyen = tuyenEdit.LoaiTuyen;
                tuyen.MaDonVi = tuyenEdit.MaDonVi;
                return Ok();
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                //Linq [object] Query
                var tuyen = _context.tuyens.SingleOrDefault(t => t.MaTuyen == id);
                if (tuyen != null) 
                {
                    _context.Remove(tuyen);
                    _context.SaveChanges();
                    return StatusCode(StatusCodes.Status200OK);
                }
                else
                {
                    return NotFound();
                }
            }
            catch
            {
                return BadRequest();
            }
        }
    }
}
