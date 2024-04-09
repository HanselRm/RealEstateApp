﻿using Microsoft.AspNetCore.Mvc;
using RealStateAppProg3.Core.Application.Helpers;
using RealStateAppProg3.Core.Application.Interfaces.Service;
using RealStateAppProg3.Core.Application.ViewModels.Users;

namespace RealStateAppProg3.Presentation.WebApp.Controllers
{
    public class AgentController : Controller
    {
        private readonly IUserService _userService;
        private readonly IHttpContextAccessor _contextAccessor;

        public AgentController(IUserService userService, IHttpContextAccessor contextAccessor)
        {
            _userService = userService;
            _contextAccessor = contextAccessor;
        }
        public IActionResult Index()
        {
            return View();
        }
        //mi perfil
        public async Task<IActionResult> MyProfile(string Id)
        {
            SaveUserViewModel vm = await _userService.GetByIdWithoutRol(Id);
            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> MyProfile(SaveUserViewModel vm)
        {
            if (ModelState["Name"].Errors.Any() || ModelState["LastName"].Errors.Any() || ModelState["Identification"].Errors.Any()
                || ModelState["PhoneNumber"].Errors.Any() || ModelState["file"].Errors.Any() || ModelState["PhoneNumber"].Errors.Any()
                || ModelState["Username"].Errors.Any() || ModelState["Email"].Errors.Any() || ModelState["Password"].Errors.Any()
                || ModelState["ConfirmPassword"].Errors.Any() || ModelState["TypeUser"].Errors.Any())
            {
                return View(vm);
            }
            var origin = Request.Headers["origin"];
            vm.PhotoProfileUrl = UploadFiles.UploadFile(vm.file, "User", vm.Id);

            SaveUserViewModel response = await _userService.RegisterAsync(vm, origin);

            if (response != null && response.HasError != true)
            {
                return RedirectToRoute(new { controller = "User", action = "Index" });
            }
            else
            {
                vm.HasError = response.HasError;
                vm.Error = response.Error;
                return View(vm);

            }

        }
    }
}
