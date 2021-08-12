using System;
using System.Collections.Generic;
using System.Text;
using TS.EasyStockManager.Model.ViewModel.Base;

namespace TS.EasyStockManager.Model.ViewModel.Report.StoreStock
{
    public class ListStoreStockReportViewModel : BaseViewModel
    {
        public string QTY { get; set; }
        public string StoreFullName { get; set; }
        public string ProductFullName { get; set; }
    }
}
