using Aurora.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Aurora.Data.Models;
using Microsoft.EntityFrameworkCore;
using Aurora.ViewModels;

namespace Aurora.Data.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly AppDbContext _appDbContext;
        public ProductRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public IEnumerable<Product> Products => _appDbContext.Products.Include(c => c.Category);

        public IEnumerable<Product> PreferredProducts => _appDbContext.Products.Where(p => p.IsPreferredProduct).Include(c => c.Category);

        public Product GetProductById(int productId) => _appDbContext.Products.FirstOrDefault(p => p.ProductId == productId);
        public void UpdateProduct(Product product) 
        { 
           
            _appDbContext.Products.Update(product);
            SaveChangesAsync();
        }
        public void AddProduct(Product product) 
        { 
            _appDbContext.Products.Add(product); 
            SaveChangesAsync();

        }
        public void RemoveProduct(int id)
        {
            _appDbContext.Products.Remove(GetProductById(id));
        }
        public async Task<bool> SaveChangesAsync() 
        { 
            if (await _appDbContext.SaveChangesAsync()>0) 
            { 
                return true; 
            } 
                return false; 
            }
        }
}
