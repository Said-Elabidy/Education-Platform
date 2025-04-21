using Education.Application.DTO_s.StudentCourse;
using Education.Application.Services.StudentCourseServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace EducationPlatform.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class StudentCourseController : ControllerBase
    {
        private readonly IStudetCourseServices _studetCourseServices;

        public StudentCourseController(IStudetCourseServices studetCourseServices)
        {
            _studetCourseServices = studetCourseServices;
        }
        [HttpGet("ByCourse/{courseId:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> StudentCourseByCourseId(int courseId)
        {
            var studentCourse = await _studetCourseServices.GellStudentCoursesByCourseId(courseId);
            if (studentCourse != null)
                return Ok(studentCourse);
            return NotFound($"Connot Find StudentCourse With CourseID = {courseId}");
        }
        [HttpGet("{courseId:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> StudentCourse(int courseId)
        {
            var userId = User.FindFirst("id")?.Value;
            if (userId != null)
            {
                var studentCourse = await _studetCourseServices.GellStudentCourse(userId, courseId);
                if (studentCourse != null)
                    return Ok(studentCourse);
            }
            return NotFound($"This Student isnot have StudentCourse With CourseID = {courseId} ");
        }
        [HttpPut("{courseId:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateStudentCourse(int courseId,[FromForm] UpdateStudentCourseDTO studentCourseDTO)
        {
            var userId = User.FindFirst("id")?.Value;
            if (userId != null)
            {
                var studentCourse =await _studetCourseServices.Update(userId, courseId, studentCourseDTO);
                if (studentCourse)
                    return NoContent();
            }
            return NotFound(new { Massage = $"No StudentCourse found for CourseID = {courseId}" });

            //return BadRequest($"Failed to Update StudentCourse with CourseID = {courseId} ");
        }
    }
}
