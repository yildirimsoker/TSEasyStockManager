using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TS.EasyStockManager.Core.Service;
using TS.EasyStockManager.Model.Domain;
using TS.EasyStockManager.Model.Service;
using TS.EasyStockManager.Model.ViewModel.Category;
using TS.EasyStockManager.Model.ViewModel.JsonResult;

namespace TS.EasyStockManager.Web.Controllers
{
    public class CategoryController : Controller
    {

        private readonly ICategoryService _categoryService;
        private readonly IMapper _mapper;

        public CategoryController(ICategoryService categoryService,
                                  IMapper mapper)
        {
            _categoryService = categoryService;
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
        public async Task<IActionResult> Create(CreateCategoryViewModel model)
        {
            JsonResultModel jsonResultModel = new JsonResultModel();
            try
            {
                CategoryDTO categoryDTO = _mapper.Map<CategoryDTO>(model);
                var serviceResult = await _categoryService.AddAsync(categoryDTO);
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
            var serviceResult = await _categoryService.GetById(id);
            EditCategoryViewModel model = _mapper.Map<EditCategoryViewModel>(serviceResult.TransactionResult);
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken()]
        public async Task<IActionResult> Edit(EditCategoryViewModel model)
        {
            JsonResultModel jsonResultModel = new JsonResultModel();
            try
            {
                CategoryDTO categoryDTO = _mapper.Map<CategoryDTO>(model);
                var serviceResult = await _categoryService.Update(categoryDTO);
                jsonResultModel = _mapper.Map<JsonResultModel>(serviceResult);
                if (jsonResultModel.IsSucceeded)
                {
                    jsonResultModel.IsRedirect = true;
                    jsonResultModel.RedirectUrl = "/Category";
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
        public async Task<IActionResult> List(SearchCategoryViewModel model)
        {
            JsonDataTableModel jsonDataTableModel = new JsonDataTableModel();
            try
            {
                CategoryDTO categoryDTO = _mapper.Map<CategoryDTO>(model);
                ServiceResult<int> serviceCountResult = await _categoryService.FindCount(categoryDTO);
                ServiceResult<IEnumerable<CategoryDTO>> serviceListResult = await _categoryService.Find(categoryDTO);

                if (serviceCountResult.IsSucceeded && serviceListResult.IsSucceeded)
                {
                    List<ListCategoryViewModel> listVM = _mapper.Map<List<ListCategoryViewModel>>(serviceListResult.TransactionResult);
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
                ServiceResult serviceResult = await _categoryService.RemoveById(id);
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
