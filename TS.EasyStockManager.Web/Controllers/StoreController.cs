using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TS.EasyStockManager.Core.Service;
using TS.EasyStockManager.Model.Domain;
using TS.EasyStockManager.Model.Service;
using TS.EasyStockManager.Model.ViewModel.JsonResult;
using TS.EasyStockManager.Model.ViewModel.Store;

namespace TS.EasyStockManager.Web.Controllers
{
    public class StoreController : Controller
    {
        private readonly IStoreService _storeService;
        private readonly IMapper _mapper;

        public StoreController(IStoreService storeService,
                              IMapper mapper)
        {
            _storeService = storeService;
            _mapper = mapper;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken()]
        public async Task<IActionResult> Create(CreateStoreViewModel model)
        {
            JsonResultModel jsonResultModel = new JsonResultModel();
            try
            {
                StoreDTO storeDTO = _mapper.Map<StoreDTO>(model);
                var serviceResult = await _storeService.AddAsync(storeDTO);
                jsonResultModel = _mapper.Map<JsonResultModel>(serviceResult);
            }
            catch (Exception ex)
            {
                jsonResultModel.IsSucceeded = false;
                jsonResultModel.UserMessage = ex.Message;
            }
            return Json(jsonResultModel);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var serviceResult = await _storeService.GetById(id);
            EditStoreViewModel model = _mapper.Map<EditStoreViewModel>(serviceResult.TransactionResult);
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken()]
        public async Task<IActionResult> Edit(EditStoreViewModel model)
        {
            JsonResultModel jsonResultModel = new JsonResultModel();
            try
            {
                StoreDTO StoreDTO = _mapper.Map<StoreDTO>(model);
                var serviceResult = await _storeService.Update(StoreDTO);
                jsonResultModel = _mapper.Map<JsonResultModel>(serviceResult);
                if (jsonResultModel.IsSucceeded)
                {
                    jsonResultModel.IsRedirect = true;
                    jsonResultModel.RedirectUrl = "/Store";
                }
            }
            catch (Exception ex)
            {
                jsonResultModel.IsSucceeded = false;
                jsonResultModel.UserMessage = ex.Message;
            }
            return Json(jsonResultModel);
        }

        [HttpGet]
        public async Task<IActionResult> List(SearchStoreViewModel model)
        {
            JsonDataTableModel jsonDataTableModel = new JsonDataTableModel();
            try
            {
                StoreDTO storeDTO = _mapper.Map<StoreDTO>(model);
                ServiceResult<int> serviceCountResult = await _storeService.FindCount(storeDTO);
                ServiceResult<IEnumerable<StoreDTO>> serviceListResult = await _storeService.Find(storeDTO);

                if (serviceCountResult.IsSucceeded && serviceListResult.IsSucceeded)
                {
                    List<ListStoreViewModel> listVM = _mapper.Map<List<ListStoreViewModel>>(serviceListResult.TransactionResult);
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

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            JsonResultModel jsonResultModel = new JsonResultModel();
            try
            {
                ServiceResult serviceResult = await _storeService.RemoveById(id);
                jsonResultModel = _mapper.Map<JsonResultModel>(serviceResult);
            }
            catch (Exception ex)
            {
                jsonResultModel.IsSucceeded = false;
                jsonResultModel.UserMessage = ex.Message;
            }
            return Json(jsonResultModel);
        }
    }
}
