using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QuanLiTuyenXeBusDalat.Models;

namespace QuanLiTuyenXeBusDalat.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaiXesController : ControllerBase
    {
        public static List<TaiXe> taiXes = new List<TaiXe>();


        [HttpGet]
        //Interface dùng để trả về cho các action
        public IActionResult GetAll()
        {
            // Trả về danh sách các tài xế
            return Ok(taiXes);
        }
        [HttpGet("{id}")]
        public IActionResult GetByID(string id)
        {
            try
            {
                //LinQ [Object] Query
                var taiXe = taiXes.SingleOrDefault(tx => tx.MaTaiXe == Guid.Parse(id));
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
            var taiXe = new TaiXe
            {
                TenTaiXe = taiXeVM.TenTaiXe,
                GioiTinh=taiXeVM.GioiTinh,
                NgaySinh=taiXeVM.NgaySinh,
                DiaChi=taiXeVM.DiaChi,
                QueQuan=taiXeVM.QueQuan,
                NgayBDHopDong=taiXeVM.NgayBDHopDong,
                Luong=taiXeVM.Luong,
                BangLai=taiXeVM.BangLai
            };
            taiXes.Add(taiXe);
            return Ok(new
            {
                Success = true,
                Data = taiXe
            });
        }

        [HttpPut("{id}")]
        public IActionResult Edit(Guid id, TaiXe taiXeEdit)
        {
            try
            {
                //LINQ [Object] Query
                var taiXe = taiXes.SingleOrDefault(tx => tx.MaTaiXe == id); ;
                if (taiXe == null) { return NotFound(); }
                if (id != taiXe.MaTaiXe) { return BadRequest(); }
                //Update
                taiXe.TenTaiXe = taiXeEdit.TenTaiXe;
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
                var taiXe = taiXes.SingleOrDefault(tx => tx.MaTaiXe == id);
                if (taiXe == null) { return NotFound(); }
                taiXes.Remove(taiXe);
                return Ok();
            }
            catch
            {
                return BadRequest();
            }
        }

    }
}
