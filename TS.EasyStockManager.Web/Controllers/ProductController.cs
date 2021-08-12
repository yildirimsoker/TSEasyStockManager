using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using TS.EasyStockManager.Core.Service;
using TS.EasyStockManager.Model.Domain;
using TS.EasyStockManager.Model.Service;
using TS.EasyStockManager.Model.ViewModel.JsonResult;
using TS.EasyStockManager.Model.ViewModel.Product;

namespace TS.EasyStockManager.Web.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService _productService;
        private readonly IMapper _mapper;
        private readonly ICategoryService _categoryService;
        private readonly IUnitOfMeasureService _unitOfMeasureService;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public ProductController(IProductService productService,
                                 ICategoryService categoryService,
                                 IUnitOfMeasureService unitOfMeasureService,
                                 IWebHostEnvironment webHostEnvironment,
                                 IMapper mapper)
        {
            _productService = productService;
            _categoryService = categoryService;
            _unitOfMeasureService = unitOfMeasureService;
            _webHostEnvironment = webHostEnvironment;
            _mapper = mapper;
        }
        public async Task<IActionResult> Index()
        {
            SearchProductViewModel model = new SearchProductViewModel();
            model.CategoryList = await GetCategoryList();
            model.UnitOfMeasureList = await GetUnitOfMeasureList();
            return View(model);
        }
        public async Task<IActionResult> Create()
        {
            CreateProductViewModel model = new CreateProductViewModel();
            model.CategoryList = await GetCategoryList();
            model.UnitOfMeasureList = await GetUnitOfMeasureList();
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken()]
        public async Task<IActionResult> Create(CreateProductViewModel model)
        {
            JsonResultModel jsonResultModel = new JsonResultModel();
            try
            {
                if (model.ImageFile != null && model.ImageFile.Length > 0)
                {
                    string newFileName = Guid.NewGuid().ToString();
                    string fileExtension = Path.GetExtension(model.ImageFile.FileName);
                    newFileName += fileExtension;
                    var upload = Path.Combine(_webHostEnvironment.WebRootPath, "upload", newFileName);
                    model.ImageFile.CopyTo(new FileStream(upload, FileMode.Create));
                    model.Image = newFileName;
                }

                ProductDTO productDTO = _mapper.Map<ProductDTO>(model);
                var serviceResult = await _productService.AddAsync(productDTO);
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
            EditProductViewModel model = new EditProductViewModel();
            var serviceResult = await _productService.GetById(id);
            model = _mapper.Map<EditProductViewModel>(serviceResult.TransactionResult);
            model.CategoryList = await GetCategoryList();
            model.UnitOfMeasureList = await GetUnitOfMeasureList();

            if (!string.IsNullOrEmpty(model.Image))
                model.ImageDisplayURL = Path.Combine(_webHostEnvironment.WebRootPath, "upload", model.Image);
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken()]
        public async Task<IActionResult> Edit(EditProductViewModel model)
        {
            JsonResultModel jsonResultModel = new JsonResultModel();
            try
            {
                if (model.ImageFile != null && model.ImageFile.Length > 0)
                {
                    string newFileName = Guid.NewGuid().ToString();
                    string fileExtension = Path.GetExtension(model.ImageFile.FileName);
                    newFileName += fileExtension;
                    var upload = Path.Combine(_webHostEnvironment.WebRootPath, "upload", newFileName);
                    model.ImageFile.CopyTo(new FileStream(upload, FileMode.Create));
                    model.Image = newFileName;

                }
                ProductDTO productDTO = _mapper.Map<ProductDTO>(model);
                var serviceResult = await _productService.Update(productDTO);
                jsonResultModel = _mapper.Map<JsonResultModel>(serviceResult);

                if (jsonResultModel.IsSucceeded)
                {
                    jsonResultModel.IsRedirect = true;
                    jsonResultModel.RedirectUrl = "/Product";
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
        public async Task<IActionResult> List(SearchProductViewModel model)
        {
            JsonDataTableModel jsonDataTableModel = new JsonDataTableModel();
            try
            {
                ProductDTO productDTO = _mapper.Map<ProductDTO>(model);
                ServiceResult<int> serviceCountResult = await _productService.FindCount(productDTO);
                ServiceResult<IEnumerable<ProductDTO>> serviceListResult = await _productService.Find(productDTO);

                if (serviceCountResult.IsSucceeded && serviceListResult.IsSucceeded)
                {
                    List<ListProductViewModel> listVM = _mapper.Map<List<ListProductViewModel>>(serviceListResult.TransactionResult);
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
        public async Task<IActionResult> DeleteImage(int id)
        {
            JsonResultModel jsonResultModel = new JsonResultModel();
            try
            {
                ServiceResult serviceResult = await _productService.DeleteProductImage(id);
                jsonResultModel = _mapper.Map<JsonResultModel>(serviceResult);
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
                ServiceResult serviceResult = await _productService.RemoveById(id);
                jsonResultModel = _mapper.Map<JsonResultModel>(serviceResult);
            }
            catch (Exception ex)
            {
                jsonResultModel.IsSucceeded = false;
                jsonResultModel.UserMessage = ex.Message;
            }
            return Json(jsonResultModel);
        }
        private async Task<IEnumerable<SelectListItem>> GetCategoryList()
        {
            ServiceResult<IEnumerable<CategoryDTO>> serviceResult = await _categoryService.GetAll();
            IEnumerable<SelectListItem> drpCategoryList = _mapper.Map<IEnumerable<SelectListItem>>(serviceResult.TransactionResult);
            return drpCategoryList;
        }
        private async Task<IEnumerable<SelectListItem>> GetUnitOfMeasureList()
        {
            ServiceResult<IEnumerable<UnitOfMeasureDTO>> serviceResult = await _unitOfMeasureService.GetAll();
            IEnumerable<SelectListItem> drpUnitOfMeasureList = _mapper.Map<IEnumerable<SelectListItem>>(serviceResult.TransactionResult);
            return drpUnitOfMeasureList;
        }


    }
}
