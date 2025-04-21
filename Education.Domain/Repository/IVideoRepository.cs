using Education.Domain.Entities;

namespace Education.Domain.Repository
{
    public interface IVideoRepository:IGenericRepository<Video>
    {
        Task<IEnumerable<Video?>> GetBySectionIdAsync(int id);

    }
}
