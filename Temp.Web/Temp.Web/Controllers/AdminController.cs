using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Temp.Common.Infrastructure;
using Temp.Service.Service;

namespace Temp.Web.Controllers
{
    /// <summary>
    /// admin controller
    /// </summary>
    [Authorize(Policy = Constants.Role.Shipper)]
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
            
            int[] arr = new int[12];
            for (int i = 0; i < 12; i++)
            {
                arr[i] = _cartService.GetTotalInMonth(i + 1);
            }
            ViewBag.total = arr;

            int[] arr2 = new int[12];
            for (int i = 0; i < 12; i++)
            {
                arr2[i] = _cartService.GetCountInMonth(i + 1);
            }
            ViewBag.total2 = arr2;

            var user = HttpContext.User.Identity.Name;
            return View("Index", user);
        }
    }
}