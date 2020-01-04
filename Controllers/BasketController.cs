using Aurora.Data.Interfaces;
using Aurora.Data.Models;
using Aurora.TagHelpers;
using Aurora.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace Aurora.Controllers
{
    public class BasketController : Controller
    {
        private readonly IProductRepository _productRepository;
        private readonly Basket _basket;
        private readonly IHttpContextAccessor _httpContextAccessor; 
    
        public BasketController(IProductRepository productRepository, Basket basket)
        {
            _productRepository = productRepository;
            _basket = basket;
        }

        public ViewResult Index()
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


        public RedirectToActionResult AddToBasket(int productId)
        {
            var selectedProduct = _productRepository.Products.FirstOrDefault(p => p.ProductId == productId);
            if (selectedProduct != null)
            {
                _basket.AddToCart(selectedProduct, 1);
            }
            return RedirectToAction("Index");
        }

        public RedirectToActionResult RemoveFromBasket(int productId)
        {
            var selectedProduct = _productRepository.Products.FirstOrDefault(p => p.ProductId == productId);
            if (selectedProduct != null)
            {
                _basket.RemoveFromCart(selectedProduct);
            }
            return RedirectToAction("Index");
        }

    }
}
