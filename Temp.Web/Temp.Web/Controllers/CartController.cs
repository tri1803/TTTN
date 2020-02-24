using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Temp.Service.DTO;
using Temp.Service.Service;
using Temp.Web.Infacestructure.Helpers;

namespace Temp.Web.Controllers {
    [Authorize]
    public class CartController : Controller {

        private readonly IProductService _productService;

        public CartController(IProductService productService)
        {
            _productService = productService;
        }
        public IActionResult Index()
        {
            var cart = SessionHelper.GetObjectFromJson<List<CartItemDto>>(HttpContext.Session, "cart");
            ViewBag.cart = cart;
            return View();
        }

        public IActionResult Add(int id)
        {            
            if (SessionHelper.GetObjectFromJson<List<CartItemDto>>(HttpContext.Session, "cart") == null)
            {
                List<CartItemDto> cart = new List<CartItemDto>();
                cart.Add(new CartItemDto { Product = _productService.GetById(id), Amount = 1 });
                SessionHelper.SetObjectAsJson(HttpContext.Session, "cart", cart);
            }
            else
            {
                List<CartItemDto> cart = SessionHelper.GetObjectFromJson<List<CartItemDto>>(HttpContext.Session, "cart");
                int index = isExist(id);
                if (index != -1)
                {
                    cart[index].Amount++;
                }
                else
                {
                    cart.Add(new CartItemDto { Product = _productService.GetById(id), Amount = 1 });
                }
                SessionHelper.SetObjectAsJson(HttpContext.Session, "cart", cart);
            }
            return RedirectToAction("Index", "Cart");
        }

        public IActionResult Update(int id, CartItemDto cartItem)
        {
            List<CartItemDto> cart = SessionHelper.GetObjectFromJson<List<CartItemDto>>(HttpContext.Session, "cart");
            int index = isExist(id);
            cart[index].Amount += cartItem.Amount;
            SessionHelper.SetObjectAsJson(HttpContext.Session, "cart", cart);
            return RedirectToAction("Index", "Cart");
        }

        public IActionResult Delete(int id)
        {
            List<CartItemDto> cart = SessionHelper.GetObjectFromJson<List<CartItemDto>>(HttpContext.Session, "cart");
            int index = isExist(id);
            cart.RemoveAt(index);
            SessionHelper.SetObjectAsJson(HttpContext.Session, "cart", cart);
            return RedirectToAction("Index", "Cart");
        }

        private int isExist(int id)
        {
            List<CartItemDto> cart = SessionHelper.GetObjectFromJson<List<CartItemDto>>(HttpContext.Session, "cart");
            for (int i = 0; i < cart.Count; i++)
            {
                if (cart[i].Product.Id.Equals(id))
                {
                    return i;
                }
            }
            return -1;
        }
    }
}