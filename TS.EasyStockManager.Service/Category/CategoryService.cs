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

namespace TS.EasyStockManager.Service.Category
{
    public class CategoryService : BaseService, ICategoryService
    {
        public CategoryService(IUnitOfWorks unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {
        }
        public async Task<ServiceResult> AddAsync(CategoryDTO model)
        {
            ServiceResult result = new ServiceResult();
            try
            {
                using (_unitOfWork)
                {
                    Entity.Category entity = _mapper.Map<Data.Entity.Category>(model);
                    entity.CreateDate = DateTime.Now;
                    await _unitOfWork.CategoryRepository.AddAsync(entity);
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
        public async Task<ServiceResult<IEnumerable<CategoryDTO>>> Find(CategoryDTO criteria)
        {
            ServiceResult<IEnumerable<CategoryDTO>> result = new ServiceResult<IEnumerable<CategoryDTO>>();

            try
            {
                using (_unitOfWork)
                {
                    IEnumerable<Entity.Category> list = await _unitOfWork
                                                                .CategoryRepository
                                                                .FindAsync(filter: x => (string.IsNullOrEmpty(criteria.CategoryName) || x.CategoryName.Contains(criteria.CategoryName)),
                                                                           orderByDesc: x => x.Id,
                                                                           skip: criteria.PageNumber,
                                                                           take: criteria.RecordCount);

                    result.TransactionResult = _mapper.Map<IEnumerable<CategoryDTO>>(list);
                }
            }
            catch (Exception ex)
            {
                result.IsSucceeded = false;
                result.UserMessage = string.Format(CommonMessages.MSG0002, ex.Message);
            }

            return result;
        }
        public async Task<ServiceResult<int>> FindCount(CategoryDTO criteria)
        {
            ServiceResult<int> result = new ServiceResult<int>();

            try
            {
                using (_unitOfWork)
                {
                    int count = await _unitOfWork.CategoryRepository.FindCountAsync(filter: x => (string.IsNullOrEmpty(criteria.CategoryName) || x.CategoryName.Contains(criteria.CategoryName)));
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
        public async Task<ServiceResult<IEnumerable<CategoryDTO>>> GetAll()
        {
            ServiceResult<IEnumerable<CategoryDTO>> result = new ServiceResult<IEnumerable<CategoryDTO>>();
            try
            {
                using (_unitOfWork)
                {
                    IEnumerable<Entity.Category> list = await _unitOfWork.CategoryRepository.GetAllAsync();
                    result.TransactionResult = _mapper.Map<IEnumerable<CategoryDTO>>(list);
                }
            }
            catch (Exception ex)
            {
                result.IsSucceeded = false;
                result.UserMessage = string.Format(CommonMessages.MSG0002, ex.Message);
            }

            return result;
        }
        public async Task<ServiceResult<CategoryDTO>> GetById(int id)
        {
            ServiceResult<CategoryDTO> result = new ServiceResult<CategoryDTO>();
            try
            {
                using (_unitOfWork)
                {
                    Entity.Category entity = await _unitOfWork.CategoryRepository.GetByIdAsync(id);
                    result.TransactionResult = _mapper.Map<CategoryDTO>(entity);
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
                    await _unitOfWork.CategoryRepository.RemoveById(id);
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
        public async Task<ServiceResult> Update(CategoryDTO model)
        {
            ServiceResult result = new ServiceResult();
            try
            {
                using (_unitOfWork)
                {
                    Entity.Category entity = await _unitOfWork.CategoryRepository.GetByIdAsync(model.Id.Value);
                    if (entity != null)
                    {
                        _mapper.Map(model, entity);
                        _unitOfWork.CategoryRepository.Update(entity);
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
