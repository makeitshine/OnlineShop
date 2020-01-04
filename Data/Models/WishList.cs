using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Aurora.Data.Models
{
    public class WishList
    {
        private readonly AppDbContext _appDbContext;
        private WishList(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public string WishListId { get; set; }

        public List<WishListItem> WishListItems { get; set; }

        public static WishList GetCart(IServiceProvider services)
        {
            ISession session = services.GetRequiredService<IHttpContextAccessor>()?
                .HttpContext.Session;

            var context = services.GetService<AppDbContext>();
            string cartId = session.GetString("CartId") ?? Guid.NewGuid().ToString();

            session.SetString("CartId", cartId);

            return new WishList(context) { WishListId = cartId };
        }

        public void AddToCart(Product product, int amount)
        {
            var wishListItem =
                    _appDbContext.WishListItems.SingleOrDefault(
                        s => s.Product.ProductId == product.ProductId && s.WishListId == WishListId);

            if (wishListItem == null)
            {
                wishListItem = new WishListItem
                {
                    WishListId = WishListId,
                    Product = product,
                    Amount = 1
                };

                _appDbContext.WishListItems.Add(wishListItem);
            }
            else
            {
                wishListItem.Amount++;
            }
            _appDbContext.SaveChanges();
        }

        public int RemoveFromCart(Product product)
        {
             var wishListItem =
                    _appDbContext.WishListItems.SingleOrDefault(
                        s => s.Product.ProductId == product.ProductId && s.WishListId == WishListId);

            var localAmount = 0;

            if (wishListItem != null)
            {
                if (wishListItem.Amount > 1)
                {
                    wishListItem.Amount--;
                    localAmount = wishListItem.Amount;
                }
                else
                {
                    _appDbContext.WishListItems.Remove(wishListItem);
                }
            }

            _appDbContext.SaveChanges();

            return localAmount;
        }

        public List<WishListItem> GetCartItems()
        {
            return WishListItems ??
                   (WishListItems =
                       _appDbContext.WishListItems.Where(c => c.WishListId == WishListId)
                           .Include(s => s.Product)
                           .ToList());
        }

        public void ClearCart()
        {
            var cartItems = _appDbContext
                .WishListItems
                .Where(cart => cart.WishListId == WishListId);

            _appDbContext.WishListItems.RemoveRange(cartItems);

            _appDbContext.SaveChanges();
        }
    }
}
