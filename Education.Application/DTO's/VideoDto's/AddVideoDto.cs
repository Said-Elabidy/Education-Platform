using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

public class AddVideoDto
{
    [Required(ErrorMessage = "Title is required")]
    [StringLength(100, MinimumLength = 2, ErrorMessage = "Title must be between 2 and 100 characters")]
    public string Title { get; set; }

    [StringLength(500, ErrorMessage = "Description cannot exceed 500 characters")]
    public string? Description { get; set; }


    [Required(ErrorMessage = "Must specify if video is free or not")]
    public bool IsFree { get; set; }

    public IFormFile? ThumbnailImage { get; set; }

    [Required(ErrorMessage = "Video file is required")]
    public IFormFile VideoFile { get; set; }

    [Required(ErrorMessage = "Section ID is required")]
    [Range(1, int.MaxValue, ErrorMessage = "Section ID must be a positive number")]
    public int SectionId { get; set; }
}