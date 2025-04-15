using Education.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Education.Domain.Entities
{
	public class Courses : BaseModal
	{
		public int CoursesId { get; set; }	
		public string Title { get; set; }	
		public string Description { get; set; }	
		public decimal Price { get; set; }
		public int DiscountPercentage { get; set; } = 1;
		public CourseStatus CourseStatus { get; set; }
		public string CourseImage {  get; set; }	
		//public int SectionNumber { get; set; }	
		//public int VideoNumber { get; set; } = 0; //teach  will insert it compare it with  video count on video  table beased on number will determind course status
		public bool IsSequentialWatch {  get; set; }	
		public int CategoriesId { get; set; }
		public bool IsFree { get; set; } //if all course free no need to buy ,any one can wathch them

		public int? Rating { get; set; } //will be calculated by all rating for this course
        public Categories Categories { get; set; }
        
        public ICollection<Section>? Sections { get; set; }	
		public ICollection<Payment>? payments { get; set; }
		
		public ICollection<StudentCourses> StudentCourses { get; set; } 

    }
}

//IsSequentialWatch  (true) student can watch any video without order 
//IsSequentialWatch  (False) student shoud video with order


//course Rating will calculate him by all rating/her number didnt save in database 

