using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace StudentService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CourseController : ControllerBase
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public CourseController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        [HttpGet]
        public async Task<ActionResult<dynamic>> GetCourse()
        {
            using var client = _httpClientFactory.CreateClient("course");
            var response =await client.GetAsync("/api/Courses");
            var data = await response.Content.ReadAsStringAsync();
            return Ok(data);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<dynamic>> GetCourse(int id)
        {
            using var client = _httpClientFactory.CreateClient("courseByGateway");
            var response = await client.GetAsync($"/Gateway/Courses/{id}");
            var data = await response.Content.ReadAsStringAsync();
            return Ok(data);
        }

    }
}
