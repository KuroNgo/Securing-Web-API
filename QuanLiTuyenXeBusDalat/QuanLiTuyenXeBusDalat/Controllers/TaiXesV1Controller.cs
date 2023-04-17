using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QuanLiTuyenXeBusDalat.Data;
using QuanLiTuyenXeBusDalat.Models;

namespace QuanLiTuyenXeBusDalat.Controllers
{
    
    [Route("api/{v:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1.0")]

    public class TaiXesV1Controller : ControllerBase
    {
        private readonly MyDBContext _context;
        public TaiXesV1Controller(MyDBContext context)
        {
            _context = context;
        }

        [HttpGet]
        //Interface dùng để trả về cho các action
        public IActionResult GetAll()
        {
            // Trả về danh sách các hàng hóa
            try
            {
                var dsLoai = _context.TaiXes.ToList();
                return Ok(dsLoai);
            }
            catch
            {
                return BadRequest();
            }
        }
        [HttpGet("{id}")]
        public IActionResult GetByID(string id)
        {
            try
            {
                //LinQ [Object] Query
                var taiXe = _context.TaiXes.SingleOrDefault(tx => tx.MaTX == Guid.Parse(id));
                if (taiXe == null)
                {
                    return NotFound();
                }
                return Ok(taiXe);
            }
            catch
            {
                return BadRequest();
            }
        }
        //Insert
        [HttpPost]
        public IActionResult Create(TaiXeVM taiXeVM)
        {
            var taiXe = new Data.TaiXe
            {
                HoVaTen = taiXeVM.TenTaiXe,
                GioiTinh=taiXeVM.GioiTinh,
                NgaySinh=taiXeVM.NgaySinh,
                DiaChi=taiXeVM.DiaChi,
                QueQuan=taiXeVM.QueQuan,
                NgayBDHopDong=taiXeVM.NgayBDHopDong,
                Luong=taiXeVM.Luong,
                BangLai=taiXeVM.BangLai
            };
            _context.Add(taiXe);
            _context.SaveChanges();
            return Ok(new
            {
                Success = true,
                Data = taiXe
            });
        }

        [HttpPut("{id}")]
        public IActionResult Edit(Guid id, Models.TaiXe taiXeEdit)
        {
            try
            {
                //LINQ [Object] Query
                var taiXe = _context.TaiXes.SingleOrDefault(tx => tx.MaTX == id); ;
                if (taiXe == null) { return NotFound(); }
                if (id != taiXe.MaTX) { return BadRequest(); }
                //Update
                taiXe.HoVaTen = taiXeEdit.TenTaiXe;
                taiXe.GioiTinh = taiXeEdit.GioiTinh;
                taiXe.NgaySinh = taiXeEdit.NgaySinh;
                taiXe.DiaChi = taiXeEdit.DiaChi;
                taiXe.QueQuan = taiXeEdit.QueQuan;
                taiXe.NgayBDHopDong = taiXeEdit.NgayBDHopDong;
                taiXe.Luong = taiXeEdit.Luong;
                taiXe.BangLai=taiXeEdit.BangLai;
                return Ok();
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            try
            {
                //Linq [object] Query
                var taiXe = _context.TaiXes.SingleOrDefault(tx => tx.MaTX == id);
                if (taiXe == null) { return NotFound(); }
                _context.Remove(taiXe);
                return Ok();
            }
            catch
            {
                return BadRequest();
            }
        }

    }
}
