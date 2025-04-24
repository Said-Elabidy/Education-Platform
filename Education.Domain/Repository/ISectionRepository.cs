using Education.Domain.Entities;


namespace Education.Domain.Repository
{
    public interface ISectionRepository<TSection> : IGenericRepository<Section>
    {
        Task<IEnumerable<TSection>> getAllByCourseId(int courseId);
        Task<TSection?> getBySectionId(int sectionId);
    }
}