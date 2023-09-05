using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using TS.EasyStockManager.Model.ViewModel.Base;

namespace TS.EasyStockManager.Model.ViewModel.UnitOfMeasure
{
    public class SearchUnitOfMeasureViewModel : BaseViewModel
    {
        [Display(Name ="Ölçü Birimi")]
        public string UnitOfMeasureName { get; set; }

        [Display(Name ="Ölçü Kodu")]
        public string Isocode { get; set; }
    }
}
