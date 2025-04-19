using Education.Application.DTO_s.VideoProgressDto_s;
using Education.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Education.Application.Services.VideoProgressServices
{
    public interface IVideoProgressServices
    {
        Task<VideoProgressDtos?> GetByIdAsync(int videoId, string userId);
        Task<IEnumerable<VideoProgressDtos>> GetAllByUserIdAsync(string userId);
        Task<IEnumerable<VideoProgressDtos>> GetAllByVideoIdAsync(int videoId);
        Task<bool> IsVideoWatchedAsync(int videoId, string userId);
        
    }
}
