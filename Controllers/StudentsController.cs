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
            var createdStudent = await _repo.AddAsync(student);
            return CreatedAtAction(nameof(GetById), new { id = createdStudent.Id }, createdStudent);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] Student student)
        {
            if (student.Id != 0 && student.Id != id)
            {
                return BadRequest(new { message = "Id in URL and payload must match." });
            }

            student.Id = id;
            var updated = await _repo.UpdateAsync(student);
            return updated ? NoContent() : NotFound();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _repo.DeleteAsync(id);
            return deleted ? NoContent() : NotFound();
        }
    }
}
