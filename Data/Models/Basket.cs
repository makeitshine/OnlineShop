using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Aurora.Data.Models
{
    public class Basket
    {
         private readonly AppDbContext _appDbContext;
        private Basket(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public string BasketId { get; set; }

        public List<BasketItem> BasketItems { get; set; }

        public static Basket GetCart(IServiceProvider services)
        {
            ISession session = services.GetRequiredService<IHttpContextAccessor>()?
                .HttpContext.Session;

            var context = services.GetService<AppDbContext>();
            string cartId = session.GetString("CartId") ?? Guid.NewGuid().ToString();

            session.SetString("CartId", cartId);

            return new Basket(context) { BasketId = cartId };
        }

        public void AddToCart(Product product, int amount)
        {
            var basketItem =
                    _appDbContext.BasketItems.SingleOrDefault(
                        s => s.Product.ProductId == product.ProductId && s.BasketId == BasketId);

            if (basketItem == null)
            {
                basketItem = new BasketItem
                {
                    BasketId = BasketId,
                    Product = product,
                    Amount = 1
                };
                product.Count--;

                _appDbContext.BasketItems.Add(basketItem);
            }
            else
            {
                basketItem.Amount++;
                product.Count--;
            }
            _appDbContext.SaveChanges();
        }

        public int RemoveFromCart(Product product)
        {
            var basketItem =
                    _appDbContext.BasketItems.SingleOrDefault(
                        s => s.Product.ProductId == product.ProductId && s.BasketId == BasketId);

            var localAmount = 0;

            if (basketItem != null)
            {
                if (basketItem.Amount > 1)
                {
                    basketItem.Amount--;
                    localAmount = basketItem.Amount;
                    product.Count++;
                }
                else
                {
                    _appDbContext.BasketItems.Remove(basketItem);
                    product.Count++;
                }
            }

            _appDbContext.SaveChanges();

            return localAmount;
        }

        public List<BasketItem> GetCartItems()
        {
            return BasketItems ??
                   (BasketItems =
                       _appDbContext.BasketItems.Where(c => c.BasketId == BasketId)
                           .Include(s => s.Product)
                           .ToList());
        }

        public void ClearCart()
        {
            var cartItems = _appDbContext
                .BasketItems
                .Where(cart => cart.BasketId == BasketId);

            _appDbContext.BasketItems.RemoveRange(cartItems);

            _appDbContext.SaveChanges();
        }

        public decimal GetCartTotal()
        {
            var total = _appDbContext.BasketItems.Where(c => c.BasketId == BasketId)
                .Select(c => c.Product.Price * c.Amount).Sum();
            return total;
        }
    }
}
