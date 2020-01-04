using Aurora.Data.Models;
using Aurora.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Aurora.Components
{
    public class BasketSummary: ViewComponent
    {
        private readonly Basket _basket;
        public BasketSummary(Basket basket)
        {
            _basket = basket;
        }

        public IViewComponentResult Invoke()
        {
            var items = _basket.GetCartItems();
            _basket.BasketItems = items;

            var basketViewModel = new BasketViewModel
            {
                Basket = _basket,
                BasketTotal = _basket.GetCartTotal()
            };
            return View(basketViewModel);
        }


    }
}
