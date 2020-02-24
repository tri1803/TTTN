using System;
using Temp.DataAccess.Data;

namespace Temp.Service.DTO
{
    public class ProductDto
    {
        /// <summary>
        /// primary key product
        /// </summary>
        public int Id { get; set; }
        
        /// <summary>
        /// name of product
        /// </summary>
        public string Name { get; set; }
        
        /// <summary>
        /// amount of product
        /// </summary>
        public int? Amount { get; set; }
        
        /// <summary>
        /// price of a product
        /// </summary>
        public int? Price { get; set; }
        
        /// <summary>
        /// description of product
        /// </summary>
        public string Desc { get; set; }
        
        /// <summary>
        /// foreign key categoryid
        /// </summary>
        public int CategoryId { get; set; }

        public int NsxId { get; set; }

        public int ProductType { get; set; }

        public DateTime? CreateDate { get; set; }

        public int? Status { get; set; }

        public string Avatar { get; set; }

        public Category Category { get; set; }

        public Nsx Nsx { get; set; }
    }
}