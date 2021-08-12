using System;
using System.Collections.Generic;
using System.Text;
using TS.EasyStockManager.Model.ViewModel.Base;

namespace TS.EasyStockManager.Model.ViewModel.Transaction
{
    public class ListTransactionViewModel : BaseViewModel
    {
        public string TransactionCode { get; set; }
        public int? TransactionTypeId { get; set; }
        public string TransactionDate { get; set; }
        public string Description { get; set; }
        public string StoreName { get; set; }
        public string ToStoreName { get; set; }
        public string TransactionTypeName { get; set; }
    }
}
