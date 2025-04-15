using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Education.Application.ResponseModels.CourseRespondDto
{
	public class CourseRespondDto
	{
		public int CourseId { get; set; }
		public string Title { get; set; }
		public string Description { get; set; }
		public decimal Price { get; set; }
		public int DiscountPercentage { get; set; }
		public string CourseStatus { get; set; }
		public int CategoriesId { get; set; }
		public bool IsSequentialWatch { get; set; }
		public int? Rating {  get; set; }	
		public string CourseImage { get; set; }
		public bool IsDeleted { get; set; }
		public bool IsFree { get; set; }
		public DateTime CreateOn { get; set; }
		public DateTime? LastUpdateOn { get; set; }

	}
}
