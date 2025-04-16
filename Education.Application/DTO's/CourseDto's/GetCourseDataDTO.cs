using Education.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Education.Application.DTO_s.CourseDto_s
{
    public class GetCourseDataDTO
    {
        public int CoursesId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int DiscountPercentage { get; set; } = 1;
        public CourseStatus CourseStatus { get; set; }
        public string CourseImage { get; set; }
        public bool IsSequentialWatch { get; set; }
        public int CategoriesId { get; set; }
        public bool IsFree { get; set; }

        public int? Rating { get; set; }
    }
}
