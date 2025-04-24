using Education.Application.DTO_s.FeedBackDTO_s;
using Education.Application.DTO_s.QuizDto_s;
using Education.Application.Services.FeedBackServices;
using Education.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Threading.Tasks;

namespace EducationPlatform.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class FeedBackController : ControllerBase
    {
        private readonly IFeedBackServices _feedBackServices;

        public FeedBackController(IFeedBackServices feedBackServices)
        {
            _feedBackServices = feedBackServices;
        }
        [HttpGet("{Id:int}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetVedioFeedBack(int Id)
        {
            var vedioFeedBacks = await _feedBackServices.GetByVedioId(Id);
            if (vedioFeedBacks == null || !vedioFeedBacks.Any())
                return NotFound(new { Message = $"No FeedBacks found for VedioId = {Id}" });
            return Ok(vedioFeedBacks);
        }
        [HttpGet("sectioRate/{Id:int}")]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult GetSectioRate(int Id)
        {
            try
            {
                var sectioRate = _feedBackServices.GetSectionRate(Id);
                return Ok(sectioRate);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new
                {
                    Message = "An error occurred while retrieving sectionsRate.",
                    Details = ex.Message
                });
            }
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> AddFeedBack(FeedBackDTO feedBack)
        {
          
            try
            {
                feedBack.UserId = User.FindFirst("id")?.Value;
                await _feedBackServices.CreateFeedBack(feedBack);

                return CreatedAtAction(nameof(GetVedioFeedBack), new { Id = feedBack.VideoId });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new
                {
                    Message = "An error occurred while retrieving sectionsRate.",
                    Details = ex.Message
                });
            }

        }
        [HttpDelete("{Id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteFeedBack(int Id)
        {
            bool deleted = await _feedBackServices.Delete(Id);
            if (deleted)
                return NoContent();
            return BadRequest($"Failed to delete FeedBack with ID : {Id}");
        }
        [HttpPut("{Id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateFeedBack([FromRoute]int Id, [FromBody]UpdateFeedBackDTO feedBackDTO)
        {
            bool Updated = await _feedBackServices.Update(Id, feedBackDTO);
            if (Updated)
            {
                return NoContent();
            }
            return NotFound(new { Massage = $"No FeedBack found for Id = {Id}" });
             //BadRequest($"Failed to Update FeedBack with ID : {Id}");
        }
    }
}
