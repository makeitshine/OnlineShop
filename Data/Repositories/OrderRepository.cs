using Aurora.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Aurora.Data.Models;

namespace Aurora.Data.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly AppDbContext _appDbContext;
        private readonly Basket _basket;

        public IEnumerable<Order> Orders => _appDbContext.Orders;
        public OrderRepository(AppDbContext appDbContext, Basket basket)
        {
            _appDbContext = appDbContext;
            _basket = basket;
        }


        public void CreateOrder(Order order)
        {
            order.OrderPlaced = DateTime.Now;

            _appDbContext.Orders.Add(order);

            var basketItems = _basket.BasketItems;

            foreach (var basketItem in basketItems)
            {
                var orderDetail = new OrderDetail()
                {
                    Amount = basketItem.Amount,
                    ProductId = basketItem.Product.ProductId,
                    OrderId = order.OrderId,
                    Price = basketItem.Product.Price
                };

                _appDbContext.OrderDetails.Add(orderDetail);
            }

            _appDbContext.SaveChanges();
        }
    }

}
