using System;
using System.Collections.Generic;
using System.Text;

namespace TS.EasyStockManager.Data.Entity
{
    public class UnitOfMeasure : BaseEntity
    {
        public string UnitOfMeasureName { get; set; }
        public string Isocode { get; set; }
        public virtual ICollection<Product> Product { get; set; }
    }
}
