using System;
using System.Collections.Generic;
using System.Text;
using TS.EasyStockManager.Model.ViewModel.Base;

namespace TS.EasyStockManager.Model.ViewModel.UnitOfMeasure
{
    public class SearchUnitOfMeasureViewModel : BaseViewModel
    {
        public string UnitOfMeasureName { get; set; }
        public string Isocode { get; set; }
    }
}
