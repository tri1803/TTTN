
using System;
using System.Collections.Generic;
using Temp.DataAccess.Data;

namespace Temp.Service.DTO
{
    public class SaleDto
    {
        public int Id { get; set; }

        public string Desc { get; set; }

        public int ProductId { get; set; }

        public DateTime? CreateDate { get; set; }

        public DateTime? EndDate { get; set; }

        public int? PriceOld { get; set; }

        public int? PriceNow { get; set; }

        public Product Product { get; set; }

        public List<Product> Products { get; set; }
    }
}
