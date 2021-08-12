using System;
using System.Collections.Generic;
using System.Text;

namespace TS.EasyStockManager.Data.Entity
{
    public class TransactionDetail
    {
        public int ProductId { get; set; }
        public int TransactionId { get; set; }
        public decimal Amount { get; set; }
        public virtual Product Product { get; set; }
        public virtual Transaction Transaction { get; set; }
    }
}
