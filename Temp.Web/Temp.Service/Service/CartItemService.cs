using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
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

        public IEnumerable<CartDetail> GetAll()
        {
            throw new NotImplementedException();
        }
    }
}
