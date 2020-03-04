using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Temp.Service.Service;

namespace Temp.Web.Controllers
{
    [Authorize]
    public class OderController : Controller
    {
        private readonly ICartItemService _cartItemService;

        public OderController(ICartItemService cartItemService)
        {
            _cartItemService = cartItemService;
        }
        public IActionResult Index()
        {
            var oder = _cartItemService.GetAll();
            return View(oder);
        }

        public IActionResult Process(int id)
        {
            _cartItemService.Process(id);
            return RedirectToAction("Index","Oder");
        }

        public IActionResult Done(int id)
        {
            _cartItemService.Done(id);
            return RedirectToAction("Index", "Oder");
        }
    }
}