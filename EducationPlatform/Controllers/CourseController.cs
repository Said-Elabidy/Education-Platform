using Education.Application.CourseServices;
using Education.Application.DTO_s;
using Education.Application.DTO_s.CourseDto_s;
using Education.Application.RequestModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EducationPlatform.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class CourseController : ControllerBase
	{
		private readonly ICourseService courseService;
		public CourseController(ICourseService courseService)
		{
			this.courseService = courseService;
		}
		[HttpPost("Add-Course")]
		public async Task<ActionResult> AddCourseAsync([FromForm] CreateCourseDto courseDto)
		{
			if (!ModelState.IsValid)
				return BadRequest(ModelState);

			var respond = await courseService.CreateCourse(courseDto);
			if (respond.StatusCode != 200)
				return StatusCode(respond.StatusCode, respond);

			return Ok(respond);
		}
		[HttpGet("{Id:int}")]
		public async Task<ActionResult> GetCourseByIdAsync(int Id)
		{
			if (!ModelState.IsValid)
				return BadRequest(ModelState);

			var respond = await courseService.GetCourseById(Id);
			if (respond.StatusCode != 200)
				return StatusCode(respond.StatusCode, respond);

			return Ok(respond);
		}
		[HttpDelete("{Id:int}")]
		public async Task<ActionResult> DeleteAsync(int Id)
		{
			if (!ModelState.IsValid)
				return BadRequest(ModelState);

			var respond = await courseService.DeletedCourse(Id);
			if (respond.StatusCode != 200)
				return StatusCode(respond.StatusCode, respond);

			return Ok(respond);
		}

		[HttpGet("get-All-Course")]
		public async Task<ActionResult> GetAllCourcesAsync([FromQuery] PaginationFilter filter)
		{
			var route = Request.Path.Value;

			var response = await courseService.GetAllCources(filter,route);
			if (response.StatusCode != 200)
			{
				return StatusCode(response.StatusCode, response);
			}
			return Ok(response);
		}

		[HttpPut("updateCourse")]
		public async Task<ActionResult> UpdateCourse([FromForm] UpdateCourseDto updateCourseDto)
		{
			if (!ModelState.IsValid)
				return BadRequest(ModelState);

			var respond = await courseService.UpdateCourse(updateCourseDto);		
			if (respond.StatusCode != 200)
				return StatusCode(respond.StatusCode, respond);

			return Ok(respond);
		}

		[HttpPut("updateCourseAccess")]
		public async Task<ActionResult> UpdateCourse([FromBody] ChangeAccessDto changeAccessDto)
		{
			if (!ModelState.IsValid)
				return BadRequest(ModelState);

			var respond = await courseService.ChangeCourseAccess(changeAccessDto);
			if (respond.StatusCode != 200)
				return StatusCode(respond.StatusCode, respond);

			return Ok(respond);
		}

		[HttpGet("get-All-FreeCourse")]
		public async Task<ActionResult> GetAllFreeCourcesAsync([FromQuery] PaginationFilter filter)
		{
			var route = Request.Path.Value;

			var response = await courseService.GetFreeCourses(filter, route);
			if (response.StatusCode != 200)
			{
				return StatusCode(response.StatusCode, response);
			}
			return Ok(response);
		}
	}
}