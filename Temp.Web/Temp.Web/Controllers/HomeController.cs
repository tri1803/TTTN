using Microsoft.AspNetCore.Mvc;
using Temp.Service.Service;

namespace Temp.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IProductService _productService;
        private readonly ICategoryService _categoryService;

        public HomeController(IProductService productService, ICategoryService categoryService)
        {
            _productService = productService;
            _categoryService = categoryService;
        }

        public IActionResult Home()
        {
            ViewBag.MenuCate = _categoryService.GetAllCategories();
            ViewBag.NewProduct = _productService.GetProductNews_10();
            return View();
        }


    }
}