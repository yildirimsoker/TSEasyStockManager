using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using TS.EasyStockManager.Common.Enums;
using TS.EasyStockManager.Core.Service;
using TS.EasyStockManager.Model.Domain;
using TS.EasyStockManager.Model.Service;
using TS.EasyStockManager.Model.ViewModel.JsonResult;
using TS.EasyStockManager.Model.ViewModel.Transaction;

namespace TS.EasyStockManager.Web.Controllers
{
    public class TransactionController : Controller
    {
        private readonly IStoreService _storeService;
        private readonly IProductService _productService;
        private readonly ITransactionService _transactionService;
        private readonly IMapper _mapper;

        public TransactionController(IStoreService storeService,
                                     IProductService productService,
                                     ITransactionService transactionService,
                                     IMapper mapper)
        {
            _storeService = storeService;
            _productService = productService;
            _transactionService = transactionService;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            SearchTransactionViewModel model = new SearchTransactionViewModel();
            model.StoreList = await GetStoreList();
            model.ToStoreList = model.StoreList;
            return View(model);
        }

        public async Task<IActionResult> Create(int typeId)
        {
            CreateTransactionViewModel model = new CreateTransactionViewModel();
            model.TransactionTypeId = typeId;
            model.PageName = GetPageName(typeId);
            model.StoreList = await GetStoreList();
            if (typeId == (int)TransactionType.Transfer)
                model.ToStoreList = model.StoreList;
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken()]
        public async Task<IActionResult> Create(CreateTransactionViewModel model)
        {
            JsonResultModel jsonResultModel = new JsonResultModel();
            try
            {
                TransactionDTO transactionDTO = _mapper.Map<TransactionDTO>(model);
                var serviceResult = await _transactionService.AddAsync(transactionDTO);
                jsonResultModel = _mapper.Map<JsonResultModel>(serviceResult);
            }
            catch (Exception ex)
            {
                jsonResultModel.IsSucceeded = false;
                jsonResultModel.UserMessage = ex.Message;
            }
            return Json(jsonResultModel);
        }


        public async Task<IActionResult> Edit(int id, int typeId)
        {
            EditTransactionViewModel model = new EditTransactionViewModel();
            var serviceResult = await _transactionService.GetWithDetailAndProductById(id);
            model = _mapper.Map<EditTransactionViewModel>(serviceResult.TransactionResult);
            model.StoreList = await GetStoreList();
            model.PageName = GetPageName(typeId);
            if (typeId == (int)TransactionType.Transfer)
                model.ToStoreList = model.StoreList;
            model.TransactionDetailCount = model.TransactionDetail.Count();
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken()]
        public async Task<IActionResult> Edit(EditTransactionViewModel model)
        {
            JsonResultModel jsonResultModel = new JsonResultModel();
            try
            {
                TransactionDTO transactionDTO = _mapper.Map<TransactionDTO>(model);
                var serviceResult = await _transactionService.Update(transactionDTO);
                jsonResultModel = _mapper.Map<JsonResultModel>(serviceResult);
                if (jsonResultModel.IsSucceeded)
                {
                    jsonResultModel.IsRedirect = true;
                    jsonResultModel.RedirectUrl = "/Transaction"; 
                }
            }
            catch (Exception ex)
            {
                jsonResultModel.IsSucceeded = false;
                jsonResultModel.UserMessage = ex.Message;
            }
            return Json(jsonResultModel);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            JsonResultModel jsonResultModel = new JsonResultModel();
            try
            {
                ServiceResult serviceResult = await _transactionService.RemoveById(id);
                jsonResultModel = _mapper.Map<JsonResultModel>(serviceResult);
            }
            catch (Exception ex)
            {
                jsonResultModel.IsSucceeded = false;
                jsonResultModel.UserMessage = ex.Message;
            }
            return Json(jsonResultModel);
        }

        [HttpGet]
        public async Task<IActionResult> List(SearchTransactionViewModel model)
        {
            JsonDataTableModel jsonDataTableModel = new JsonDataTableModel();
            try
            {
                TransactionDTO transactionDTO = _mapper.Map<TransactionDTO>(model);
                ServiceResult<int> serviceCountResult = await _transactionService.FindCount(transactionDTO);
                ServiceResult<IEnumerable<TransactionDTO>> serviceListResult = await _transactionService.Find(transactionDTO);

                if (serviceCountResult.IsSucceeded && serviceListResult.IsSucceeded)
                {
                    List<ListTransactionViewModel> listVM = _mapper.Map<List<ListTransactionViewModel>>(serviceListResult.TransactionResult);
                    jsonDataTableModel.aaData = listVM;
                    jsonDataTableModel.iTotalDisplayRecords = serviceCountResult.TransactionResult;
                    jsonDataTableModel.iTotalRecords = serviceCountResult.TransactionResult;
                }
                else
                {
                    jsonDataTableModel.IsSucceeded = false;
                    jsonDataTableModel.UserMessage = serviceCountResult.UserMessage + "-" + serviceListResult.UserMessage;
                }
            }
            catch (Exception ex)
            {
                jsonDataTableModel.IsSucceeded = false;
                jsonDataTableModel.UserMessage = ex.Message;
            }

            return Json(jsonDataTableModel);
        }

        [HttpGet]
        public async Task<IActionResult> GetTransactionDetail(int id)
        {
            JsonResultModel jsonResultModel = new JsonResultModel();
            try
            {
                var serviceResult = await _transactionService.GetTransactionDetailByTransactionId(id);
                if (serviceResult.IsSucceeded)
                {
                    IEnumerable<TransactionDetailViewModel> transactionDetailViewModel = _mapper.Map<IEnumerable<TransactionDetailViewModel>>(serviceResult.TransactionResult);
                    jsonResultModel.Data = transactionDetailViewModel;
                }
            }
            catch (Exception ex)
            {
                jsonResultModel.IsSucceeded = false;
                jsonResultModel.UserMessage = ex.Message;
            }

            return Json(jsonResultModel);
        }

        private async Task<IEnumerable<SelectListItem>> GetStoreList()
        {
            ServiceResult<IEnumerable<StoreDTO>> serviceResult = await _storeService.GetAll();
            IEnumerable<SelectListItem> drpStoreList = _mapper.Map<IEnumerable<SelectListItem>>(serviceResult.TransactionResult);
            return drpStoreList;
        }

        [HttpGet]
        public async Task<IActionResult> GetProduct(string search)
        {
            ServiceResult<IEnumerable<ProductDTO>> serviceResult = await _productService.GetProductsByBarcodeAndName(search);
            IEnumerable<SelectListItem> drpProductList = _mapper.Map<IEnumerable<SelectListItem>>(serviceResult.TransactionResult);
            return Json(drpProductList);
        }
        private string GetPageName(int transactionTypeId)
        {
            if ((int)TransactionType.Transfer == transactionTypeId)
                return "Transfer";
            else if ((int)TransactionType.StockIn == transactionTypeId)
                return "Stock Receipt";
            else
                return "Stock Out";
        }
    }
}
