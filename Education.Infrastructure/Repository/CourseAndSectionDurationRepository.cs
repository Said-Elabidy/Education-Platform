using Education.Domain.Entities;
using Education.Domain.Repository;
using Education.Infrastructure.Database;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Education.Infrastructure.Repository
{
    public class CourseAndSectionDurationRepository : ICourseAndSectionDurationRepository
    {
        private readonly EducationPlatformDBContext _context;

        public CourseAndSectionDurationRepository(EducationPlatformDBContext context)
        {
            _context = context;
        }
        public async Task<string> GetCourseDuration(int courseId)
        {

            var durations = await _context.videos
                .Where(v => v.Section.CourseId == courseId)
                .Select(v => v.VideoDuration)
                .ToListAsync();

            var totalDuration = durations.Aggregate(TimeSpan.Zero, (sum, next) => sum + next);

            return totalDuration.ToString(@"hh\:mm\:ss");

        }

        public async Task<string> GetSectionDuration(int sectionId)
        {
            var durations = await _context.videos
               .Where(v => v.SectionId == sectionId)
               .Select(v => v.VideoDuration)
               .ToListAsync();

            var totalDuration = durations.Aggregate(TimeSpan.Zero, (sum, next) => sum + next);

            return totalDuration.ToString(@"hh\:mm\:ss");

        }
       


    }
}
