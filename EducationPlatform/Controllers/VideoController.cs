using Education.Application.Services.Storage_Services;
using Education.Application.Services.VideoServices;
using Education.Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EducationPlatform.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VideoController : ControllerBase
    {
        private readonly IStorageService _storageService;
        private readonly IVideoService _videoService;

        public VideoController(IStorageService storageService , IVideoService videoService)
        {
            _storageService = storageService;
            _videoService = videoService;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Consumes("multipart/form-data")]
        public async Task<ActionResult<Video>> AddVideo([FromForm] AddVideoDto addVideoDto)
        {
            try
            {
                // Validate video file
                if (addVideoDto.VideoFile == null || addVideoDto.VideoFile.Length == 0)
                {
                    return BadRequest("Video file is required");
                }

                var video = await _videoService.CreateAsync(addVideoDto);
                return Ok(video);
            }
            catch (Exception ex)
            {
                return BadRequest($"Internal server error: {ex.Message}");
            }
        }


        // Get Video By Id
        [HttpGet("{Id}")]
        public async Task<ActionResult<Video>> GetVideoById(int Id)
        {
            var video = await _videoService.GetByIdAsync(Id);
            if (video == null)
            {
                return NotFound($"Video with ID : {Id} not found");
            }
            return Ok(video);

        }


    }
}
