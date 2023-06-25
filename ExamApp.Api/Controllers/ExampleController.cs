using ExamApp.Service.Services;
using Microsoft.AspNetCore.Mvc;

namespace ExamApp.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExampleController : Controller
    {
        private readonly IExampleService _exampleService;

        public ExampleController(IExampleService exampleService)
        {
            _exampleService = exampleService;
        }

        [HttpGet]
        [Route("[action]")]

        public IActionResult GetAll()
        {
            return Ok(_exampleService.GetAll());
        }

        [HttpPost]
        public IActionResult Post(int number)
        {
            return Ok(_exampleService.Add(number));
        }
    }
}
