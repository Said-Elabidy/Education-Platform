using Education.Application.DTO_s.VideoProgressDto_s;
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
    public class VideoProgressRepository : GenericRepository<VideoProgress> , IVideoProgressRepository<VideoProgressDtos>
    {
        public VideoProgressRepository(EducationPlatformDBContext context) : base(context)
        {

        }
        public async Task<IEnumerable<VideoProgressDtos>> GetAllByUserIdAsync(string userId)
        {
            return await _context.videoProgresses
                .Include(vp => vp.video)
                .Where(vp => vp.UserId == userId)
                .Select(vp => new VideoProgressDtos
                {
                    VideoId = vp.VideoId,
                    
                    UserId = vp.UserId,
                    IsWatched = vp.IsWatched,
                    
                })
                .ToListAsync();
        }
        public async Task<IEnumerable<VideoProgressDtos>> GetAllByVideoIdAsync(int videoId)
        {
            return await _context.videoProgresses
                .Include(vp => vp.applicationUser)
                .Where(vp => vp.VideoId == videoId)
                .Select(vp => new VideoProgressDtos
                {
                    VideoId = vp.VideoId,
                    
                    UserId = vp.UserId,
                    IsWatched = vp.IsWatched,
                    
                })
                .ToListAsync();
        }
        public async Task<VideoProgressDtos?> GetByIdAsync(int videoId, string userId)
        {
            return await _context.videoProgresses
                .Include(vp => vp.video)
                .Where(vp => vp.VideoId == videoId && vp.UserId == userId)
                .Select(vp => new VideoProgressDtos
                {
                    VideoId = vp.VideoId,
                    
                    UserId = vp.UserId,
                    IsWatched = vp.IsWatched,
                    
                })
                .FirstOrDefaultAsync();
        }
        public async Task<bool> IsVideoWatchedAsync(int videoId, string userId)
        {
            var videoProgress = await _context.videoProgresses
                .FirstOrDefaultAsync(vp => vp.VideoId == videoId && vp.UserId == userId);

            return videoProgress?.IsWatched ?? false;
        }





    }
}
