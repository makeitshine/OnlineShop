using Aurora.Data.Interfaces;
using Aurora.Data.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Aurora.Controllers
{
    public class OrderController : Controller
    {
        private readonly IOrderRepository _orderRepository;
        private readonly Basket _basket;

        public OrderController(IOrderRepository orderRepository, Basket basket)
        {
            _orderRepository = orderRepository;
            _basket = basket;
        }

        
        public IActionResult Checkout()
        {
            return View();
        } 

        [HttpPost]
        public IActionResult Checkout(Order order)
        {
            var items = _basket.GetCartItems();
            _basket.BasketItems = items;
            if (_basket.BasketItems.Count == 0)
            {
                ModelState.AddModelError("", "Your basket is empty, add some products first");
            }

            if (ModelState.IsValid)
            {
                _orderRepository.CreateOrder(order);
                _basket.ClearCart();
                return RedirectToAction("CheckoutComplete");
            }

            return View(order);
        }

        public IActionResult CheckoutComplete()
        {
            ViewBag.CheckoutCompleteMessage = "Thanks for your order! :) ";
            return View();
        }
    }
}
