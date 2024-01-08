using FreeCourse.Web.Models.Baskets;

namespace FreeCourse.Web.Services.Interfaces
{
    public interface IBasketService
    {
        Task<bool> SaveOrUpdate(BasketViewModel basketViewModel);

        Task<BasketViewModel> Get();

        Task<bool> Delete();

        Task AddBasketItem(BasketItemViewModel basketItemViewModel);

        Task<bool> RemoveBasketItem(string courseId);

        Task<bool> ApplyDiscount(string discountCode);

        Task<bool> CancelApplyDiscount();
    }
}
