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
    public class VideoRepository: GenericRepository<Video> , IVideoRepository
    {
        public VideoRepository(EducationPlatformDBContext context) : base(context)
        {

        }
        public async Task<IEnumerable<Video?>> GetBySectionIdAsync(int id)
        {
            return await _context.videos
                .Include(v => v.Section)
                
                .Where(v => v.SectionId == id)
                .ToListAsync();
        }
    }
}
