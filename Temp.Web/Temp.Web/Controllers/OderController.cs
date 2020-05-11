using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Temp.Service.Service;
using Temp.Common.Infrastructure;

namespace Temp.Web.Controllers
{
    [Authorize(Policy = Constants.Role.Shipper)]
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

        public IActionResult ShowDetail(int id)
        {
            var oder = _cartItemService.GetById(id);
            return View(oder);
        }        
    }
}