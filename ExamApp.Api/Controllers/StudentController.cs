using AutoMapper;
using ExamApp.Core.DTO;
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
        private readonly IMapper _mapper;

        public StudentController(IStudentService studentService, IMapper mapper)
        {
            _studentService = studentService;
            _mapper = mapper;
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
        public async Task<IActionResult> Post([FromBody] StudentPostModel item)
        {
            var mapped = _mapper.Map<Students>(item);
            return Ok(await _studentService.AddAsync(mapped));
        }

        [HttpPut]
        public async Task<IActionResult> Put([FromBody] StudentPutModel item)
        {
            var mapped = _mapper.Map<Students>(item);
            return Ok(await _studentService.UpdateAsync(mapped));
        }

        [HttpDelete]
        public async Task<IActionResult> Delete([FromQuery] Guid id)
        {
            return Ok(await _studentService.DeleteAsync(id));
        }
    }
}
