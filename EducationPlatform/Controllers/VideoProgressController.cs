using Education.Application.Services.VideoProgressServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EducationPlatform.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class VideoProgressController : ControllerBase
    {
        private readonly IVideoProgressServices _videoProgressService;
        public VideoProgressController(IVideoProgressServices videoProgressService)
        {
            _videoProgressService = videoProgressService;
        }

        [HttpGet("ByUserId")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetAllByUserId()
        {
            var userId = User.FindFirst("id")?.Value;
            if (string.IsNullOrEmpty(userId))
                return Unauthorized("User ID not found in token.");
            var result = await _videoProgressService.GetAllByUserIdAsync(userId);
            if (result == null)
                return NotFound("No video progress found for the specified user.");
            return Ok(result);
        }
        [HttpGet("ByVideoIdAndUserId/{videoId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetByVideoIdAndUserId(int videoId)
        {
            var userId = User.FindFirst("id")?.Value;
            if (string.IsNullOrEmpty(userId))
                return Unauthorized("User ID not found in token.");
            var result = await _videoProgressService.GetByIdAsync(videoId,userId);
            if (result == null)
                return NotFound("No video progress found for the specified video.");
            return Ok(result);
        }
        [HttpGet("ByVedioId/{videoId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetByVideoId(int videoId)
        {
            
            var result = await _videoProgressService.GetAllByVideoIdAsync(videoId);
            if (result == null)
                return NotFound("No video progress found for the specified video.");
            return Ok(result);
        }





    }
}
