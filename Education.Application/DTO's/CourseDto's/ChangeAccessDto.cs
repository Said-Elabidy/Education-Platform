using Education.Application.conest;

using System.ComponentModel.DataAnnotations;


namespace Education.Application.DTO_s.CourseDto_s
{
	public class ChangeAccessDto
	{
		//[Required(ErrorMessage = Errors.RequiredFiled)]
		//public int CourseId { get; set; }
		[Required(ErrorMessage = Errors.RequiredFiled)]
		public bool IsFree { get; set; }	
	}
}
