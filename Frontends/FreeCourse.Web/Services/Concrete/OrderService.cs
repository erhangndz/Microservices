using FreeCourse.Shared.Dtos;
using FreeCourse.Shared.Services.Abstract;
using FreeCourse.Web.Models.FakePayment;
using FreeCourse.Web.Models.Orders;
using FreeCourse.Web.Services.Interfaces;
using NuGet.Protocol.Core.Types;

namespace FreeCourse.Web.Services.Concrete
{
    public class OrderService : IOrderService
    {

        private readonly IPaymentService _paymentService;
        private readonly HttpClient _httpClient;
        private readonly IBasketService _basketService;
        private readonly ISharedIdentityService _sharedIdentityService;
        private readonly ICatalogService _catalogService;

        public OrderService(IPaymentService paymentService, HttpClient httpClient, IBasketService basketService, ISharedIdentityService sharedIdentityService, ICatalogService catalogService)
        {
            _paymentService = paymentService;
            _httpClient = httpClient;
            _basketService = basketService;
            _sharedIdentityService = sharedIdentityService;
            _catalogService = catalogService;
        }

        public async Task<OrderCreatedViewModel> CreateOrder(CheckoutInfoInput checkoutInfoInput)
        {
            var basket = await _basketService.Get();
            var paymentInfoInput = new PaymentInfoInput()
            {
                CardName = checkoutInfoInput.CardName,
                CardNumber = checkoutInfoInput.CardNumber,
                Expiration = checkoutInfoInput.Expiration,
                CVV = checkoutInfoInput.CVV,
                TotalPrice = basket.TotalPrice
            };

            var responsePayment = await _paymentService.ReceivePayment(paymentInfoInput);

            if (!responsePayment)
            {
                return new OrderCreatedViewModel() { Error = "Ödeme alınamadı", IsSuccessful = false };
            }

            var orderCreateInput = new OrderCreateInput()
            {
                BuyerId = _sharedIdentityService.GetUserId,
                Address = new AddressCreateInput { Province = checkoutInfoInput.Province, District = checkoutInfoInput.District,
                    AddressLine = checkoutInfoInput.AddressLine,
                    Street = checkoutInfoInput.Street,
                    ZipCode = checkoutInfoInput.ZipCode
                }
         

            };

            basket.BasketItems.ForEach(x =>
            {
                
                var orderItem = new OrderItemCreateInput { ProductId = x.CourseId, PictureUrl = "", Price = x.GetCurrentPrice, ProductName = x.CourseName };
                orderCreateInput.OrderItems.Add(orderItem);
            });

            var response = await _httpClient.PostAsJsonAsync("orders", orderCreateInput);

            if (!response.IsSuccessStatusCode)
            {
                return new OrderCreatedViewModel() { Error = "Sipariş oluşturulamadı",IsSuccessful=false };
            }

           var orderCreatedViewModel = await response.Content.ReadFromJsonAsync<Response<OrderCreatedViewModel>>();
            orderCreatedViewModel.Data.IsSuccessful = true;
            await _basketService.Delete();
            return orderCreatedViewModel.Data;
           


           

        }

        public async Task<List<OrderViewModel>> GetOrder()
        {
            var response = await _httpClient.GetFromJsonAsync<Response<List<OrderViewModel>>>("orders");

            return response.Data;
        }

        public async Task<OrderSuspendViewModel> SuspendOrder(CheckoutInfoInput checkoutInfoInput)
        {
            var basket = await _basketService.Get();
            var orderCreateInput = new OrderCreateInput()
            {
                BuyerId = _sharedIdentityService.GetUserId,
                Address = new AddressCreateInput
                {
                    Province = checkoutInfoInput.Province,
                    District = checkoutInfoInput.District,
                    AddressLine = checkoutInfoInput.AddressLine,
                    Street = checkoutInfoInput.Street,
                    ZipCode = checkoutInfoInput.ZipCode
                }


            };

            basket.BasketItems.ForEach(x =>
            {

                var orderItem = new OrderItemCreateInput { ProductId = x.CourseId, PictureUrl = "", Price = x.GetCurrentPrice, ProductName = x.CourseName };
                orderCreateInput.OrderItems.Add(orderItem);
            });


            
            var paymentInfoInput = new PaymentInfoInput()
            {
                CardName = checkoutInfoInput.CardName,
                CardNumber = checkoutInfoInput.CardNumber,
                Expiration = checkoutInfoInput.Expiration,
                CVV = checkoutInfoInput.CVV,
                TotalPrice = basket.TotalPrice,
                Order=orderCreateInput,
            };

            var responsePayment = await _paymentService.ReceivePayment(paymentInfoInput);

            if (!responsePayment)
            {
                return new OrderSuspendViewModel() { Error = "Ödeme alınamadı", IsSuccessful = false };
            }
            await _basketService.Delete();
            return new OrderSuspendViewModel() { IsSuccessful = true };

        }
    }
}
