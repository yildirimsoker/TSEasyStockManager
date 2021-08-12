using System;
using System.Collections.Generic;
using System.Text;
using TS.EasyStockManager.Model.ViewModel.Base;

namespace TS.EasyStockManager.Model.ViewModel.Report.TransactionDetail
{
    public class ListTransactionDetailReportViewModel : BaseViewModel
    {
  
        public string StoreFullName { get; set; }
        public string ToStoreFullName { get; set; }
        public string Amount { get; set; }
        public string ProductFullName { get; set; }
        public string TransactionCode { get; set; }
        public string TransactionTypeName { get; set; }
        public string TransactionDate { get; set; }
    }
}
