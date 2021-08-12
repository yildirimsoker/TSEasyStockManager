using System;
using System.Collections.Generic;
using System.Text;

namespace TS.EasyStockManager.Model.Service
{
    public partial class ServiceResult
    {
        public ServiceResult()
        {
            IsSucceeded = true;
        }
        public bool IsSucceeded { get; set; }
        public string UserMessage { get; set; }
        public int Id { get; set; }
   

    }
    public partial class ServiceResult<T>
    {

        public ServiceResult()
        {
            IsSucceeded = true;
        }
        public bool IsSucceeded { get; set; }
        public string UserMessage { get; set; }
        public int Id { get; set; }
        public T TransactionResult { get; set; }
    }
}
