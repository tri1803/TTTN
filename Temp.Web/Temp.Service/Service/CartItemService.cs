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

        public IEnumerable<CartDetail> GetAll()
        {
            return _unitofWork.CartDetailBaseService.ObjectContext.Include(s => s.Product).Include(s => s.User).ToList().AsEnumerable();
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
