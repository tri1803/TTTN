using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Temp.Service.Service;

namespace Temp.Web.Controllers
{
    public class BlogController : Controller
    {
        private readonly IProductService _productService;
        private readonly ICategoryService _categoryService;

        public BlogController(IProductService productService, ICategoryService categoryService)
        {
            _productService = productService;
            _categoryService = categoryService;
        }

        public IActionResult Index()
        {
            ViewBag.MenuCate = _categoryService.GetAllCategories();
            ViewBag.NewProduct = _productService.GetProductNews_10();
            return View();
        }
    }
}