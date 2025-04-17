using Education.Application.DTO_s.SectionDTO_s;


namespace Education.Application.Services.SectionServices
{
    public interface ISectionServices
    {
        Task<IEnumerable<SectionDto>> GetSections();
        Task<IEnumerable<SectionDto>> GetSectionsByCourseId(int courseId);

        Task<SectionDto> GetSectionById(int id);

        Task<bool> Update(int sectionId, UpdateSectionDto section);

        Task<bool> Delete(int id);

        Task Add(CreateSectionDto section);

    }
}
