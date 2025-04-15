using Education.Application.DTO_s.ImageDto_s;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Education.Application.Implementations.Abstracts
{
	public interface IImageService
	{
		Task<UploadResult> UploadImage(IFormFile image, string folderPath);
		public void DeleteImage(string ImagePath);
	}
}
