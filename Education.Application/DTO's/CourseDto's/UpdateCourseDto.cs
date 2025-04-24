using Education.Application.conest;
using Microsoft.AspNetCore.Http;

using System.ComponentModel.DataAnnotations;


namespace Education.Application.DTO_s.CourseDto_s
{
	public class UpdateCourseDto
	{
		[Required(ErrorMessage = Errors.RequiredFiled)]
		public int CourseId { get; set; }	

		[Required(ErrorMessage = Errors.RequiredFiled)]
		[MinLength(2, ErrorMessage = Errors.MinLength)]
		public string Title { get; set; }


		[Required(ErrorMessage = Errors.RequiredFiled)]
		[MaxLength(1000, ErrorMessage = Errors.MaxLength)]
		public string Description { get; set; }


		[Range(1, 100000.00, ErrorMessage = Errors.Rang)]
		public decimal Price { get; set; }


		[Display(Name = "Discount Percentage"), Range(1, 100, ErrorMessage = Errors.RangValue)]
		public int DiscountPercentage { get; set; } = 1;

		[Required(ErrorMessage = Errors.RequiredFiled)]
		public string CourseStatus { get; set; }    //first Will Be Not Completed ,Comming soon Can not be completes now Becouse he still didnt insert video

		[Required(ErrorMessage = Errors.RequiredFiled)]
		public int CategoriesId { get; set; }

		[Required(ErrorMessage = Errors.RequiredFiled)]
		public bool IsSequentialWatch { get; set; }

		[Required(ErrorMessage = Errors.RequiredFiled)]
		public IFormFile CourseImage { get; set; }
	}
}
