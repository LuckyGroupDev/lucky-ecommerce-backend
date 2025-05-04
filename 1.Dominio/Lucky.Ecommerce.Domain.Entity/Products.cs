using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lucky.Ecommerce.Domain.Entity
{
    public class Products
    {
        public int ProductID { get; set; }  // int -> int
        public string ProductName { get; set; }  // nvarchar -> string
        public int SupplierID { get; set; }  // int -> int
        public int CategoryID { get; set; }  // int -> int
        public string QuantityPerUnit { get; set; }  // nvarchar -> string
        public decimal UnitPrice { get; set; }  // money -> decimal
        public short UnitsInStock { get; set; }  // smallint -> short
        public short UnitsOnOrder { get; set; }  // smallint -> short
        public short ReorderLevel { get; set; }  // smallint -> short
        public bool Discontinued { get; set; }  // bit -> bool

    }
}
