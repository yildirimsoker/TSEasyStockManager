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

namespace TS.EasyStockManager.Service.UnitOfMeasure
{
    public class UnitOfMeasureService : BaseService, IUnitOfMeasureService
    {
        public UnitOfMeasureService(IUnitOfWorks unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {
        }

        public async Task<ServiceResult> AddAsync(UnitOfMeasureDTO model)
        {
            ServiceResult result = new ServiceResult();
            try
            {
                using (_unitOfWork)
                {
                    Entity.UnitOfMeasure entity = _mapper.Map<Data.Entity.UnitOfMeasure>(model);
                    entity.CreateDate = DateTime.Now;
                    await _unitOfWork.UnitOfMeasureRepository.AddAsync(entity);
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

        public async Task<ServiceResult<IEnumerable<UnitOfMeasureDTO>>> Find(UnitOfMeasureDTO criteria)
        {
            ServiceResult<IEnumerable<UnitOfMeasureDTO>> result = new ServiceResult<IEnumerable<UnitOfMeasureDTO>>();
            try
            {
                using (_unitOfWork)
                {
                    IEnumerable<Entity.UnitOfMeasure> list = await _unitOfWork
                                                                   .UnitOfMeasureRepository
                                                                   .FindAsync(filter: x => (string.IsNullOrEmpty(criteria.UnitOfMeasureName) || x.UnitOfMeasureName.Contains(criteria.UnitOfMeasureName)) &&
                                                                                           (string.IsNullOrEmpty(criteria.Isocode) || x.Isocode.Contains(criteria.Isocode)),
                                                                           orderByDesc: x => x.Id,
                                                                           skip: criteria.PageNumber,
                                                                           take: criteria.RecordCount);
                    result.TransactionResult = _mapper.Map<IEnumerable<UnitOfMeasureDTO>>(list);
                }
            }
            catch (Exception ex)
            {
                result.IsSucceeded = false;
                result.UserMessage = string.Format(CommonMessages.MSG0002, ex.Message);
            }
            return result;
        }

        public async Task<ServiceResult<int>> FindCount(UnitOfMeasureDTO criteria)
        {
            ServiceResult<int> result = new ServiceResult<int>();
            try
            {
                using (_unitOfWork)
                {
                    int count = await _unitOfWork.UnitOfMeasureRepository
                                                 .FindCountAsync(filter: x => (string.IsNullOrEmpty(criteria.UnitOfMeasureName) || x.UnitOfMeasureName.Contains(criteria.UnitOfMeasureName)) &&
                                                                              (string.IsNullOrEmpty(criteria.Isocode) || x.Isocode.Contains(criteria.Isocode)));
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
        public async Task<ServiceResult<IEnumerable<UnitOfMeasureDTO>>> GetAll()
        {
            ServiceResult<IEnumerable<UnitOfMeasureDTO>> result = new ServiceResult<IEnumerable<UnitOfMeasureDTO>>();
            try
            {
                using (_unitOfWork)
                {
                    IEnumerable<Entity.UnitOfMeasure> list = await _unitOfWork.UnitOfMeasureRepository.GetAllAsync();
                    result.TransactionResult = _mapper.Map<IEnumerable<UnitOfMeasureDTO>>(list);
                }
            }
            catch (Exception ex)
            {
                result.IsSucceeded = false;
                result.UserMessage = string.Format(CommonMessages.MSG0002, ex.Message);
            }

            return result;
        }
        public async Task<ServiceResult<UnitOfMeasureDTO>> GetById(int id)
        {
            ServiceResult<UnitOfMeasureDTO> result = new ServiceResult<UnitOfMeasureDTO>();
            try
            {
                using (_unitOfWork)
                {
                    Entity.UnitOfMeasure entity = await _unitOfWork.UnitOfMeasureRepository.GetByIdAsync(id);
                    result.TransactionResult = _mapper.Map<UnitOfMeasureDTO>(entity);
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
                    await _unitOfWork.UnitOfMeasureRepository.RemoveById(id);
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
        public async Task<ServiceResult> Update(UnitOfMeasureDTO model)
        {
            ServiceResult result = new ServiceResult();
            try
            {
                using (_unitOfWork)
                {
                    Entity.UnitOfMeasure entity = await _unitOfWork.UnitOfMeasureRepository.GetByIdAsync(model.Id.Value);
                    if (entity != null)
                    {
                        _mapper.Map(model, entity);
                        _unitOfWork.UnitOfMeasureRepository.Update(entity);
                        await _unitOfWork.SaveAsync();
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
