using Education.Application.Base;
using Education.Application.DTO_s.CourseDto_s;
using Education.Application.RequestModels;
using Education.Application.ResponseModels.CourseRespondDto;

namespace Education.Application.CourseServices
{
	public interface ICourseService
	{
	   Task<ApiResponse<CourseRespondDto>> CreateCourse(CreateCourseDto course);
	   Task<ApiResponse<CourseRespondDto>> GetCourseById(int CourseId);
       Task<ApiResponse<string>> DeletedCourse(int CourseId);
	   Task<ApiResponse<CourseRespondDto>> UpdateCourse(UpdateCourseDto course);
	   Task<ApiResponse<string>> ChangeCourseAccess(ChangeAccessDto changeAccessDto);
	   Task<PagedResponse<IEnumerable<CourseRespondDto>>> GetAllCources(PaginationFilter filter,string route);
	   Task<PagedResponse<IEnumerable<CourseRespondDto>>> GetFreeCourses(PaginationFilter filter, string route);
	   //update coure complete
	}
}
