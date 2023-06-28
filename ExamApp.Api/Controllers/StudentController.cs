using ExamApp.Core.Models;
using ExamApp.Service.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ExamApp.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : Controller
    {
        private readonly IStudentService _studentService;

        public StudentController(IStudentService studentService)
        {
            _studentService = studentService;
        }

        [HttpGet]
        [Route("[action]")]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _studentService.GetAllAsync());
        }

        [HttpGet]
        [Route("[action]")]
        public async Task<IActionResult> GetById([FromQuery] Guid id)
        {
            return Ok(await _studentService.GetByIdAsync(id));
        }

        [HttpGet]
        [Route("[action]")]
        public async Task<IActionResult> GetPagination([FromQuery] int offset, int limit)
        {
            return Ok(await _studentService.GetPaginationAsync(offset, limit));
        }

        [HttpGet]
        [Route("[action]")]
        public async Task<IActionResult> GetFullSearch([FromQuery] int offset, int limit, string search = "")
        {
            return Ok(await _studentService.GetFullSearchAsync(offset, limit, search));
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Student item)
        {
            return Ok(await _studentService.AddAsync(item));
        }

        [HttpPut]
        public async Task<IActionResult> Put([FromBody] Student item)
        {
            return Ok(await _studentService.UpdateAsync(item));
        }

        [HttpDelete]
        public async Task<IActionResult> Delete([FromQuery] Guid id)
        {
            return Ok(await _studentService.DeleteAsync(id));
        }
    }
}
