using Microsoft.AspNetCore.Mvc;
using RenielDavidInventorySystem.Infrastructure.Domain;
using RenielDavidInventorySystem.Infrastructure.Domain.Models;
using RenielDavidInventorySystem.Infrastructure.Paged;
using RenielDavidInventorySystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RenielDavidInventorySystem.Areas.Manage.Controllers
{
    [Area("manage")]
    public class ProductController : Controller
    {
        private readonly DefaultDbContext _context;

        public ProductController(DefaultDbContext context)
        {
            _context = context;
        }
        [HttpGet("manage/product")]
        public IActionResult Index(int pageIndex = 1,
                                    int pageSize = 10,
                                    string sortBy = "",
                                    string sortOrder = "asc",
                                    string keyword = "")
        {
            IQueryable<Product> allProducts = _context.Products.AsQueryable();
            Paged<Product> Products = new Paged<Product>();

            if (!string.IsNullOrEmpty(keyword))
            {
                allProducts = allProducts.Where(f => f.Brand.Contains(keyword) || f.Name.Contains(keyword) || f.Tagline
                .Contains(keyword));
            }
            Products.Items = allProducts.ToList();
            var queryCount = allProducts.Count();
            var skip = pageSize * (pageIndex - 1);

            long pageCount = (long)Math.Ceiling((decimal)(queryCount / pageSize));

            /*if (sortBy.ToLower() == "brand" && sortOrder.ToLower() == "asc")
            {
                Products.Items = allProducts.OrderBy(e => e.Brand).Skip(skip).Take(pageSize).ToList();
            }

            if (sortBy.ToLower() == "firstname" && sortOrder.ToLower() == "desc")
            {
               Products.Items = allProducts.OrderByDescending(e => e.Brand).Skip(skip).Take(pageSize).ToList();
            }

            if (sortBy.ToLower() == "lastname" && sortOrder.ToLower() == "asc")
            {
                Products.Items = allProducts.OrderBy(e => e.Name).Skip(skip).Take(pageSize).ToList();
            }

            if (sortBy.ToLower() == "lastname" && sortOrder.ToLower() == "desc")
            {
                Products.Items = allProducts.OrderByDescending(e => e.Name).Skip(skip).Take(pageSize).ToList();
            }*/


            var result = new ProductSearchViewModel();
            result.Products = new Paged<ProductViewModel>();
            result.Products.Keyword = keyword;
            result.Products.PageCount = pageCount;
            result.Products.PageIndex = pageIndex;
            result.Products.PageSize = pageSize;
            result.Products.QueryCount = queryCount;

            result.Products.Items = new List<ProductViewModel>();

            foreach (var product in Products.Items)
            {
                result.Products.Items.Add(new ProductViewModel()
                {

                    Name = product.Name,
                    Brand = product.Brand,
                    Tagline = product.Tagline,
                    Description = product.Description,
                    Price = product.Price,
                    CategoryId = product.CategoryId,
                    CreatedAt = product.CreatedAt,
                    UpdatedAt = product.UpdatedAt,
                    ID = product.ID
                });
            }



            return View(result);
        }
        [HttpGet("manage/product/AddProduct")]
        public IActionResult AddProduct()
        {
            return View();
        }


        [HttpPost, Route("~/Create")]
        public IActionResult Create(Models.ProductViewModel model)
        {
            Infrastructure.Domain.Models.Product product = new Infrastructure.Domain.Models.Product()
            {
                Name = model.Name,
                Brand = model.Brand,
                Price = model.Price,
                Description = model.Description,
                Tagline = model.Tagline,
                CategoryId = Guid.NewGuid()
                
                

                
            };
            _context.Products.Add(product);
            _context.SaveChanges();
            return Redirect("~/");
        }



    }
}
