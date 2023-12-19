using FreeCourse.Web.Models.PhotoStocks;

namespace FreeCourse.Web.Services.Interfaces
{
    public interface IPhotoStockService
    {

        Task<PhotoViewModel> UploadPhoto(IFormFile photo);
        Task<bool> DeletePhoto(string photoUrl);
    }
}
