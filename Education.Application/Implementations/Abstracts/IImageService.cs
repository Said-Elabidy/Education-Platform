using Education.Application.DTO_s.ImageDto_s;
using Microsoft.AspNetCore.Http;


namespace Education.Application.Implementations.Abstracts
{
	public interface IImageService
	{
		Task<UploadResult> UploadImage(IFormFile image, string folderPath);
		public void DeleteImage(string ImagePath);
	}
}
