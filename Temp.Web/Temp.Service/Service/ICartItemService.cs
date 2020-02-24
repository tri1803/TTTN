using System;
using System.Collections.Generic;
using System.Text;
using Temp.DataAccess.Data;
using Temp.Service.DTO;

namespace Temp.Service.Service
{
    public interface ICartItemService
    {
        IEnumerable<CartDetail> GetAll();

        void Create(CartItemDto cartItemDto);
    }
}
