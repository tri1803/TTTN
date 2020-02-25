using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Temp.Common.Infrastructure;
using Temp.Service.DTO;
using Temp.Service.Service;

namespace Temp.Web.Controllers{
    [Authorize(Policy = Constants.Role.Manager)]
    public class SaleController : Controller {
        public readonly ISaleService _saleService;
        public readonly IProductService _productService;

        public SaleController(ISaleService saleService, IProductService productService)
        {
            _saleService = saleService;
            _productService = productService;
        }

        public IActionResult Index() {
            var sales = _saleService.GetAll();
            return View(sales);
        }

        [HttpGet]
        public IActionResult Create(int id) {
            var products = _productService.GetAllProduct();
            
            var sale = new SaleDto{
                Products = products,                            
            };
            return View(sale);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(SaleDto saleDto) 
        {
           _saleService.Create(saleDto);
            return RedirectToAction("Index", "Sale");
        }
    }
}