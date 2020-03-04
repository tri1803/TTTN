using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Temp.DataAccess.Data;
using Temp.Service.BaseService;
using Temp.Service.DTO;
using Temp.Service.Service;
using Temp.Web.Infacestructure.Helpers;

namespace Temp.Web.Controllers {
    [Authorize]
    public class CartController : Controller {

        private readonly IProductService _productService;
        private readonly IUnitofWork _unitofWork;

        public CartController(IProductService productService, IUnitofWork unitofWork)
        {
            _productService = productService;
            _unitofWork = unitofWork;
        }
        public IActionResult Index()
        {
            var cart = SessionHelper.GetObjectFromJson<List<CartItemDto>>(HttpContext.Session, "cart");
            ViewBag.cart = cart;
            return View(cart);
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

        [HttpPost]
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

        [HttpGet]
        public IActionResult Checkout()
        {
            return View();
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public IActionResult Checkout(CreateUserDto userDto)
        {
            var name = HttpContext.User.Identity.Name;
            var user = _unitofWork.UserBaseService.Get(s => s.Username == name);
            List<CartItemDto> cart = SessionHelper.GetObjectFromJson<List<CartItemDto>>(HttpContext.Session, "cart");
            //update user
            user.FirstName = userDto.FirstName;
            user.LastName = userDto.LastName;
            user.Phone = userDto.Phone;
            user.Address = userDto.Address;
            _unitofWork.UserBaseService.Update(user);

            //them vao cart detail
            foreach (var item in cart)
            {
                var cartDetail = new CartDetail();
                cartDetail.Amount = item.Amount;
                cartDetail.Price = item.Product.Price;
                cartDetail.ProductId = item.Product.Id;
                cartDetail.Status = 0;
                cartDetail.UserId = user.Id;
                _unitofWork.CartDetailBaseService.Add(cartDetail);
            }


            //giam so luong product
            foreach (var item in cart)
            {
                var product = _unitofWork.ProductBaseService.GetById(item.Product.Id);
                product.Amount -= item.Amount;
                _unitofWork.ProductBaseService.Update(product);
            }

            _unitofWork.Save();
            cart = null;
            SessionHelper.SetObjectAsJson(HttpContext.Session, "cart", cart);
            return RedirectToAction();
        }
    }
}