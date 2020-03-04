using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Temp.DataAccess.Data
{
    /// <summary>
    /// Class Product
    /// </summary>
    public class Product
    {
      
        public int Id { get; set; }
        
      
        public string Name { get; set; }
        
     
        public int? Amount { get; set; }
        
       
        public int? Price { get; set; }
        
        
        [Column(TypeName = "ntext")]
        public string Desc { get; set; }
        
       
        public int CategoryId { get; set; }

        public int NsxId { get; set; }

        public DateTime? CreateDate { get; set; }

        public int? Status { get; set; }

        public string Avatar { get; set; }

        public int ProductType { get; set; }

        public Category Category { get; set; }

        public Nsx Nsx { get; set; }

        public Image Image { get; set; }

        public Sale Sale { get; set; }

        public Comment Comment { get; set; }

        public ICollection<CartDetail> CartDetails { get; set; }
    }
}