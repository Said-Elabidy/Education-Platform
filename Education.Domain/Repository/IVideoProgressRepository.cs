using Education.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Education.Domain.Repository
{
    public interface IVideoProgressRepository <TVideoProgress>: IGenericRepository<VideoProgress>
    {
        Task<TVideoProgress?> GetByIdAsync(int videoId, string userId);
        Task<IEnumerable<TVideoProgress>> GetAllByUserIdAsync(string userId);
        Task<IEnumerable<TVideoProgress>> GetAllByVideoIdAsync(int videoId);
        Task<bool> IsVideoWatchedAsync(int videoId, string userId);


    }
}
