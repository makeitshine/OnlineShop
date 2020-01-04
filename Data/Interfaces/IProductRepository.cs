using Aurora.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Aurora.Data.Interfaces
{
    public interface IProductRepository
    {
        IEnumerable<Product> Products { get; }
        IEnumerable<Product> PreferredProducts { get; }
        Product GetProductById(int productId);
        void UpdateProduct(Product product);
        void AddProduct(Product product);
        void RemoveProduct(int id);
        Task<bool> SaveChangesAsync();
    }
}