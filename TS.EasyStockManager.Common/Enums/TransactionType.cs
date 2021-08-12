using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace TS.EasyStockManager.Common.Enums
{
    public enum TransactionType
    {
        [Display(Name = "Stock Receipt")]
        StockIn = 1,
        [Display(Name = "Stock Out")]
        StockOut = 2,
        [Display(Name = "Transfer")]
        Transfer = 3
    }
}
