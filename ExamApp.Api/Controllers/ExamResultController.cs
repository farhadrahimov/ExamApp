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
    public class ExamResultController : Controller
    {
        private readonly IExamResultService _examResultService;
        private readonly IMapper _mapper;

        public ExamResultController(IExamResultService examResultService, IMapper mapper)
        {
            _examResultService = examResultService;
            _mapper = mapper;
        }

        [HttpGet]
        [Route("[action]")]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _examResultService.GetAllAsync());
        }

        [HttpGet]
        [Route("[action]")]
        public async Task<IActionResult> GetById([FromQuery] Guid id)
        {
            return Ok(await _examResultService.GetByIdAsync(id));
        }

        [HttpGet]
        [Route("[action]")]
        public async Task<IActionResult> GetPagination([FromQuery] int offset, int limit)
        {
            return Ok(await _examResultService.GetPaginationAsync(offset, limit));
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> GetFullSearch([FromForm] ExamResultsRequestModel requestModel)
        {
            return Ok(await _examResultService.GetFullSearchAsync(requestModel));
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] ExamResultPostModel item)
        {
            var mapped = _mapper.Map<ExamResult>(item);
            return Ok(await _examResultService.AddAsync(mapped));
        }
    }
}
