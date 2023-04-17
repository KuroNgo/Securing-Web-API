//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Mvc;
//using QuanLiTuyenXeBusDalat.Data;
//using QuanLiTuyenXeBusDalat.Models;
//using QuanLiTuyenXeBusDalat.Services;

//namespace QuanLiTuyenXeBusDalat.Controllers
//{
//    // THông tin của bảng này được lấy từ bộ nhớ ( In memory )
  
//    [Route("api/{v:apiVersion}/[controller]")]
//    [ApiController]
//    [ApiVersion("1.0")]
//    public class DonViQuanLiXeV1Controller : ControllerBase
//    {
//        private readonly IDonViQuanLiXe _donViQuanLyXeRepository;

//        public DonViQuanLiXeV1Controller(IDonViQuanLiXe donViQuanLiXe)
//        {
//            _donViQuanLyXeRepository= donViQuanLiXe;
//        }
//        [HttpGet]
//        //Interface dùng để trả về cho các action
//        public IActionResult GetAll()
//        {
//            // Trả về danh sách các hàng hóa
//            try
//            {
//                return Ok(_donViQuanLyXeRepository.GetDonViQuanLyXes());
//            }
//            catch
//            {
//                return StatusCode(StatusCodes.Status500InternalServerError);
//            }
//        }
//        [HttpGet("{name}")]
//        public IActionResult GetByID(string name)
//        {
//            try
//            {
//                var data = _donViQuanLyXeRepository.GetByName(name);
//                if (data != null)
//                {
//                    return Ok(data);
//                }
//                else
//                {
//                    return NotFound();
//                }
//            }
//            catch
//            {
//                return StatusCode(StatusCodes.Status500InternalServerError);
//            }
//        }
//        //Insert
//        [HttpPost]
//        public IActionResult Create(DonViQuanLiXeModel donViQuanLiXeModel)
//        {
//            try
//            {
//                return Ok(_donViQuanLyXeRepository.Add(donViQuanLiXeModel));
//            }
//            catch
//            {
//                return StatusCode(StatusCodes.Status500InternalServerError);
//            }
//        }

//        [HttpPut("{name}")]
//        public IActionResult Edit(string name, DonViQuanLiXeVM donViQuanLiXe)
//        {
//            if (name != donViQuanLiXe.TenDonVi)
//            {
//                return BadRequest();
//            }
//            try
//            {
//                _donViQuanLyXeRepository.Update(donViQuanLiXe);
//                return NoContent();
//            }
//            catch
//            {
//                return BadRequest();
//            }
//        }

//        //[HttpDelete("{name}")]
//        //public IActionResult Delete(string name)
//        //{
//        //    try
//        //    {
//        //        _donViQuanLyXeRepository.Remove(name);
//        //        return Ok();
//        //    }
//        //    catch
//        //    {
//        //        return BadRequest();
//        //    }
//        //}

//    }
//}
