using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using TS.EasyStockManager.Model.ViewModel.Base;

namespace TS.EasyStockManager.Model.ViewModel.Transaction
{
    public class EditTransactionViewModel : BaseViewModel
    {
        public string PageName { get; set; }

        [Required]
        [MaxLength(30)]
        [Display(Name = "Transaction Code")]
        public string TransactionCode { get; set; }

        [Required]
        [Display(Name = "Store")]
        public int StoreId { get; set; }

        [Required]
        [Display(Name = "To Store")]
        public int? ToStoreId { get; set; }
        public int TransactionTypeId { get; set; }

        [Required]
        [Display(Name = "Date")]
        public string TransactionDate { get; set; }

        public string Description { get; set; }

        public IList<TransactionDetailViewModel> TransactionDetail { get; set; }
        public IEnumerable<SelectListItem> StoreList { get; set; }
        public IEnumerable<SelectListItem> ToStoreList { get; set; }
        public int TransactionDetailCount { get; set; }
    }
}
