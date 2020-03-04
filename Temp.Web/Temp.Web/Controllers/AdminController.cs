using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Temp.Common.Infrastructure;
using Temp.Service.Service;

namespace Temp.Web.Controllers
{
    /// <summary>
    /// admin controller
    /// </summary>
    [Authorize(Policy = Constants.Role.Manager)]
    public class AdminController : Controller
    {
        private readonly ICategoryService _categoryService;
        private readonly IProductService _productService;
        private readonly ICartItemService _cartService;
        private readonly IUserService _userService;

        public AdminController(ICategoryService categoryService, IProductService productService, ICartItemService cartItemService, IUserService userService)
        {
            _categoryService = categoryService;
            _productService = productService;
            _cartService = cartItemService;
            _userService = userService;
        }
        /// <summary>
        /// view home admin
        /// </summary>
        /// <returns></returns>
        public IActionResult Index()
        {
            ViewBag.countCate = _categoryService.GetCountCate();
            ViewBag.countProduct = _productService.GetCountProduct();
            ViewBag.countOrder = _cartService.GetCountOrder();
            ViewBag.countUser = _userService.GetCountUser();
            ViewBag.listOrder = _cartService.Get10();
            var user = HttpContext.User.Identity.Name;
            return View("Index", user);
        }
    }
}