using System;
using System.Collections.Generic;
using System.Text;

namespace TS.EasyStockManager.Data.Entity
{
    public class Category : BaseEntity
    {
        public string CategoryName { get; set; }
        public virtual ICollection<Product> Product { get; set; }
    }
}
