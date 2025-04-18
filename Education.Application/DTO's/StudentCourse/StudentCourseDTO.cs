using Education.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Education.Application.DTO_s.StudentCourse
{
    public class StudentCourseDTO
    {
        public string UserId { get; set; }
        public int CoursesId { get; set; }
        public int ProgressValue { get; set; }

        public int Rating { get; set; }  //to know if student complete course
        public DateTime EnrollmentDate { get; set; }
        public bool IsCompleted { get; set; }  //to know if student complete course
        public DateTime? CompletedAt { get; set; }

    }
}
