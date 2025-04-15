using Education.Application.DTO_s.SectionDTO_s;
using Education.Application.Services.SectionServices;
using Education.Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static System.Collections.Specialized.BitVector32;

namespace EducationPlatform.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SectionController : ControllerBase
    {
        private readonly ISectionServices _sectionServices;

        public SectionController(ISectionServices sectionServices)
        {
            _sectionServices = sectionServices;
        }
        [HttpGet("by-course /{courseId:int}")]
        public async Task<IActionResult> GetSectionsInCourse(int courseId)
        {
            try
            {
                var sections = await _sectionServices.GetSectionsByCourseId(courseId);
                if (sections == null || !sections.Any())
                {
                    return NotFound(new { Message = $"No sections found for CourseId = {courseId}" });
                }

                return Ok(sections);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new
                {
                    Message = "An error occurred while retrieving sections.",
                    Details = ex.Message
                });
            }
        }
        [HttpGet("{Id:int}")]
        public async Task<IActionResult> GetSectionById(int Id)
        {
            SectionDto section = await _sectionServices.GetSectionById(Id);
            if (section == null )
            {
                return NotFound(new { Message = $"No sections found for This Id = {Id}" });
            }

            return Ok(section);

        }
        [HttpDelete("{Id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int Id)
        {
            bool section = await _sectionServices.Delete(Id);
            if (section == true)
                return NoContent();
            return NotFound(new { Massage = $"No Sections found for This Id = {Id}" });
        }
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateSection(CreateSectionDto sectionDto)
        {
            try
            {
                await _sectionServices.Add(sectionDto);
                return CreatedAtAction("GetSectionsInCourse", new { courseId = sectionDto.CourseId }, sectionDto);
            }
            catch
            {
                return BadRequest();
            }
        }
        [HttpPut("{Id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateSection(int Id, UpdateSectionDto updateSection)
        {
           bool IsUpdated= await _sectionServices.Update(Id, updateSection);
            if (IsUpdated)
                return NoContent();
            return NotFound(new { Massage = $"No Sections found for This Id = {Id}" });
        }
    }
}
