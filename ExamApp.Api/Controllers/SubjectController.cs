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
    public class SubjectController : Controller
    {
        private readonly ISubjectService _subjectService;
        private readonly IMapper _mapper;

        public SubjectController(ISubjectService subjectService, IMapper mapper)
        {
            _subjectService = subjectService;
            _mapper = mapper;
        }

        [HttpGet]
        [Route("[action]")]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _subjectService.GetAllAsync());
        }

        [HttpGet]
        [Route("[action]")]
        public async Task<IActionResult> GetById([FromQuery] Guid id)
        {
            return Ok(await _subjectService.GetByIdAsync(id));
        }

        [HttpGet]
        [Route("[action]")]
        public async Task<IActionResult> GetPagination([FromQuery] int offset, int limit)
        {
            return Ok(await _subjectService.GetPaginationAsync(offset, limit));
        }

        [HttpGet]
        [Route("[action]")]
        public async Task<IActionResult> GetFullSearch([FromQuery] int offset, int limit, string search = "")
        {
            return Ok(await _subjectService.GetFullSearchAsync(offset, limit, search));
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] SubjectPostModel item)
        {
            var mapped = _mapper.Map<Subjects>(item);
            return Ok(await _subjectService.AddAsync(mapped));
        }
    }
}
