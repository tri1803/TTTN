using System;
using System.Collections.Generic;
using System.Text;

namespace Temp.DataAccess.Data
{
    public class Cart
    {
        public int Id { get; set; }

        public int Status { get; set; }

        public int CartDetailId { get; set; }

        public CartDetail CartDetail { get; set; }
    }
}
