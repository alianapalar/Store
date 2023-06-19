using Entities.Models;
using Microsoft.AspNetCore.Mvc;
using Repositories;
using Repositories.Contracts;
using Services.Contracts;
using StoreApp.Models;

namespace StoreApp.Controllers
{
    public class ProductController : Controller
    {
        private readonly IServiceManager _manager;

        public ProductController(IServiceManager manager)
        {
            _manager = manager;
        }

        public IActionResult Index(ProductRequestParameters p)
        {
            var products=_manager.ProductService.GetAllProductsWitDetails(p);
            var pagination=new Pagination()
            {
                CurrentPage=p.PageNumber,
                ItemsPerPage=p.PageSize,
                TotalItems=_manager.ProductService.GetAllProducts(false).Count()
            };
            return View(new ProductListViewModel()
            {
                Products=products,
                Pagination=pagination
            });
        }
        public IActionResult Get(int id)
        {
            var model = _manager.ProductService.GetOneProduct(id,false);
            ViewData["Title"] = model?.ProductName;
            return View(model);
        }
    }
}