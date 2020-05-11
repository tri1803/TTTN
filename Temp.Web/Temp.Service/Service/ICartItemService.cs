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

        void Process(int id);

        void Done(int id);

        int GetCountOrder();

        List<CartDetail> Get10();

        int GetTotalInMonth(int month);

        int GetCountInMonth(int month);

        CartDetail GetById(int id);
    }
}
