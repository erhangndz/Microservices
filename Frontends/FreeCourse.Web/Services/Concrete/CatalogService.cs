using FreeCourse.Web.Models;
using FreeCourse.Web.Models.Catalogs;
using FreeCourse.Web.Services.Interfaces;

namespace FreeCourse.Web.Services.Concrete
{
    public class CatalogService : ICatalogService
    {
        private readonly HttpClient _client;

        public CatalogService(HttpClient client)
        {
            _client = client;
        }

        public Task<bool> CreateCourseAsync(CreateCourseInput createCourseInput)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteCourseAsync(string courseId)
        {
            throw new NotImplementedException();
        }

        public Task<List<CategoryViewModel>> GetAllCategoriesAsync()
        {
            throw new NotImplementedException();
        }

        public Task<List<CourseViewModel>> GetAllCourseByUserIdAsync(string userId)
        {
            throw new NotImplementedException();
        }

        public Task<List<CourseViewModel>> GetAllCoursesAsync()
        {
            throw new NotImplementedException();
        }

        public Task<CourseViewModel> GetByCourseIdAsync(string courseId)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateCourseAsync(UpdateCourseInput updateCourseInput)
        {
            throw new NotImplementedException();
        }
    }
}
