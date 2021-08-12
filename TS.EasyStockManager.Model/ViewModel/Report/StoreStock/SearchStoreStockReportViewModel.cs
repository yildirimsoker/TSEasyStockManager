using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using TS.EasyStockManager.Model.ViewModel.Base;

namespace TS.EasyStockManager.Model.ViewModel.Report.StoreStock
{
    public class SearchStoreStockReportViewModel : BaseViewModel
    {
        [Display(Name = "Product")]
        public int? ProductId { get; set; }

        [Display(Name = "Store")]
        public int? StoreId { get; set; }
        public IEnumerable<SelectListItem> StoreList { get; set; }
        public IEnumerable<SelectListItem> ProductList { get; set; }
    }
}
