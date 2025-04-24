using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Education.Domain.Repository
{
    public interface ICourseAndSectionDurationRepository
    {
        Task<string> GetCourseDuration(int courseId);

        Task<string> GetSectionDuration(int sectionId);
    }
}
