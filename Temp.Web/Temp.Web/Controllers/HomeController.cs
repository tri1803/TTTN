using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using Temp.DataAccess.Data;
using Temp.Service.DTO;
using Temp.Service.Service;
using Temp.Web.Infacestructure.Helpers;
using Microsoft.AspNetCore.Session;
using Microsoft.AspNetCore.Http;

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
            ViewBag.phukien = _productService.Get10Phukien();
            ViewBag.productInActive = _productService.GetProductInactive();
            
            return View();
        }

        public IActionResult DetailProduct(int id)
        {
            var product =  _productService.GetProductEditById(id);
            ViewBag.get4Mac = _productService.Get4Mac();
            ViewBag.get4Dell = _productService.Get4Dell();
            return View(product);
        }

        public IActionResult GetCateWithProducts(int id)
        {
            var products = _productService.GetProductWithCate(id);
            return View(products);
        }
        
    }
}