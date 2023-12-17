using FreeCourse.Shared.Dtos;
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

        public async Task<bool> CreateCourseAsync(CreateCourseInput createCourseInput)
        {
            var response = await _client.PostAsJsonAsync("courses", createCourseInput);
            if (response.IsSuccessStatusCode)
            {
                return true;
            }
            return false;
        }

        public async Task<bool> DeleteCourseAsync(string courseId)
        {
			var response = await _client.DeleteAsync($"courses/{courseId}");
			if (response.IsSuccessStatusCode)
			{
				return true;
			}
			return false;
		}

        public async Task<List<CategoryViewModel>> GetAllCategoriesAsync()
        {
			var response = await _client.GetAsync("categories");
			if (!response.IsSuccessStatusCode)
			{
				return null;
			}
			var responseSuccess = await response.Content.ReadFromJsonAsync<Response<List<CategoryViewModel>>>();
			return responseSuccess.Data;
		}

        public async Task<List<CourseViewModel>> GetAllCourseByUserIdAsync(string userId)
        {
			var response = await _client.GetAsync($"courses/GetAllByUserId/{userId}");
			if (!response.IsSuccessStatusCode)
			{
				return null;
			}
			var responseSuccess = await response.Content.ReadFromJsonAsync<Response<List<CourseViewModel>>>();
			return responseSuccess.Data;
		}

        public async Task<List<CourseViewModel>> GetAllCoursesAsync()
        {
            var response = await _client.GetAsync("courses");
            if (!response.IsSuccessStatusCode)
            {
                return null;
            }
            var responseSuccess =await  response.Content.ReadFromJsonAsync<Response<List<CourseViewModel>>>();
            return responseSuccess.Data;
        }

        public async Task<CourseViewModel> GetByCourseIdAsync(string courseId)
        {
			var response = await _client.GetAsync($"courses/{courseId}");
			if (!response.IsSuccessStatusCode)
			{
				return null;
			}
			var responseSuccess = await response.Content.ReadFromJsonAsync<Response<CourseViewModel>>();
			return responseSuccess.Data;
		}

        public async Task<bool> UpdateCourseAsync(UpdateCourseInput updateCourseInput)
        {
			var response = await _client.PutAsJsonAsync("courses", updateCourseInput);
			if (response.IsSuccessStatusCode)
			{
				return true;
			}
			return false;
		}
    }
}
