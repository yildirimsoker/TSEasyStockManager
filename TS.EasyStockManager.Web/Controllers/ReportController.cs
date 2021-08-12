using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using TS.EasyStockManager.Core.Service;
using TS.EasyStockManager.Model.Domain;
using TS.EasyStockManager.Model.Service;
using TS.EasyStockManager.Model.ViewModel.JsonResult;
using TS.EasyStockManager.Model.ViewModel.Report.StoreStock;
using TS.EasyStockManager.Model.ViewModel.Report.TransactionDetail;

namespace TS.EasyStockManager.Web.Controllers
{
    public class ReportController : Controller
    {
        private readonly IStoreStockService _storeStockService;
        private readonly ITransactionService _transactionService;
        private readonly IProductService _productService;
        private readonly IStoreService _storeService;
        private readonly IMapper _mapper;

        public ReportController(IStoreStockService storeStockService,
                                IProductService productService,
                                IStoreService storeService,
                                IMapper mapper,
                                ITransactionService transactionService)
        {
            _storeStockService = storeStockService;
            _productService = productService;
            _storeService = storeService;
            _transactionService = transactionService;
            _mapper = mapper;
        }
        public async Task<IActionResult> StoreStockReport()
        {
            SearchStoreStockReportViewModel model = new SearchStoreStockReportViewModel();
            model.StoreList = await GetStoreList();
            model.ProductList = new List<SelectListItem>();
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> StoreStockList(SearchStoreStockReportViewModel model)
        {
            JsonDataTableModel jsonDataTableModel = new JsonDataTableModel();
            try
            {
                StoreStockDTO storeStockDTO = _mapper.Map<StoreStockDTO>(model);
                ServiceResult<int> serviceCountResult = await _storeStockService.FindCount(storeStockDTO);
                ServiceResult<IEnumerable<StoreStockDTO>> serviceListResult = await _storeStockService.StoreStockReport(storeStockDTO);

                if (serviceCountResult.IsSucceeded && serviceListResult.IsSucceeded)
                {
                    List<ListStoreStockReportViewModel> listVM = _mapper.Map<List<ListStoreStockReportViewModel>>(serviceListResult.TransactionResult);
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

        public async Task<IActionResult> TransactionDetailReport()
        {
            SearchTransactionDetailReportViewModel model = new SearchTransactionDetailReportViewModel();
            model.StoreList = await GetStoreList();
            model.ToStoreList = model.StoreList;
            model.ProductList = new List<SelectListItem>();
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> TransactionDetailList(SearchTransactionDetailReportViewModel model)
        {
            JsonDataTableModel jsonDataTableModel = new JsonDataTableModel();
            try
            {
                TransactionDetailReportDTO transactionDetailReportDTO = _mapper.Map<TransactionDetailReportDTO>(model);
                ServiceResult<int> serviceCountResult = await _transactionService.TransactionDetailReportCount(transactionDetailReportDTO);
                ServiceResult<IEnumerable<TransactionDetailReportDTO>> serviceListResult = await _transactionService.TransactionDetailReport(transactionDetailReportDTO);

                if (serviceCountResult.IsSucceeded && serviceListResult.IsSucceeded)
                {
                    List<ListTransactionDetailReportViewModel> listVM = _mapper.Map<List<ListTransactionDetailReportViewModel>>(serviceListResult.TransactionResult);
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
    }
}
