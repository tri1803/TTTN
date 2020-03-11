using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Temp.Common.Infrastructure;
using Temp.DataAccess.Data;
using Temp.Service.BaseService;
using Temp.Service.DTO;

namespace Temp.Service.Service
{
    public class CartItemService : ICartItemService
    {
        private readonly IUnitofWork _unitofWork;
        private readonly IMapper _mapper;

        public CartItemService(IUnitofWork unitofWork, IMapper mapper)
        {
            _unitofWork = unitofWork;
            _mapper = mapper;
        }

        public void Create(CartItemDto cartItemDto)
        {
            throw new NotImplementedException();
        }

        public void Done(int id)
        {
            var cart = _unitofWork.CartDetailBaseService.GetById(id);
            cart.Status = (int)OrderType.Done;
            cart.Date = DateTime.Now;
            _unitofWork.CartDetailBaseService.Update(cart);
            _unitofWork.Save();
        }

        public List<CartDetail> Get10()
        {
            var results = _unitofWork.CartDetailBaseService.ObjectContext.Where(s => s.Status == (int)OrderType.Done).Include(s => s.Product).Include(s => s.User).Take(10).ToList();
            return results;
        }

        public IEnumerable<CartDetail> GetAll()
        {
            return _unitofWork.CartDetailBaseService.ObjectContext.Include(s => s.Product).Include(s => s.User).ToList().OrderByDescending(s => s.Date).AsEnumerable();
        }

        public int GetCountInMonth(int month)
        {
            var total = _unitofWork.CartDetailBaseService.ObjectContext.Where(s => s.Date.Value.Month.ToString().Equals(month.ToString())).Count();
            return total;
        }

        public int GetCountOrder()
        {
            return _unitofWork.CartDetailBaseService.GetAll().Count();
        }

        public int GetTotalInMonth(int month)
        {
            var total = _unitofWork.CartDetailBaseService.ObjectContext.FirstOrDefault(s => s.Date.Value.Month.ToString().Equals(month.ToString()));
            return Convert.ToInt32((total.Amount * total.Price));
        }

        public void Process(int id)
        {
            var cart = _unitofWork.CartDetailBaseService.GetById(id);
            cart.Status = (int)OrderType.Processing;
            _unitofWork.CartDetailBaseService.Update(cart);
            _unitofWork.Save();
        }
    }
}
