using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Temp.Service.BaseService;
using Temp.Service.DTO;
using Temp.Service.Service;
using System.Linq;
using Temp.Common.Infrastructure;

namespace Temp.Web.Controllers
{    
    public class ProductController : Controller
    {
        private readonly IProductService _productService;
        private readonly ICategoryService _categoryService;
        private readonly INsxService _nsxService;
        private readonly ISaleService _saleService;
        private readonly IUnitofWork _unitofWork;

        public ProductController(IProductService productService, ICategoryService categoryService, INsxService nsxService, ISaleService saleService, IUnitofWork unitofWork)
        {
            _productService = productService;
            _categoryService = categoryService;
            _nsxService = nsxService;
            _saleService = saleService;
            _unitofWork = unitofWork;
        }

        // GET
        [Authorize(Policy = Constants.Role.Manager)]
        public IActionResult Index()
        {
            var products = _productService.GetAllActive();
            return View(products);
        }
        
        [HttpGet]
        [Authorize(Policy = Constants.Role.Manager)]
        public IActionResult Save(int id)
        {
            var cates = _categoryService.GetAllCategories();
            var nsxes = _nsxService.GetAllNsxes();
            if (id <= 0)
            {
                var viewInsert = new CreateProductDto
                {
                    Categories = cates,
                    Nsxes = nsxes
                };
                return View(viewInsert);
            }
            var viewEdit = _productService.GetProductEditById(id);
            viewEdit.Categories = cates;
            viewEdit.Nsxes = nsxes;
            return View(viewEdit);
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = Constants.Role.Manager)]
        public IActionResult Save(CreateProductDto productDto)
        {
            if (!ModelState.IsValid)
            {
                return View(productDto);
            }
            var product = _unitofWork.ProductBaseService.ObjectContext.Any(s => s.Name == productDto.Name);
            if (product == true)
            {
                ModelState.AddModelError(string.Empty, "San pham da ton tai");
                return View(productDto);
            } 
            _productService.Save(productDto, productDto.AvataPath);
            return RedirectToAction("Index", "Product");
            
        }

        [Authorize(Policy = Constants.Role.Manager)]
        public IActionResult Delete(int id)
        {
            _productService.Delete(id);
            return RedirectToAction("Index", "Product");
        }

        [Authorize(Policy = Constants.Role.Manager)]
        public IActionResult Active(int id)
        {
            _productService.Active(id);
            return RedirectToAction("Index", "Product");
        }

        [Authorize(Policy = Constants.Role.Manager)]
        public IActionResult InActive(int id)
        {
            _productService.InActive(id);
            return RedirectToAction("Index", "Product");
        }

        public IActionResult Search(string key)
        {
            var products = _productService.SeachProduct(key);
            return View(products);
        }

    }
}