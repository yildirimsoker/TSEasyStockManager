using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TS.EasyStockManager.Model.Domain;
using TS.EasyStockManager.Model.Service;

namespace TS.EasyStockManager.Core.Service
{
    public interface IStoreStockService : IService<StoreStockDTO>
    {
        Task<ServiceResult<IEnumerable<StoreStockDTO>>> StoreStockReport(StoreStockDTO criteria);
    }
}
