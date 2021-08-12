using System;
using System.Collections.Generic;
using System.Text;

namespace TS.EasyStockManager.Model.Domain
{
    public class BaseDTO
    {
        public BaseDTO()
        {
            SearchStartDate = DateTime.MinValue;
            SearchEndDate = DateTime.MaxValue;
            IsSuccess = true;
        }

        public int? Id { get; set; }
        public bool IsSuccess { get; set; }
        public string UserMessage { get; set; }
        public int? PageNumber { get; set; }
        public int? RecordCount { get; set; }
        public DateTime? SearchStartDate { get; set; }
        public DateTime? SearchEndDate { get; set; }
        
    }
}
