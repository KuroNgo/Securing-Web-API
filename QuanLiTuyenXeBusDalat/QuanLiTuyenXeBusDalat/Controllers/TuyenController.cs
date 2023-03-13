using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QuanLiTuyenXeBusDalat.Models;

namespace QuanLiTuyenXeBusDalat.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TuyenController : ControllerBase
    {
        public static List<Tuyen> tuyens = new List<Tuyen>();


        [HttpGet]
        //Interface dùng để trả về cho các action
        public IActionResult GetAll()
        {
            // Trả về danh sách các hàng hóa
            return Ok(tuyens);
        }
        [HttpGet("{id}")]
        public IActionResult GetByID(int id)
        {
            try
            {
                //LinQ [Object] Query
                var tuyen = tuyens.SingleOrDefault(t => t.MaTuyen == id);
                if (tuyen == null)
                {
                    return NotFound();
                }
                return Ok(tuyen);
            }
            catch
            {
                return BadRequest();
            }
        }
        //Insert
        [HttpPost]
        public IActionResult Create(TuyenVM tuyenVM)
        {
            var tuyen = new Tuyen
            {
                TenTuyen= tuyenVM.TenTuyen,
                ThoiGianBatDau=tuyenVM.ThoiGianBatDau,
                ThoiGianKetThuc=tuyenVM.ThoiGianKetThuc,
                ThoiGianGianCach=tuyenVM.ThoiGianGianCach,
                LoTrinhLuotDi=tuyenVM.LoTrinhLuotDi,
                LoTrinhLuotVe=tuyenVM.LoTrinhLuotVe,
                LoaiTuyen=tuyenVM.LoaiTuyen
            };
            tuyens.Add(tuyen);
            return Ok(new
            {
                Success = true,
                Data = tuyen
            });
        }

        [HttpPut("{id}")]
        public IActionResult Edit(int id, Tuyen tuyenEdit)
        {
            try
            {
                //LINQ [Object] Query
                var tuyen = tuyens.SingleOrDefault(t => t.MaTuyen == id);
                if (tuyen == null) { return NotFound(); }
                if (id != tuyen.MaTuyen) { return BadRequest(); }
                //Update
                tuyen.TenTuyen = tuyenEdit.TenTuyen;
                tuyen.ThoiGianBatDau = tuyenEdit.ThoiGianBatDau;
                tuyen.ThoiGianKetThuc=tuyenEdit.ThoiGianKetThuc;
                tuyen.ThoiGianGianCach = tuyenEdit.ThoiGianGianCach;
                tuyen.LoTrinhLuotDi = tuyenEdit.LoTrinhLuotDi;
                tuyen.LoTrinhLuotVe=tuyenEdit.LoTrinhLuotVe;
                tuyen.LoaiTuyen = tuyenEdit.LoaiTuyen;
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
                var tuyen = tuyens.SingleOrDefault(t=>t.MaTuyen == id);
                if (tuyen == null) { return NotFound(); }
                tuyens.Remove(tuyen);
                return Ok();
            }
            catch
            {
                return BadRequest();
            }
        }

    }
}
