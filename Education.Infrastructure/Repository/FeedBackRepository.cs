using Education.Application.DTO_s.FeedBackDTO_s;
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
    public class FeedBackRepository : GenericRepository<FeedBack>, IFeedBackRepo<FeedBackDTO>
    {
        public FeedBackRepository(EducationPlatformDBContext dBContext):base(dBContext)
        {
            
        }
        public async Task<IEnumerable<FeedBackDTO>> FeedBackByVedio(int vedioId)
        {
            return await _dbSet.Where(f => f.VideoId == vedioId).Select(f => new FeedBackDTO 
            { VideoId = f.VideoId, Comment = f.Comment, FeedBackId = f.FeedBackId, Rating = f.Rating, UserId = f.UserId }).ToListAsync();
        }

        public int GetRatingBySection(int sectionId)
        {
            var rating = _dbSet.Include(f => f.video).Where(f => f.video.SectionId == sectionId).
                Select(f => new GetSectionRating { Rating = f.Rating }).ToList();
            int sectionRate=0;
            for (int i = 0; i < rating.Count; i++)
                sectionRate += rating[i].Rating;
            return sectionRate;
        }
    }
}
