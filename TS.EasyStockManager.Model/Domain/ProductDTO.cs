using System;
using System.Collections.Generic;
using System.Text;

namespace TS.EasyStockManager.Model.Domain
{
    public class ProductDTO : BaseDTO
    {
        public string ProductName { get; set; }
        public string Barcode { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public decimal? Price { get; set; }
        public int? CategoryId { get; set; }
        public string CategoryName { get; set; }
        public int? UnitOfMeasureId { get; set; }
        public string UnitOfMeasureName { get; set; }

    }
}
