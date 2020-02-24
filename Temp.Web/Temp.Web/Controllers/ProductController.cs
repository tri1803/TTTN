using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Temp.Service.DTO;
using Temp.Service.Service;

namespace Temp.Web.Controllers
{
    [Authorize]
    public class ProductController : Controller
    {
        private readonly IProductService _productService;
        private readonly ICategoryService _categoryService;
        private readonly INsxService _nsxService;
        private readonly ISaleService _saleService;

        public ProductController(IProductService productService, ICategoryService categoryService, INsxService nsxService, ISaleService saleService)
        {
            _productService = productService;
            _categoryService = categoryService;
            _nsxService = nsxService;
            _saleService = saleService;
        }
        
        // GET
        public IActionResult Index()
        {
            var products = _productService.GetAllActive();
            return View(products);
        }
        
        [HttpGet]
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
        public IActionResult Save(CreateProductDto productDto)
        {
            _productService.Save(productDto, productDto.AvataPath);
            return RedirectToAction("Index", "Product");
            //var cates = _categoryService.GetAllCategories();
            //var nsxes = _nsxService.GetAllNsxes();
            //if (productDto.Id <= 0)
            //{
            //    var viewInsert = new CreateUserDto
            //    {
            //        Roles = roles
             //   };
             //   return RedirectToAction("Index", "User");
            //}
           // var viewEdit = _userService.GetUserEditById(userDto.Id);
            //viewEdit.Roles = roles;
            //return View(viewEdit);
        }

        public IActionResult Delete(int id)
        {
            _productService.Delete(id);
            return RedirectToAction("Index", "Product");
        }

        public IActionResult Active(int id)
        {
            _productService.Active(id);
            return RedirectToAction("Index", "Product");
        }

        public IActionResult InActive(int id)
        {
            _productService.InActive(id);
            return RedirectToAction("Index", "Product");
        }

    }
}