using System;
using System.Collections.Generic;
using System.Text;

namespace TS.EasyStockManager.Model.Domain
{
    public class TransactionDTO : BaseDTO
    {
        public TransactionDTO()
        {
            TransactionDetail = new List<TransactionDetailDTO>();
        }

        public string TransactionCode { get; set; }
        public int? StoreId { get; set; }
        public int? ToStoreId { get; set; }
        public int? TransactionTypeId { get; set; }
        public DateTime TransactionDate { get; set; }
        public string Description { get; set; }
        public string StoreName { get; set; }
        public string ToStoreName { get; set; }
        public string TransactionTypeName { get; set; }
        public IList<TransactionDetailDTO> TransactionDetail { get; set; }
    }
}
