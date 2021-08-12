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

namespace TS.EasyStockManager.Service.Product
{
    public class ProductService : BaseService, IProductService
    {
        public ProductService(IUnitOfWorks unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {
        }

        public async Task<ServiceResult> AddAsync(ProductDTO model)
        {
            ServiceResult result = new ServiceResult();
            try
            {
                using (_unitOfWork)
                {
                    Entity.Product entity = _mapper.Map<Data.Entity.Product>(model);
                    entity.CreateDate = DateTime.Now;
                    await _unitOfWork.ProductRepository.AddAsync(entity);
                    await _unitOfWork.SaveAsync();
                    result.Id = entity.Id;
                    result.UserMessage = CommonMessages.MSG0001;
                }
            }
            catch (Exception ex)
            {
                result.IsSucceeded = false;
                result.UserMessage = ex.Message;
            }
            return result;
        }

        public async Task<ServiceResult<IEnumerable<ProductDTO>>> Find(ProductDTO criteria)
        {
            ServiceResult<IEnumerable<ProductDTO>> result = new ServiceResult<IEnumerable<ProductDTO>>();

            try
            {
                using (_unitOfWork)
                {
                    IEnumerable<Entity.Product> list = await _unitOfWork
                                                                .ProductRepository
                                                                .FindAsync(filter: x => (string.IsNullOrEmpty(criteria.ProductName) || x.ProductName.Contains(criteria.ProductName)) &&
                                                                                        (string.IsNullOrEmpty(criteria.Barcode) || x.Barcode.Contains(criteria.Barcode)) &&
                                                                                        (criteria.CategoryId == null || x.CategoryId == criteria.CategoryId) &&
                                                                                        (criteria.UnitOfMeasureId == null || x.UnitOfMeasureId == criteria.UnitOfMeasureId),
                                                                           includes: new List<string>() { "UnitOfMeasure", "Category" },
                                                                           orderByDesc: x => x.Id,
                                                                           skip: criteria.PageNumber,
                                                                           take: criteria.RecordCount);

                    result.TransactionResult = _mapper.Map<IEnumerable<ProductDTO>>(list);
                }
            }
            catch (Exception ex)
            {
                result.IsSucceeded = false;
                result.UserMessage = string.Format(CommonMessages.MSG0002, ex.Message);
            }

            return result;
        }

        public async Task<ServiceResult<int>> FindCount(ProductDTO criteria)
        {
            ServiceResult<int> result = new ServiceResult<int>();

            try
            {
                using (_unitOfWork)
                {
                    int count = await _unitOfWork.ProductRepository
                                                 .FindCountAsync(filter: x => (string.IsNullOrEmpty(criteria.ProductName) || x.ProductName.Contains(criteria.ProductName)) &&
                                                                              (string.IsNullOrEmpty(criteria.Barcode) || x.Barcode.Contains(criteria.Barcode)) &&
                                                                              (criteria.CategoryId == null || x.CategoryId == criteria.CategoryId) &&
                                                                              (criteria.UnitOfMeasureId == null || x.UnitOfMeasureId == criteria.UnitOfMeasureId));
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

        public async Task<ServiceResult<IEnumerable<ProductDTO>>> GetAll()
        {
            ServiceResult<IEnumerable<ProductDTO>> result = new ServiceResult<IEnumerable<ProductDTO>>();
            try
            {
                using (_unitOfWork)
                {
                    IEnumerable<Entity.Product> list = await _unitOfWork.ProductRepository.GetAllAsync();
                    result.TransactionResult = _mapper.Map<IEnumerable<ProductDTO>>(list);
                }
            }
            catch (Exception ex)
            {
                result.IsSucceeded = false;
                result.UserMessage = string.Format(CommonMessages.MSG0002, ex.Message);
            }

            return result;
        }

        public async Task<ServiceResult<IEnumerable<ProductDTO>>> GetProductsByBarcodeAndName(string term)
        {
            ServiceResult<IEnumerable<ProductDTO>> result = new ServiceResult<IEnumerable<ProductDTO>>();
            try
            {
                using (_unitOfWork)
                {
                    IEnumerable<Entity.Product> list = await _unitOfWork.ProductRepository.GetProductsByBarcodeAndName(term);
                    result.TransactionResult = _mapper.Map<IEnumerable<ProductDTO>>(list);
                }
            }
            catch (Exception ex)
            {
                result.IsSucceeded = false;
                result.UserMessage = string.Format(CommonMessages.MSG0002, ex.Message);
            }

            return result;
        }

        public async Task<ServiceResult<ProductDTO>> GetById(int id)
        {
            ServiceResult<ProductDTO> result = new ServiceResult<ProductDTO>();
            try
            {
                using (_unitOfWork)
                {
                    Entity.Product entity = await _unitOfWork.ProductRepository.GetByIdAsync(id);
                    result.TransactionResult = _mapper.Map<ProductDTO>(entity);
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
                    await _unitOfWork.ProductRepository.RemoveById(id);
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

        public async Task<ServiceResult> Update(ProductDTO model)
        {
            ServiceResult result = new ServiceResult();
            try
            {
                using (_unitOfWork)
                {
                    Entity.Product entity = await _unitOfWork.ProductRepository.GetByIdAsync(model.Id.Value);
                    if (entity != null)
                    {
                        _mapper.Map(model, entity);
                        _unitOfWork.ProductRepository.Update(entity);
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

        public async Task<ServiceResult> DeleteProductImage(int id)
        {
            ServiceResult result = new ServiceResult();
            try
            {
                using (_unitOfWork)
                {
                    await _unitOfWork.ProductRepository.DeleteProductImage(id);
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
    }
}
