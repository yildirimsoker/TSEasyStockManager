using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TS.EasyStockManager.Common.Message;
using TS.EasyStockManager.Core.Service;
using TS.EasyStockManager.Core.UnitOfWorks;
using TS.EasyStockManager.Model.Domain;
using TS.EasyStockManager.Model.Service;
using TS.EasyStockManager.Service.Base;
using Entity = TS.EasyStockManager.Data.Entity;

namespace TS.EasyStockManager.Service.Store
{
    public class StoreService : BaseService, IStoreService
    {
        public StoreService(IUnitOfWorks unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {
        }


        public async Task<ServiceResult> AddAsync(StoreDTO model)
        {
            ServiceResult result = new ServiceResult();
            try
            {
                using (_unitOfWork)
                {
                    Entity.Store entity = _mapper.Map<Data.Entity.Store>(model);
                    entity.CreateDate = DateTime.Now;
                    await _unitOfWork.StoreRepository.AddAsync(entity);
                    await _unitOfWork.SaveAsync();
                    result.Id = entity.Id;
                    result.UserMessage = CommonMessages.MSG0001;
                }
            }
            catch (Exception ex)
            {
                result.IsSucceeded = false;
                result.UserMessage = string.Format(CommonMessages.MSG0002, ex.Message);
            }
            return result;
        }

        public async Task<ServiceResult<IEnumerable<StoreDTO>>> Find(StoreDTO criteria)
        {
            ServiceResult<IEnumerable<StoreDTO>> result = new ServiceResult<IEnumerable<StoreDTO>>();

            try
            {
                using (_unitOfWork)
                {
                    IEnumerable<Entity.Store> list = await _unitOfWork
                                                                .StoreRepository
                                                                .FindAsync(filter: x => (string.IsNullOrEmpty(criteria.StoreName) || x.StoreName.Contains(criteria.StoreName)) &&
                                                                                        (string.IsNullOrEmpty(criteria.StoreCode) || x.StoreCode.Contains(criteria.StoreCode)),
                                                                           orderByDesc: x => x.Id,
                                                                           skip: criteria.PageNumber,
                                                                           take: criteria.RecordCount);

                    result.TransactionResult = _mapper.Map<IEnumerable<StoreDTO>>(list);
                }
            }
            catch (Exception ex)
            {
                result.IsSucceeded = false;
                result.UserMessage = string.Format(CommonMessages.MSG0002, ex.Message);
            }

            return result;
        }

        public async Task<ServiceResult<int>> FindCount(StoreDTO criteria)
        {
            ServiceResult<int> result = new ServiceResult<int>();

            try
            {
                using (_unitOfWork)
                {
                    int count = await _unitOfWork.StoreRepository
                                                 .FindCountAsync(filter: x => (string.IsNullOrEmpty(criteria.StoreName) || x.StoreName.Contains(criteria.StoreName)) &&
                                                                              (string.IsNullOrEmpty(criteria.StoreCode) || x.StoreCode.Contains(criteria.StoreCode)));

                    result.TransactionResult = count;
                }
            }
            catch (Exception ex)
            {
                result.IsSucceeded = false;
                result.UserMessage = string.Format(CommonMessages.MSG0002, ex.Message);
            }

            return result;
        }

        public async Task<ServiceResult<IEnumerable<StoreDTO>>> GetAll()
        {
            ServiceResult<IEnumerable<StoreDTO>> result = new ServiceResult<IEnumerable<StoreDTO>>();
            try
            {
                using (_unitOfWork)
                {
                    IEnumerable<Entity.Store> list = await _unitOfWork.StoreRepository.GetAllAsync();
                    result.TransactionResult = _mapper.Map<IEnumerable<StoreDTO>>(list);
                }
            }
            catch (Exception ex)
            {
                result.IsSucceeded = false;
                result.UserMessage = string.Format(CommonMessages.MSG0002, ex.Message);
            }

            return result;
        }

        public async Task<ServiceResult<StoreDTO>> GetById(int id)
        {
            ServiceResult<StoreDTO> result = new ServiceResult<StoreDTO>();
            try
            {
                using (_unitOfWork)
                {
                    Entity.Store entity = await _unitOfWork.StoreRepository.GetByIdAsync(id);
                    result.TransactionResult = _mapper.Map<StoreDTO>(entity);
                }
            }
            catch (Exception ex)
            {
                result.IsSucceeded = false;
                result.UserMessage = string.Format(CommonMessages.MSG0002, ex.Message);
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
                    await _unitOfWork.StoreRepository.RemoveById(id);
                    await _unitOfWork.SaveAsync();
                    result.UserMessage = CommonMessages.MSG0001;
                }
            }
            catch (Exception ex)
            {
                result.IsSucceeded = false;
                result.UserMessage = string.Format(CommonMessages.MSG0002, ex.Message);
            }
            return result;
        }

        public async Task<ServiceResult> Update(StoreDTO model)
        {
            ServiceResult result = new ServiceResult();
            try
            {
                using (_unitOfWork)
                {
                    Entity.Store entity = await _unitOfWork.StoreRepository.GetByIdAsync(model.Id.Value);
                    if (entity != null)
                    {
                        _mapper.Map(model, entity);
                        _unitOfWork.StoreRepository.Update(entity);
                        await _unitOfWork.SaveAsync();
                        result.UserMessage = CommonMessages.MSG0001;
                    }
                }
            }
            catch (Exception ex)
            {
                result.IsSucceeded = false;
                result.UserMessage = string.Format(CommonMessages.MSG0002, ex.Message);
            }
            return result;
        }
    }
}
