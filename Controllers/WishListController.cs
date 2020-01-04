using Aurora.Data.Interfaces;
using Aurora.Data.Models;
using Aurora.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace Aurora.Controllers
{
    public class WishListController : Controller
    {
        private readonly IProductRepository _productRepository;
        private readonly WishList _wishList;

        public WishListController(IProductRepository productRepository, WishList wishList)
        {
            _productRepository = productRepository;
            _wishList = wishList;
        }

        public ViewResult Index()
        {
            var items = _wishList.GetCartItems();
            _wishList.WishListItems = items;

            var wishListViewModel = new WishListViewModel
            {
                WishList = _wishList
            };
            return View(wishListViewModel);
        }


        public RedirectToActionResult AddToWishList(int productId)
        {
            var selectedProduct = _productRepository.Products.FirstOrDefault(p => p.ProductId == productId);
            if (selectedProduct != null)
            {
                _wishList.AddToCart(selectedProduct, 1);
            }
            return RedirectToAction("Index");
        }

        public RedirectToActionResult RemoveFromWishList(int productId)
        {
            var selectedProduct = _productRepository.Products.FirstOrDefault(p => p.ProductId == productId);
            if (selectedProduct != null)
            {
                _wishList.RemoveFromCart(selectedProduct);
            }
            return RedirectToAction("Index");
        }

    }
}
