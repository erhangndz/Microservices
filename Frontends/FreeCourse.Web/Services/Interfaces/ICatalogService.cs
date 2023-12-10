using FreeCourse.Web.Models.Catalogs;

namespace FreeCourse.Web.Services.Interfaces
{
    public interface ICatalogService
    {
        Task<List<CourseViewModel>> GetAllCoursesAsync();

        Task<List<CategoryViewModel>> GetAllCategoriesAsync();

        Task<List<CourseViewModel>> GetAllCourseByUserIdAsync(string  userId);   

        Task<bool> DeleteCourseAsync(string courseId);

        Task<CourseViewModel> GetByCourseIdAsync(string courseId);

        Task<bool> CreateCourseAsync(CreateCourseInput createCourseInput);

        Task<bool> UpdateCourseAsync(UpdateCourseInput updateCourseInput);
    }
}
