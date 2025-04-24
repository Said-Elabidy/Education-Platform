using Education.Application.Base;
using Education.Application.DTO_s.CourseDto_s;
using Education.Application.Implementations.Abstracts;
using Education.Application.RequestModels;
using Education.Application.ResponseModels;
using Education.Application.ResponseModels.CourseRespondDto;
using Education.Domain.Entities;
using Education.Domain.Enum;
using Education.Domain.Repository;
using Microsoft.AspNetCore.Hosting;

namespace Education.Application.Services.CourseServices
{
	public class CourseService : ICourseService
	{
		private readonly ICourseRepository<GetCourseDataDTO> courseRepository;
		private readonly IImageService imageService;
		private readonly string CourseFolderName = @"/Courses";
		private readonly IWebHostEnvironment webHostEnvironment;

		private readonly IUriService uriService;
        private readonly ICourseAndSectionDurationRepository _courseDurationRepository;

        public CourseService(ICourseRepository  courseRepository, IImageService imageService, IWebHostEnvironment webHostEnvironment
			                , IUriService uriService , ICourseAndSectionDurationRepository courseDurationRepository)

		{ 
			this.courseRepository = courseRepository;
			this.imageService = imageService;
			this.webHostEnvironment = webHostEnvironment;

			this.uriService = uriService;
            _courseDurationRepository = courseDurationRepository;
        }


		public async Task<ApiResponse<CourseRespondDto>> CreateCourse(CreateCourseDto coursesDto)
		{
			if (coursesDto == null)
				return new ApiResponse<CourseRespondDto>(400, "Course data is missing");
			Courses course = new Courses();
			try
			{
				//chech if categoryId is on category tables;

			//	var category = await courseRepository.GetEntityAsync(e => e.CategoriesId == coursesDto.CategoriesId&&!e.IsDeleted);
				//if (category is null)
				//	new ApiResponse<CourseRespondDto>(404, $"Category Id :{coursesDto.CategoriesId}  Is Not Found");

				var imageResult = await imageService.UploadImage(coursesDto.CourseImage, CourseFolderName);
				if (!imageResult.IsUploaded)
					return new ApiResponse<CourseRespondDto>(400, imageResult.ErrorMessage);

				course.CourseImage = imageResult.ImageName;
				MapDTOToCourse(course, null, coursesDto);
				course.CreateOn = DateTime.Now;
				course.IsFree = false;
				course.Rating = null;

				if (!Enum.TryParse<CourseStatus>(coursesDto.CourseStatus, true, out var courseStatus))
					return new ApiResponse<CourseRespondDto>(400, "Invalid Course Status Value");

				course.CourseStatus = courseStatus;

				await courseRepository.AddAsync(course);
				await courseRepository.SaveChangesAsync();

				var CourseRsepond = MapCourseToDTO(course);
				return new ApiResponse<CourseRespondDto>(200, CourseRsepond, "Created Course Data");
			}
			catch
			{
				imageService.DeleteImage($"{CourseFolderName}/{course.CourseImage}");
				return new ApiResponse<CourseRespondDto>(500, "An unexpected error occurred while processing the Course .");
			}
		}
		public async Task<ApiResponse<CourseRespondDto>> GetCourseById(int CourseId)
		{
			//caculate rating
			try
			{
				string[] Includes = { "Categories" };
				var course = await courseRepository.GetEntityAsync(e => e.CoursesId == CourseId && !e.IsDeleted, Includes, true);
				if (course is null)
					return new ApiResponse<CourseRespondDto>(404, $"Course With Id :{CourseId}  Is Not Found");


				var CourseRsepond = MapCourseToDTO(course);
				return new ApiResponse<CourseRespondDto>(200, CourseRsepond, "Course Data");
			}
			catch
			{
				return new ApiResponse<CourseRespondDto>(500, "An unexpected error occurred while processing the Get Course By Id");
			}
		}
		public async Task<ApiResponse<string>> DeletedCourse(int CourseId)
		{
			try
			{
				var course = await courseRepository.GetEntityAsync(e => e.CoursesId == CourseId, null, true);
				if (course is null)
					return new ApiResponse<string>(404, $"Course With Id :{CourseId}  Is Not Found");

				if (course.IsDeleted)
					return new ApiResponse<string>(404, $"Course Is Already Deleted");


				course.IsDeleted = true;
				course.LastUpdateOn = DateTime.Now;

				courseRepository.Update(course);
				await courseRepository.SaveChangesAsync();

				return new ApiResponse<string>(200, $"Course With Id :{CourseId} Is Deleted Successfully .");
			}
			catch
			{
				return new ApiResponse<string>(500, "An unexpected error occurred while processing the Delete Course");
			}
		}
		public async Task<ApiResponse<CourseRespondDto>> UpdateCourse(UpdateCourseDto coursesDto)
		{
			bool IsNewPhotoUpload = false;
			if (coursesDto is null)
				return new ApiResponse<CourseRespondDto>(400, "Course data is missing");
			try
			{
				string[] Includes = { "Categories" };
				//var category = await courseRepository.GetEntityAsync(e => e.CategoriesId == coursesDto.CategoriesId&&!e.IsDeleted);
				//if (category is null)
					//new ApiResponse<CourseRespondDto>(404, $"Category Id :{coursesDto.CategoriesId}  Is Not Found");

				var course = await courseRepository.GetEntityAsync(e => e.CoursesId == coursesDto.CourseId && !e.IsDeleted, Includes, true);
				if (course is null)
					return new ApiResponse<CourseRespondDto>(404, $"Course With Id :{coursesDto.CourseId}  Is Not Found");

				imageService.DeleteImage($"{CourseFolderName}/{course.CourseImage}");

				var imageResult = await imageService.UploadImage(coursesDto.CourseImage, CourseFolderName);
				if (!imageResult.IsUploaded)
					return new ApiResponse<CourseRespondDto>(400, imageResult.ErrorMessage);

				course.CourseImage = imageResult.ImageName;
				IsNewPhotoUpload = true;
				if (!Enum.TryParse<CourseStatus>(coursesDto.CourseStatus, true, out var courseStatus))
					return new ApiResponse<CourseRespondDto>(400, "Invalid Course Status Value");

				course.CourseStatus = courseStatus;
				course.LastUpdateOn = DateTime.Now;
				MapDTOToCourse(course, coursesDto, null);

				courseRepository.Update(course);
				await courseRepository.SaveChangesAsync();

				var CourseRsepond = MapCourseToDTO(course);
				return new ApiResponse<CourseRespondDto>(200, CourseRsepond, $"Update Course Data fo{course.Title} Successfully ");
			}
			catch
			{
				if (IsNewPhotoUpload)
				{
					var course = await courseRepository.GetByIdAsync(coursesDto.CourseId);
					imageService.DeleteImage($"{CourseFolderName}/{course.CourseImage}");
				}
				return new ApiResponse<CourseRespondDto>(500, "An unexpected error occurred while processing the Course .");
			}
		}
		private CourseRespondDto MapCourseToDTO(Courses courses)
		{
			var CourseResponse = new CourseRespondDto()
			{
				CourseId = courses.CoursesId,
				IsSequentialWatch = courses.IsSequentialWatch,
				CategoriesId = courses.CategoriesId,
				Price = courses.Price,
				Title = courses.Title,
				Description = courses.Description,
				CourseImage = $"{webHostEnvironment.WebRootPath}{CourseFolderName}{courses.CourseImage}",
				CreateOn = courses.CreateOn,
				DiscountPercentage = courses.DiscountPercentage,
				LastUpdateOn = courses.LastUpdateOn,
				IsDeleted = courses.IsDeleted,
				CourseStatus = courses.CourseStatus.ToString(),
				IsFree = courses.IsFree,
				Rating = courses.Rating,
			};
			return CourseResponse;
		}

		private void MapDTOToCourse(Courses course, UpdateCourseDto coursesUpdateDto, CreateCourseDto coursesCreateDto)
		{
			dynamic dto = coursesUpdateDto is null ? coursesCreateDto : coursesUpdateDto;

			course.Title = dto.Title;
			course.CategoriesId = dto.CategoriesId;
			course.Description = dto.Description;
			course.Price = dto.Price;
			course.DiscountPercentage = dto.DiscountPercentage;
			course.IsSequentialWatch = dto.IsSequentialWatch;
		}

		public async Task<ApiResponse<string>> ChangeCourseAccess(ChangeAccessDto changeAccessDto)
		{
			if (changeAccessDto is null)
				return new ApiResponse<string>(400, "Course data is missing");
			try
			{
				var course = await courseRepository.GetByIdAsync(changeAccessDto.CourseId);
				if (course is null)
					return new ApiResponse<string>(404, $"Course With Id :{changeAccessDto.CourseId}  Is Not Found");
				if (course.IsDeleted)
					return new ApiResponse<string>(404, $"Course With Id :{changeAccessDto.CourseId}  Is Deleted");

				string Oldstatus = course.IsFree ? "Free" : "Paid";


				course.LastUpdateOn = DateTime.Now;
				course.IsFree = changeAccessDto.IsFree;

				courseRepository.Update(course);
				await courseRepository.SaveChangesAsync();

				string Newstatus = course.IsFree ? "Free" : "Paid";

				return new ApiResponse<string>(200, $"{course.Title} Course Access Status Is Changed From {Oldstatus} to {Newstatus} Successfuly. ");

			}
			catch
			{
				return new ApiResponse<string>(500, "An unexpected error occurred while processing the Course Change Access Request.");
			}
		}

		public async Task<PagedResponse<IEnumerable<CourseRespondDto>>> GetAllCources(PaginationFilter filter,string route)
		{
			try
			{
				string[] Includes = { "Categories" };
				var courses = await courseRepository.GetAllEntitiesAsync(null, Includes, true,filter.pageNumber,filter.PageSize);
				if (courses is null)
					return new PagedResponse<IEnumerable<CourseRespondDto>>(404, "Still No Cources Craeted On Data Base");

				var totalRecords = await courseRepository.RecordCount();

				List<CourseRespondDto> coursesRespondsDto= new List<CourseRespondDto>();
				foreach(var course in courses)
				{
					var courseDto = MapCourseToDTO(course);
			        coursesRespondsDto.Add(courseDto);	
				}
				return coursesRespondsDto.CreatePagedReponse(filter, totalRecords, uriService, route);
			}
			catch
			{
				return new PagedResponse<IEnumerable<CourseRespondDto>>(500, "An unexpected error occurred while processing the Get All Course ");
			}
		}

		public async Task<PagedResponse<IEnumerable<CourseRespondDto>>> GetFreeCourses(PaginationFilter filter, string route)
		{
			try
			{
				string[] Includes = { "Categories" };
				var courses = await courseRepository.GetAllEntitiesAsync(e => !e.IsFree, Includes, true, filter.pageNumber, filter.PageSize);
				if (courses is null)
					return new PagedResponse<IEnumerable<CourseRespondDto>>(404, "Still No Cources Craeted On Data Base");

				var totalRecords = await courseRepository.RecordCount();

				List<CourseRespondDto> coursesRespondsDto = new List<CourseRespondDto>();
				foreach (var course in courses)
				{
					var courseDto = MapCourseToDTO(course);
					coursesRespondsDto.Add(courseDto);
				}
				return coursesRespondsDto.CreatePagedReponse(filter, totalRecords, uriService, route);
			}
			catch
			{
				return new PagedResponse<IEnumerable<CourseRespondDto>>(500, "An unexpected error occurred while processing the Get All Course ");
			}
		}


		public async Task<string> CourseDuration (int courseId)
		{
            var duration = await _courseDurationRepository.GetCourseDuration(courseId);
            if (duration is null)
                return "00:00:00";
            return duration;
     }

    } 

}
