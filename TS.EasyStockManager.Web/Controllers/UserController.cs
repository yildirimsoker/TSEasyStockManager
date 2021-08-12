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
using TS.EasyStockManager.Model.ViewModel.User;

namespace TS.EasyStockManager.Web.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public UserController(IUserService userService,
                              IMapper mapper)
        {
            _userService = userService;
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
        public async Task<IActionResult> Create(CreateUserViewModel model)
        {
            JsonResultModel jsonResultModel = new JsonResultModel();
            try
            {
                UserDTO userDTO = _mapper.Map<UserDTO>(model);
                var serviceResult = await _userService.AddAsync(userDTO);
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
            var serviceResult = await _userService.GetById(id);
            EditUserViewModel model = _mapper.Map<EditUserViewModel>(serviceResult.TransactionResult);
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken()]
        public async Task<IActionResult> Edit(EditUserViewModel model)
        {
            JsonResultModel jsonResultModel = new JsonResultModel();
            try
            {
                UserDTO userDTO = _mapper.Map<UserDTO>(model);
                var serviceResult = await _userService.Update(userDTO);
                jsonResultModel = _mapper.Map<JsonResultModel>(serviceResult);
                if (jsonResultModel.IsSucceeded)
                {
                    jsonResultModel.IsRedirect = true;
                    jsonResultModel.RedirectUrl = "/User";
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
        public async Task<IActionResult> List(SearchUserViewModel model)
        {
            JsonDataTableModel jsonDataTableModel = new JsonDataTableModel();
            try
            {
                UserDTO userDTO = _mapper.Map<UserDTO>(model);
                ServiceResult<int> serviceCountResult = await _userService.FindCount(userDTO);
                ServiceResult<IEnumerable<UserDTO>> serviceListResult = await _userService.Find(userDTO);

                if (serviceCountResult.IsSucceeded && serviceListResult.IsSucceeded)
                {
                    List<ListUserViewModel> listVM = _mapper.Map<List<ListUserViewModel>>(serviceListResult.TransactionResult);
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
                ServiceResult serviceResult = await _userService.RemoveById(id);
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
