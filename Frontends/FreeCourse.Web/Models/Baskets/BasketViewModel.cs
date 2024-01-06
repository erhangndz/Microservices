namespace FreeCourse.Web.Models.Baskets
{
    public class BasketViewModel
    {
        public BasketViewModel()
        {
            _basketItems = new List<BasketItemViewModel>();
        }

        public string UserId { get; set; }
        public string DiscountCode { get; set; }

        public int DiscountRate { get; set; }
        private List<BasketItemViewModel> _basketItems;

        public List<BasketItemViewModel> BasketItems
        {
            get
            {
                if (HasDiscount)
                {
                    _basketItems.ForEach(x =>
                    {
                        var discountPrice = x.Price * ((decimal)DiscountRate / 100);
                        x.AppliedDiscount(Math.Round(x.Price - discountPrice, 2));
                    });
                }
                return _basketItems;
            }
            set
            {
                _basketItems = value;
            }


        }

        public decimal TotalPrice
        {
            get
            {
                if(HasDiscount)
                {
                   return _basketItems.Sum(x => x.GetCurrentPrice);
                }
               return _basketItems.Sum(x => x.Price);
            }

            set
            {
                TotalPrice = value; 
            }
        }


        public bool HasDiscount
        {
            get => !string.IsNullOrEmpty(DiscountCode) && !DiscountRate.Equals(null);
        }

        public void CancelDiscount()
        {
            DiscountCode = null;
            DiscountRate = Convert.ToInt32(null);
        }

        public void ApplyDiscount(string code, int rate)
        {
            DiscountCode = code;
            DiscountRate = rate;
        }

  
    }
}
