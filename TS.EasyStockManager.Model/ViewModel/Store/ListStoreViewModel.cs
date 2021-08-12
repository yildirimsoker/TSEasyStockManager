using System;
using System.Collections.Generic;
using System.Text;
using TS.EasyStockManager.Model.ViewModel.Base;

namespace TS.EasyStockManager.Model.ViewModel.Store
{
    public class ListStoreViewModel : BaseViewModel
    {
        public string StoreName { get; set; }
        public string StoreCode { get; set; }
    }
}
