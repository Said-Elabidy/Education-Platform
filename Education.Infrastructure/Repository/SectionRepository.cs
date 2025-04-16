using Education.Application.DTO_s.SectionDTO_s;
using Education.Domain.Entities;
using Education.Domain.Repository;
using Education.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Education.Infrastructure.Repository
{
    public class SectionRepository : GenericRepository<Section>, ISectionRepository<SectionDto>
    {
        public SectionRepository(EducationPlatformDBContext context) : base(context)
        {

        }

        //public async Task<IEnumerable<Section>> getAllByCourseId(int courseId)
        //{

        //}

        public async Task<SectionDto?> getBySectionId(int sectionId)
        {
            var s = await _dbSet.Include(s => s.Quiz).Include(s => s.Videos).FirstOrDefaultAsync(s => s.SectionId == sectionId);
            if (s != null)
                return new SectionDto() {  IsPassSection = s.IsPassSection, Quiz = s.Quiz, SectionName = s.SectionName, VideosNum = s.Videos.Count };
            return null;
        }

        public async Task<IEnumerable<SectionDto>> getAllByCourseId(int courseId)
        {
            return await _dbSet.Include(s=>s.Quiz).Include(s=>s.Videos).Where(s => s.CourseId == courseId).Select(s => new SectionDto() {  IsPassSection = s.IsPassSection, Quiz = s.Quiz, SectionName = s.SectionName, VideosNum = s.Videos.Count }).ToListAsync();
        }
    }
}