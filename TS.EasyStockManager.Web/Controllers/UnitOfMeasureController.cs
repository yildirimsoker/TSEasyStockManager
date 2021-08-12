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
using TS.EasyStockManager.Model.ViewModel.UnitOfMeasure;

namespace TS.EasyStockManager.Web.Controllers
{
    public class UnitOfMeasureController : Controller
    {
        private readonly IUnitOfMeasureService _unitOfMeasureService;
        private readonly IMapper _mapper;

        public UnitOfMeasureController(IUnitOfMeasureService unitOfMeasureService,
                                       IMapper mapper)
        {
            _unitOfMeasureService = unitOfMeasureService;
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
        public async Task<IActionResult> Create(CreateUnitOfMeasureViewModel model)
        {
            JsonResultModel jsonResultModel = new JsonResultModel();
            try
            {
                UnitOfMeasureDTO unitOfMeasureDTO = _mapper.Map<UnitOfMeasureDTO>(model);
                var serviceResult = await _unitOfMeasureService.AddAsync(unitOfMeasureDTO);
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
            var serviceResult = await _unitOfMeasureService.GetById(id);
            EditUnitOfMeasureViewModel model = _mapper.Map<EditUnitOfMeasureViewModel>(serviceResult.TransactionResult);
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken()]
        public async Task<IActionResult> Edit(EditUnitOfMeasureViewModel model)
        {
            JsonResultModel jsonResultModel = new JsonResultModel();
            try
            {
                UnitOfMeasureDTO unitOfMeasureDTO = _mapper.Map<UnitOfMeasureDTO>(model);
                var serviceResult = await _unitOfMeasureService.Update(unitOfMeasureDTO);
                jsonResultModel = _mapper.Map<JsonResultModel>(serviceResult);
                if (jsonResultModel.IsSucceeded)
                {
                    jsonResultModel.IsRedirect = true;
                    jsonResultModel.RedirectUrl = "/UnitOfMeasure";
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
        public async Task<IActionResult> List(SearchUnitOfMeasureViewModel model)
        {
            JsonDataTableModel jsonDataTableModel = new JsonDataTableModel();
            try
            {
                UnitOfMeasureDTO unitOfMeasureDTO = _mapper.Map<UnitOfMeasureDTO>(model);
                ServiceResult<int> serviceCountResult = await _unitOfMeasureService.FindCount(unitOfMeasureDTO);
                ServiceResult<IEnumerable<UnitOfMeasureDTO>> serviceListResult = await _unitOfMeasureService.Find(unitOfMeasureDTO);

                if (serviceCountResult.IsSucceeded && serviceListResult.IsSucceeded)
                {
                    List<ListUnitOfMeasureViewModel> listVM = _mapper.Map<List<ListUnitOfMeasureViewModel>>(serviceListResult.TransactionResult);
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
                ServiceResult serviceResult = await _unitOfMeasureService.RemoveById(id);
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
