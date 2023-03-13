using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QuanLiTuyenXeBusDalat.Data;
using QuanLiTuyenXeBusDalat.Models;

namespace QuanLiTuyenXeBusDalat.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DonViQuanLiXeController : ControllerBase
    {
        public static List<DonViQuanLiXe> donViQuanLiXes = new List<DonViQuanLiXe>();


        [HttpGet]
        //Interface dùng để trả về cho các action
        public IActionResult GetAll()
        {
            // Trả về danh sách các hàng hóa
            return Ok(donViQuanLiXes);
        }
        [HttpGet("{id}")]
        public IActionResult GetByID(int id)
        {
            try
            {
                //LinQ [Object] Query
                var donViQuanLiXe = donViQuanLiXes.SingleOrDefault(dvql => dvql.MaDonVi == id);
                if (donViQuanLiXe == null)
                {
                    return NotFound();
                }
                return Ok(donViQuanLiXe);
            }
            catch
            {
                return BadRequest();
            }
        }
        //Insert
        [HttpPost]
        public IActionResult Create(DonViQuanLiXeVM donViQuanLiXeVM)
        {
            var donViQuanLiXe = new DonViQuanLiXe
            {
                
            };
            donViQuanLiXes.Add(donViQuanLiXe);
            return Ok(new
            {
                Success = true,
                Data = donViQuanLiXe
            });
        }

        [HttpPut("{id}")]
        public IActionResult Edit(int id, DonViQuanLiXe donViQuanLiXeEdit)
        {
            try
            {
                //LINQ [Object] Query
                var donViQuanLiXe = donViQuanLiXes.SingleOrDefault(dvql => dvql.MaDonVi == id);
                if (donViQuanLiXe == null) { return NotFound(); }
                if (id != donViQuanLiXe.MaDonVi) { return BadRequest(); }
                //Update
                donViQuanLiXe.TenDonVi = donViQuanLiXeEdit.TenDonVi;
                donViQuanLiXe.DiaChi = donViQuanLiXeEdit.DiaChi;
                donViQuanLiXe.SoDienThoai = donViQuanLiXeEdit.SoDienThoai;
                donViQuanLiXe.Email = donViQuanLiXeEdit.Email;
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
                var donViQuanLiXe = donViQuanLiXes.SingleOrDefault(dvql => dvql.MaDonVi == id);
                if (donViQuanLiXe == null) { return NotFound(); }
                donViQuanLiXes.Remove(donViQuanLiXe);
                return Ok();
            }
            catch
            {
                return BadRequest();
            }
        }

    }
}
