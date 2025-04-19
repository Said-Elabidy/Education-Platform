using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace Education.Application.DTO_s.VideoDto_s
{
    public class UpdateVideoDto
    {

        [StringLength(100, MinimumLength = 2, ErrorMessage = "Title must be between 2 and 100 characters")]
        public string? Title { get; set; }

        [StringLength(500, ErrorMessage = "Description cannot exceed 500 characters")]
        public string? Description { get; set; }


        public bool? IsFree { get; set; }

        public IFormFile? ThumbnailImage { get; set; }


        public IFormFile? VideoFile { get; set; }
        
    }
}
