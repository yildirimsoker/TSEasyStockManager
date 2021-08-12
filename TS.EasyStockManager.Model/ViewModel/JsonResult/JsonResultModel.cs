using System;
using System.Collections.Generic;
using System.Text;

namespace TS.EasyStockManager.Model.ViewModel.JsonResult
{
    public class JsonResultModel
    {
        public JsonResultModel()
        {
            IsSucceeded = true;
            IsRedirect = false;
        }
        public bool IsSucceeded { get; set; }
        public bool IsRedirect { get; set; }
        public string UserMessage { get; set; }
        public string RedirectUrl { get; set; }
        public object Data { get; set; }
    }
}
