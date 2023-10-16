using Business.Abstract;
using Entities.Concrete;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentCoursesController : ControllerBase
    {
        private IStudentCourseService _studentCourseService;

        public StudentCoursesController(IStudentCourseService studentCourseService)
        {
            _studentCourseService = studentCourseService;
        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            var result = await _studentCourseService.GetAll();
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpGet("GetById")]
        public async Task<IActionResult> GetById(int studentCourseId)
        {
            var result = await _studentCourseService.GetById(studentCourseId);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpPost("Add")]
        public async Task<IActionResult> Add(StudentCourse studentCourse)
        {
            var result = await _studentCourseService.Add(studentCourse);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpPut("Update")]
        public async Task<IActionResult> Update(StudentCourse studentCourse)
        {
            var result = await _studentCourseService.Update(studentCourse);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpDelete("Delete")]
        public async Task<IActionResult> Delete(StudentCourse studentCourse)
        {
            var result = await _studentCourseService.Delete(studentCourse);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpGet("GetStudentCoursesByStudentId")]
        public async Task<IActionResult> GetStudentCoursesByStudentId(int studentId)
        {
            var result = await _studentCourseService.GetStudentCoursesByStudentId(studentId);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
    }
}
