using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TS.EasyStockManager.Core.Service;
using TS.EasyStockManager.Core.UnitOfWorks;
using TS.EasyStockManager.Model.Domain;
using TS.EasyStockManager.Model.Service;
using TS.EasyStockManager.Service.Base;
using Entity = TS.EasyStockManager.Data.Entity;

namespace TS.EasyStockManager.Service.StoreStock
{
    public class StoreStockService : BaseService, IStoreStockService
    {
        public StoreStockService(IUnitOfWorks unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {
        }

        public async Task<ServiceResult> AddAsync(StoreStockDTO model)
        {
            ServiceResult result = new ServiceResult();
            try
            {
                using (_unitOfWork)
                {
                    Entity.StoreStock entity = _mapper.Map<Data.Entity.StoreStock>(model);
                    await _unitOfWork.StoreStockRepository.AddAsync(entity);
                    await _unitOfWork.SaveAsync();
                }
            }
            catch (Exception ex)
            {
                result.IsSucceeded = false;
                result.UserMessage = ex.Message;
            }
            return result;
        }

        public async Task<ServiceResult<IEnumerable<StoreStockDTO>>> Find(StoreStockDTO criteria)
        {
            ServiceResult<IEnumerable<StoreStockDTO>> result = new ServiceResult<IEnumerable<StoreStockDTO>>();

            try
            {
                using (_unitOfWork)
                {
                    IEnumerable<Entity.StoreStock> list = await _unitOfWork
                                                                .StoreStockRepository
                                                                .FindAsync(filter: x => (criteria.ProductId == null || x.ProductId == criteria.ProductId) &&
                                                                                        (criteria.StoreId == null || x.StoreId == criteria.StoreId),
                                                                           orderByDesc: x => x.ProductId,
                                                                           skip: criteria.PageNumber,
                                                                           take: criteria.RecordCount);

                    result.TransactionResult = _mapper.Map<IEnumerable<StoreStockDTO>>(list);
                }
            }
            catch (Exception ex)
            {
                result.IsSucceeded = false;
                result.UserMessage = ex.Message;
            }

            return result;
        }

        public async Task<ServiceResult<int>> FindCount(StoreStockDTO criteria)
        {
            ServiceResult<int> result = new ServiceResult<int>();

            try
            {
                using (_unitOfWork)
                {
                    int count = await _unitOfWork.StoreStockRepository
                                                 .FindCountAsync(filter: x => (criteria.ProductId == null || x.ProductId == criteria.ProductId) &&
                                                                              (criteria.StoreId == null || x.StoreId == criteria.StoreId));
                    result.TransactionResult = count;
                }
            }
            catch (Exception ex)
            {
                result.IsSucceeded = false;
                result.UserMessage = ex.Message;
            }

            return result;
        }

        public async Task<ServiceResult<IEnumerable<StoreStockDTO>>> GetAll()
        {
            ServiceResult<IEnumerable<StoreStockDTO>> result = new ServiceResult<IEnumerable<StoreStockDTO>>();
            try
            {
                using (_unitOfWork)
                {
                    IEnumerable<Entity.StoreStock> list = await _unitOfWork.StoreStockRepository.GetAllAsync();
                    result.TransactionResult = _mapper.Map<IEnumerable<StoreStockDTO>>(list);
                }
            }
            catch (Exception ex)
            {
                result.IsSucceeded = false;
                result.UserMessage = ex.Message;
            }

            return result;
        }

        public async Task<ServiceResult<StoreStockDTO>> GetById(int id)
        {
            ServiceResult<StoreStockDTO> result = new ServiceResult<StoreStockDTO>();
            try
            {
                using (_unitOfWork)
                {
                    Entity.StoreStock entity = await _unitOfWork.StoreStockRepository.GetByIdAsync(id);
                    result.TransactionResult = _mapper.Map<StoreStockDTO>(entity);
                }
            }
            catch (Exception ex)
            {
                result.IsSucceeded = false;
                result.UserMessage = ex.Message;
            }
            return result;
        }

        public async Task<ServiceResult> RemoveById(int id)
        {
            ServiceResult result = new ServiceResult();
            try
            {
                using (_unitOfWork)
                {
                    await _unitOfWork.StoreStockRepository.RemoveById(id);
                    await _unitOfWork.SaveAsync();
                }
            }
            catch (Exception ex)
            {
                result.IsSucceeded = false;
                result.UserMessage = ex.Message;
            }
            return result;
        }

        public async Task<ServiceResult> Update(StoreStockDTO model)
        {
            ServiceResult result = new ServiceResult();
            try
            {
                using (_unitOfWork)
                {
                    Entity.StoreStock entity = await _unitOfWork.StoreStockRepository.GetByStoreAndProductId(model.ProductId.Value, model.StoreId.Value);
                    if (entity != null)
                    {
                        _mapper.Map(model, entity);
                        _unitOfWork.StoreStockRepository.Update(entity);
                        await _unitOfWork.SaveAsync();
                    }
                }
            }
            catch (Exception ex)
            {
                result.IsSucceeded = false;
                result.UserMessage = ex.Message;
            }
            return result;
        }

        public async Task<ServiceResult<IEnumerable<StoreStockDTO>>> StoreStockReport(StoreStockDTO criteria)
        {
            ServiceResult<IEnumerable<StoreStockDTO>> result = new ServiceResult<IEnumerable<StoreStockDTO>>();

            try
            {
                using (_unitOfWork)
                {
                    IEnumerable<Entity.StoreStock> list = await _unitOfWork
                                                                .StoreStockRepository
                                                                .FindAsync(filter: x => (criteria.ProductId == null || x.ProductId == criteria.ProductId) &&
                                                                                        (criteria.StoreId == null || x.StoreId == criteria.StoreId),
                                                                           includes: new List<string>() { "Product", "Store", "Product.UnitOfMeasure" },
                                                                           orderByDesc: x => x.Stock,
                                                                           skip: criteria.PageNumber,
                                                                           take: criteria.RecordCount);

                    result.TransactionResult = _mapper.Map<IEnumerable<StoreStockDTO>>(list);
                }
            }
            catch (Exception ex)
            {
                result.IsSucceeded = false;
                result.UserMessage = ex.Message;
            }

            return result;
        }
    }
}
