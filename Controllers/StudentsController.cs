using Microsoft.AspNetCore.Mvc;
using PracticalTask5_RestApi.Models;
using PracticalTask5_RestApi.Services;

namespace PracticalTask5_RestApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StudentsController : ControllerBase
    {
        private readonly StudentRepository _repo;

        public StudentsController(StudentRepository repo)
        {
            _repo = repo;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] string? group, [FromQuery] double? gpa)
        {
            var students = await _repo.GetStudentsAsync(group, gpa);
            return Ok(students);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var student = await _repo.GetByIdAsync(id);
            return student is null ? NotFound() : Ok(student);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Student student)
        {
            await _repo.AddAsync(student);
            return Ok(new { message = "Student added" });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] Student student)
        {
            student.Id = id;
            await _repo.UpdateAsync(student);
            return Ok(new { message = "Student updated" });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _repo.DeleteAsync(id);
            return Ok(new { message = "Student deleted" });
        }
    }
}
