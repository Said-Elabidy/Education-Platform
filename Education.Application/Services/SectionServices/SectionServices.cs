using Education.Application.DTO_s.SectionDTO_s;
using Education.Domain.Entities;
using Education.Domain.Repository;


namespace Education.Application.Services.SectionServices
{
    public class SectionServices : ISectionServices
    {
        private readonly ISectionRepository _sectionRepo;

        public SectionServices(ISectionRepository sectionRepo)
        {
            _sectionRepo = sectionRepo;
        }
        public async Task Add(CreateSectionDto sectionDto)
        {
            var section = new Section()
            {
                SectionName = sectionDto.SectionName,
                Videos = sectionDto.Videos,
                CourseId = sectionDto.CourseId,
                Quiz = sectionDto.Quiz,
                IsPassSection=sectionDto.IsPassSection
            };
            await _sectionRepo.AddAsync(section);
            await _sectionRepo.SaveChangesAsync();


        }

        public async Task<bool> Delete(int id)
        {
            var section = await _sectionRepo.GetByIdAsync(id);
            if (section != null)
            {
                await _sectionRepo.Delete(id);
                await _sectionRepo.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<SectionDto> GetSectionById(int id)
        {
            var section = await _sectionRepo.GetByIdAsync(id);
            if (section != null)
            {
                var sectionDto = new SectionDto()
                {
                    SectionId = section.SectionId,
                    SectionName = section.SectionName,
                    IsPassSection = section.IsPassSection,
                    //CourseId = section.CourseId,
                    //Courses = section.Courses,
                    //Videos = section.Videos,
                    Quiz = section.Quiz
                };

                return sectionDto;
            }
            return new SectionDto();
        }

        public async Task<IEnumerable<SectionDto>> GetSections()
        {
            var sections =await _sectionRepo.GetAllAsync();
            var sectionDto = sections.Select(s => new SectionDto()
            {
                SectionId = s.SectionId,
                SectionName = s.SectionName,
                //CourseId = s.CourseId,
                //Courses = s.Courses,
                IsPassSection = s.IsPassSection,
                Quiz = s.Quiz,
                //Videos = s.Videos
            }).ToList();
            return sectionDto;
        }

        public async Task<IEnumerable<SectionDto>> GetSectionsByCourseId(int courseId)
        {
            var sections = await _sectionRepo.getAllByCourseId(courseId);
            var sectionDto = sections.Select(s => new SectionDto()
            {
                SectionId = s.SectionId,
                SectionName = s.SectionName,
                //CourseId = s.CourseId,
                //Courses = s.Courses,
                IsPassSection = s.IsPassSection,
                Quiz = s.Quiz,
                //Videos = s.Videos
            }).ToList();
            return sectionDto;
        }

        public async Task<bool> Update(int sectionId, UpdateSectionDto sectionDto)
        {
            var section = await _sectionRepo.GetByIdAsync(sectionId);
            if (section != null) { 
                section.Quiz = sectionDto.Quiz;
                section.SectionName = sectionDto.SectionName;
                section.IsPassSection = sectionDto.IsPassSection;
                await _sectionRepo.SaveChangesAsync();
                return true;
            }
            return false;
        }
    }
}
