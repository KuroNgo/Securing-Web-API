using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QuanLiTuyenXeBusDalat.Models;
using QuanLiTuyenXeBusDalat.Services;

namespace QuanLiTuyenXeBusDalat.Controllers
{
    // Thông tin của bảng này được lấy từ bộ nhớ (In memory)
 
    [Route("api/{v:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1.0")]
    public class TuyenV1Controller : ControllerBase
    {
        private readonly ITuyenRepository _tuyenRepository;

        public TuyenV1Controller(ITuyenRepository tuyen)
        {
            _tuyenRepository = tuyen;
        }

        [HttpGet]
        //Interface dùng để trả về cho các action
        public IActionResult GetAll()
        {
            // Trả về danh sách các hàng hóa
            try
            {
                return Ok(_tuyenRepository.GetTuyens());
            }
            catch 
            {
                return StatusCode(StatusCodes.Status500InternalServerError);  
            }
          
        }
        [HttpGet("{name}")]
        public IActionResult GetByName(string name)
        {
            try
            {
                var data = _tuyenRepository.GetByName(name);
                if(data != null)
                {
                    return Ok(data);
                }
                else
                {
                    return NotFound();
                }
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
        //Insert
        [HttpPost]
        public IActionResult Create(TuyenModel tuyenModel)
        {
            try
            {
                return Ok(_tuyenRepository.Add(tuyenModel));
            }
            catch 
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPut("{name}")]
        public IActionResult Edit(string name, TuyenVM tuyenVM)
        {
            if (name != tuyenVM.TenTuyen)
            {
                return BadRequest();
            }
            try
            {
                _tuyenRepository.Update(tuyenVM);
                return NoContent();
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpDelete("{name}")]
        public IActionResult Delete(string name)
        {
            try
            {
                _tuyenRepository.Remove(name);
                return Ok();
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

    }
}
