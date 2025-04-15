using Education.Application.conest;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Education.Application.DTO_s.CourseDto_s
{
	public class ChangeAccessDto
	{
		[Required(ErrorMessage = Errors.RequiredFiled)]
		public int CourseId { get; set; }
		[Required(ErrorMessage = Errors.RequiredFiled)]
		public bool IsFree { get; set; }	
	}
}
