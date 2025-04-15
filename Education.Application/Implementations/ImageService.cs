using Education.Application.conest;
using Education.Application.DTO_s.ImageDto_s;
using Education.Application.Implementations.Abstracts;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Education.Application.Implementations
{
	public class ImageService : IImageService
	{
		private readonly IWebHostEnvironment webHostEnvironment;
		private readonly List<String> AllowExtentions = new List<string> { ".jpg", ".jpeg", ".png" };
		private const int maxLenght = 2097152;
		public ImageService(IWebHostEnvironment webHostEnvironment)
		{
			this.webHostEnvironment = webHostEnvironment;
		}
		public async Task<UploadResult> UploadImage(IFormFile image, string folderPath)
		  {
			UploadResult result=new UploadResult();
			var FolderPath = Path.Combine(webHostEnvironment.WebRootPath, folderPath.TrimStart('/'));
			if (!Directory.Exists(FolderPath))
			{
				Directory.CreateDirectory(FolderPath);
			}
			if (image.Length > maxLenght) 
			{
				result.ErrorMessage = Errors.NotAllowedExtension;
				return result;
			}
			var extenstion = Path.GetExtension(image.FileName);
			if (!AllowExtentions.Contains(extenstion))
			{
				result.ErrorMessage = Errors.MaxSize;
				return result;
			}

			var imageName= $"{Guid.NewGuid().ToString()}{extenstion}";
			var ImageFolderPath = Path.Combine($"{webHostEnvironment.WebRootPath}{folderPath}", imageName);
			using(var fileStream=new FileStream(ImageFolderPath, FileMode.Create, FileAccess.Write))
			{
				await image.CopyToAsync(fileStream);
			}
			result.ImageName = imageName;	
			result.IsUploaded = true;
			return result;
		}
		public void DeleteImage(string ImagePath)
		{
			var oldImagePath = $"{webHostEnvironment.WebRootPath}{ImagePath}";

			if (File.Exists(oldImagePath))
				File.Delete(oldImagePath);
		}
	}
}
