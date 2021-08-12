using System;
using System.Collections.Generic;
using System.Text;

namespace TS.EasyStockManager.Data.Entity
{
    public class BaseEntity
    {
        public int Id { get; set; }
        public DateTime CreateDate { get; set; }
    }
}
