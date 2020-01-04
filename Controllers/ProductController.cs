using Aurora.Data;
using Aurora.Data.Interfaces;
using Aurora.Data.Models;
using Aurora.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Aurora.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductRepository _productRepository;
        private readonly ICategoryRepository _categoryRepository;

        [Authorize(Roles = "Admin")]
         public IActionResult Index() => View(_productRepository.Products.ToList());
        
        public ICollection<Category> Categories { get; set; }


        public ProductController(IProductRepository productRepository, ICategoryRepository categoryRepository)
        {
            _productRepository = productRepository;
            _categoryRepository = categoryRepository;
        }

        public ViewResult List(string category)
        {
            string _category = category;
            IEnumerable<Product> products;
            string currentCategory = string.Empty;

            if (string.IsNullOrEmpty(category))
            {
                products = _productRepository.Products.OrderBy(p => p.ProductId);
                currentCategory = "All products";
            }
            else
            {
                if (string.Equals("Rings", _category, StringComparison.OrdinalIgnoreCase))
                    products = _productRepository.Products.Where(p => p.Category.CategoryName.Equals("Rings")).OrderBy(p => p.Name);
                else
                    products = _productRepository.Products.Where(p => p.Category.CategoryName.Equals("Braslets")).OrderBy(p => p.Name);

                currentCategory = _category;
            }

            return View(new ProductsListViewModel
            {
                Products = products,
                CurrentCategory = currentCategory
            });
        }

        public ViewResult Search(string searchString)
        {
            string _searchString = searchString;
            IEnumerable<Product> products;
            string currentCategory = string.Empty;

            if (string.IsNullOrEmpty(_searchString))
            {
                products = _productRepository.Products.OrderBy(p => p.ProductId);
            }
            else
            {
                products = _productRepository.Products.Where(p=> p.Name.ToLower().Contains(_searchString.ToLower()));
            }

            return View("~/Views/Product/List.cshtml", new ProductsListViewModel{Products = products, CurrentCategory = "All products" });
        }

        public  ViewResult SortGreater(int price)
        {
            var products = from p in _productRepository.Products
                           select p;
            
            products = products.Where(s => s.Price>price);
        
            return View("~/Views/Product/List.cshtml", new ProductsListViewModel{Products = products, CurrentCategory = "All products" });
        }
        public  ViewResult SortLess(int price)
        {
            var products =  _productRepository.Products.Where(s => s.Price<price);
        
            return View("~/Views/Product/List.cshtml", new ProductsListViewModel{Products = products, CurrentCategory = "All products" });
        }

        public ViewResult Details(int productId)
        {
            var product = _productRepository.Products.FirstOrDefault(d => d.ProductId == productId);
            if (product == null)
            {
                return View("~/Views/Error/Error.cshtml");
            }
            return View(product);
        }
        [Authorize (Roles = "Admin")]
        [HttpGet] 
        public async Task<IActionResult> Remove(int id) 
        { 
            _productRepository.RemoveProduct(id); 
            await _productRepository.SaveChangesAsync(); 
            return RedirectToAction("Index"); 
        } 

        [Authorize (Roles = "Admin")]
        [HttpGet]
        public IActionResult Create()
        {
            Categories = _categoryRepository.Categories.ToList();
            ViewBag.Categories = Categories;
            Product pr = new Product();
            return View(pr);
        }
        
        [Authorize (Roles = "Admin")]
      
        public IActionResult Edit(int? id) 
        { 
            if (id == null) 
            { 
                return View(new ProductViewModel()); 
            } 
            else 
            { 
                var product = _productRepository.GetProductById((int)id); 
                return View(new ProductViewModel 
                { 
                    ProductId = product.ProductId,
                    Name = product.Name,
                    ShortDescription = product.ShortDescription,
                    Price = product.Price,
                    ImageThumbnailUrl = product.ImageThumbnailUrl
                   
                }); 
            } 
        }

        [Authorize (Roles = "Admin")]
        [HttpPost] 
        public async Task<IActionResult> Create(Product pr)
        {
            if(ModelState.IsValid){
                var product = new Product
                {
                    // ProductId = pr.ProductId,
                    Name = pr.Name,
                    ShortDescription = pr.ShortDescription,
                    LongDescription = pr.LongDescription,
                    Price = pr.Price,
                    Count = pr.Count,
                    ImageUrl = pr.ImageUrl,
                    CategoryId = pr.CategoryId,
                    ImageThumbnailUrl = pr.ImageThumbnailUrl
                
                };
                _productRepository.AddProduct(product);
                return RedirectToAction("Index", "Product", null);
            }
            Categories = _categoryRepository.Categories.ToList();
            ViewBag.Categories = Categories;
            return View();
        }
        
        [Authorize (Roles = "Admin")]
        [HttpPost] 
        public async Task<IActionResult> Edit(ProductViewModel vm) 
        { 
            if(ModelState.IsValid)
            {
                var product =  _productRepository.GetProductById((int)vm.ProductId);
                
                    product.ProductId = vm.ProductId;
                    product.Name = vm.Name;
                    product.ShortDescription = vm.ShortDescription;
                    product.Price = vm.Price;
                    product.Count = vm.Count;
                    product.ImageThumbnailUrl = vm.ImageThumbnailUrl;
                 
                _productRepository.UpdateProduct(product); 
                return RedirectToAction("Index", "Product", null);
            }
            return View(); 
        }
        
    }
}