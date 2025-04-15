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
    public class SectionRepository:GenericRepository<Section>,ISectionRepository
    {
        public SectionRepository(EducationPlatformDBContext context):base(context)
        {
            
        }

        public async Task<IEnumerable<Section>> getAllByCourseId(int courseId)
        {
            return await _dbSet.Where(s => s.CourseId == courseId).ToListAsync();
        }
    }
}
