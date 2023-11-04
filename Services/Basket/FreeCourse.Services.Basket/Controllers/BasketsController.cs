using FreeCourse.Services.Basket.Dtos;
using FreeCourse.Services.Basket.Services.Abstract;
using FreeCourse.Shared.ControllerBases;
using FreeCourse.Shared.Services.Abstract;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FreeCourse.Services.Basket.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BasketsController : CustomBaseController
    {
        private readonly IBasketService _basketService;
        private readonly ISharedIdentityService _sharedIdentityService;

        public BasketsController(ISharedIdentityService sharedIdentityService, IBasketService basketService)
        {
            _sharedIdentityService = sharedIdentityService;
            _basketService = basketService;
        }

        [HttpGet]
        public async Task<IActionResult> GetBasket()
        {

            return CreateActionResultInstance(await _basketService.GetBasket(_sharedIdentityService.GetUserId));
        }


        [HttpPost]
        public async Task<IActionResult> SaveorUpdateBasket(BasketDto basketDto)
        {
            var response= await _basketService.SaveOrUpdate(basketDto);

            return CreateActionResultInstance(response);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteBasket()
        {
            return CreateActionResultInstance(await _basketService.Delete(_sharedIdentityService.GetUserId));
        }


    }
}
