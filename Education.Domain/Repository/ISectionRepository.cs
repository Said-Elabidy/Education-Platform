using Education.Domain.Entities;

namespace Education.Domain.Repository
{
    public interface ISectionRepository:IGenericRepository<Section>
    {
        Task<IEnumerable<Section>> getAllByCourseId(int courseId);
    }
}
