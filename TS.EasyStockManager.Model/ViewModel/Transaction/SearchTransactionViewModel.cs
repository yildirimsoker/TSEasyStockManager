using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using TS.EasyStockManager.Model.ViewModel.Base;

namespace TS.EasyStockManager.Model.ViewModel.Transaction
{
    public class SearchTransactionViewModel : BaseViewModel
    {

        [Display(Name = "Transaction Code")]
        public string TransactionCode { get; set; }

        [Display(Name = "Store")]
        public int? StoreId { get; set; }

        [Display(Name = "To Store")]
        public int? ToStoreId { get; set; }

        [Display(Name = "Transaction Type")]
        public int? TransactionTypeId { get; set; }

        [Display(Name = "Start Date")]
        public string SearchStartDate { get; set; }

        [Display(Name = "End Date")]
        public string SearchEndDate { get; set; }

        public IEnumerable<SelectListItem> StoreList { get; set; }
        public IEnumerable<SelectListItem> ToStoreList { get; set; }
    }
}
