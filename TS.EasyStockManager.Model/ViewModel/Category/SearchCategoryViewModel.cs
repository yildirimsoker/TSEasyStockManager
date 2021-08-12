using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using TS.EasyStockManager.Model.ViewModel.Base;

namespace TS.EasyStockManager.Model.ViewModel.Category
{
    public class SearchCategoryViewModel: BaseViewModel
    {

        [Display(Name = "Category Name")]
        public string CategoryName { get; set; }

    }
}
