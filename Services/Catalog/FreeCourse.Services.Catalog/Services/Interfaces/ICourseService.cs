using FreeCourse.Services.Catalog.Dtos;
using FreeCourse.Shared.Dtos;

namespace FreeCourse.Services.Catalog.Services.Interfaces
{
    internal interface ICourseService
    {

        Task<Response<List<CourseDto>>> GetAllAsync();

        Task<Response<CourseDto>> GetByIdAsync(string id);

        Task<Response<CourseDto>> CreateAsync(CreateCourseDto course);

        Task<Response<List<CourseDto>>> GetAllByUserIdAsync(string userId);

        Task<Response<NoContent>> UpdateAsync(UpdateCourseDto course);

        Task<Response<NoContent>> DeleteAsync(string id);
        
    }
}
