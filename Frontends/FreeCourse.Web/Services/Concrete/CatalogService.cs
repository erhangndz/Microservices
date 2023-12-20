using FreeCourse.Shared.Dtos;
using FreeCourse.Web.Helpers;
using FreeCourse.Web.Models;
using FreeCourse.Web.Models.Catalogs;
using FreeCourse.Web.Services.Interfaces;

namespace FreeCourse.Web.Services.Concrete
{
    public class CatalogService : ICatalogService
    {
        private readonly HttpClient _client;
        private readonly IPhotoStockService _photoStockService;
        private readonly PhotoHelper _photoHelper;

        public CatalogService(HttpClient client, IPhotoStockService photoStockService, PhotoHelper photoHelper)
        {
            _client = client;
            _photoStockService = photoStockService;
            _photoHelper = photoHelper;
        }

        public async Task<bool> CreateCourseAsync(CreateCourseInput createCourseInput)
        {

            var resultPhoto = await _photoStockService.UploadPhoto(createCourseInput.PhotoFormFile);
            if(resultPhoto!= null)
            {
                createCourseInput.Picture = resultPhoto.Url;
            }





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
				return new List<CategoryViewModel>();
			}
			var responseSuccess = await response.Content.ReadFromJsonAsync<Response<List<CategoryViewModel>>>();
			return responseSuccess.Data;
		}

        public async Task<List<CourseViewModel>> GetAllCourseByUserIdAsync(string userId)
        {
			var response = await _client.GetFromJsonAsync<Response<List<CourseViewModel>>>($"courses/GetAllByUserId/{userId}");


            response.Data.ForEach(x =>
            {
                x.Picture = _photoHelper.GetPhotoStockUrl(x.Picture);
            });

			return response.Data;
		}

        public async Task<List<CourseViewModel>> GetAllCoursesAsync()
        {
            var response = await _client.GetAsync("courses");
            if (!response.IsSuccessStatusCode)
            {
                return null;
            }
            var responseSuccess =await  response.Content.ReadFromJsonAsync<Response<List<CourseViewModel>>>();

            responseSuccess.Data.ForEach(x =>
            {
                x.Picture = _photoHelper.GetPhotoStockUrl(x.Picture);
            });

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

            var resultPhoto = await _photoStockService.UploadPhoto(updateCourseInput.PhotoFormFile);
            if (resultPhoto != null)
            {
               await _photoStockService.DeletePhoto(updateCourseInput.Picture);
                updateCourseInput.Picture = resultPhoto.Url;
            }



            var response = await _client.PutAsJsonAsync("courses", updateCourseInput);
            return response.IsSuccessStatusCode;
			
			
		}
    }
}
