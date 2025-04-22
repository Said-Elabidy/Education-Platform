using Education.Application.DTO_s.VideoProgressDto_s;
using Education.Domain.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Education.Application.Services.VideoProgressServices
{
    public class VideoProgressServices : IVideoProgressServices
    {
        private readonly IVideoProgressRepository<VideoProgressDtos> _videoProgressRepository;
        public VideoProgressServices(IVideoProgressRepository<VideoProgressDtos> videoProgressRepository)
        {
            _videoProgressRepository = videoProgressRepository;
        }
        public async Task<bool> IsVideoWatchedAsync(int videoId, string userId)
        {
            return await _videoProgressRepository.IsVideoWatchedAsync(videoId, userId);
        }
        public async Task<IEnumerable<VideoProgressDtos>> GetAllByUserIdAsync(string userId)
        {
            return await _videoProgressRepository.GetAllByUserIdAsync(userId);
        }
        public async Task<IEnumerable<VideoProgressDtos>> GetAllByVideoIdAsync(int videoId)
        {
            return await _videoProgressRepository.GetAllByVideoIdAsync(videoId);
        }
        public async Task<VideoProgressDtos?> GetByIdAsync(int videoId, string userId)
        {
            return await _videoProgressRepository.GetByIdAsync(videoId, userId);
        }







    }
}
