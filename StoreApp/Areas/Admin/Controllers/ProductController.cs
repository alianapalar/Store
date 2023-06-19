using Entities.DTOs.Product;
using Entities.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Services.Contracts;
using StoreApp.Models;

namespace StoreApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles ="Admin")]
    public class ProductController:Controller
    {
        private readonly IServiceManager _manager;

        public ProductController(IServiceManager manager)
        {
            _manager = manager;
        }

        public IActionResult Index(ProductRequestParameters p)
        {
            ViewData["Title"] = "Products";

            var products = _manager.ProductService.GetAllProductsWitDetails(p);
            var pagination = new Pagination()
            {
                CurrentPage = p.PageNumber,
                ItemsPerPage = p.PageSize,
                TotalItems = _manager.ProductService.GetAllProducts(false).Count()
            };
            return View(new ProductListViewModel()
            {
                Products = products,
                Pagination = pagination
            });
        }
        public IActionResult Create()
        {
            ViewBag.categories=GetCategoriesSelectList();
            return View();
        }
        private SelectList GetCategoriesSelectList()
        {
            return new SelectList(_manager.CategoryService.GetAllCategories(false),
            "CategoryID",
            "CategoryName", "1");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([FromForm] ProductDTOForInsertion productDto,IFormFile file)
        {
            if (ModelState.IsValid)
            {
                string path=Path.Combine(Directory.GetCurrentDirectory(),"wwwroot","images",file.FileName);
                using (var stream=new FileStream(path,FileMode.Create))
                {
                     await file.CopyToAsync(stream);                   
                }
                productDto.ImageUrl=String.Concat("/images/",file.FileName);
                _manager.ProductService.CreateProduct(productDto);
                TempData["success"] = $"{productDto.ProductName} has been created.";
                return RedirectToAction("Index");
            }
            return View();
        }
        public IActionResult Update([FromRoute(Name ="id")] int id)
        {
            ViewBag.categories=GetCategoriesSelectList();
            var model=_manager.ProductService.GetOneProductForUpdate(id,false);
             ViewData["Title"] = model?.ProductName;
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> Update([FromForm]ProductDTOForUpdate productDto,IFormFile file)
        {
            if (ModelState.IsValid)
            {
                string path=Path.Combine(Directory.GetCurrentDirectory(),"wwwroot","images",file.FileName);
                using (var stream=new FileStream(path,FileMode.Create))
                {
                     await file.CopyToAsync(stream);                   
                }
                productDto.ImageUrl=String.Concat("/images/",file.FileName);
                _manager.ProductService.UpdateOneProduct(productDto);
                return RedirectToAction("Index");
            }
            return View();
        }
        public IActionResult Delete(int id)
        {
            _manager.ProductService.DeleteOneProduct(id);
            TempData["danger"] = "The product has been removed.";
            return RedirectToAction("Index");
        }
        
    }
}