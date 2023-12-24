﻿using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace FreeCourse.Web.Models.Baskets
{
    public class BasketItemViewModel
    {
        public int Quantity { get; set; } = 1;
        public string CourseId { get; set; }
        public string CourseName { get; set; }
        public decimal Price { get; set; }

        private decimal DiscountAppliedPrice { get; set; }

        public decimal GetCurrentPrice => DiscountAppliedPrice.Equals(null) ? DiscountAppliedPrice : Price  ;

        public void AppliedDiscount(decimal discountPrice)
        {
            DiscountAppliedPrice = discountPrice;
        }
    }
}
