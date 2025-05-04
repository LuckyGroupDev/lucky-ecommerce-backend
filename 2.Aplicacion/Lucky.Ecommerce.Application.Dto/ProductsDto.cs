using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lucky.Ecommerce.Application.Dto
{
    public class ProductsDto
    {
        public int productId { get; set; }  // int -> int
        public string productName { get; set; }  // nvarchar -> string
        public int supplierId { get; set; }  // int -> int
        public int categoryId { get; set; }  // int -> int
        public string quantityPerUnit { get; set; }  // nvarchar -> string
        public decimal unitPrice { get; set; }  // money -> decimal
        public short unitsInStock { get; set; }  // smallint -> short
        public short unitsOnOrder { get; set; }  // smallint -> short
        public short reorderLevel { get; set; }  // smallint -> short
        public bool discontinued { get; set; }  // bit -> bool

    }
}
