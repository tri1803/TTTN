using System;
using System.Collections.Generic;
using System.Text;
using Temp.DataAccess.Data;

namespace Temp.Service.DTO
{
    public class CartItemDto
    {
       // public int ProductId { get; set; }
        
        public int Amount { get; set; }

        public Product Product { get; set; }
    }
}
