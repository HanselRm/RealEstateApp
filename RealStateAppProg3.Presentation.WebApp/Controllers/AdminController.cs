﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Org.BouncyCastle.Asn1.Mozilla;
using RealStateAppProg3.Core.Application.Interfaces.Service;
using RealStateAppProg3.Core.Application.Services;
using RealStateAppProg3.Core.Application.ViewModels.TypeProperty;
using RealStateAppProg3.Core.Application.ViewModels.TypeSale;
using RealStateAppProg3.Core.Application.ViewModels.Upgrades;
using RealStateAppProg3.Core.Application.ViewModels.Users;

namespace RealStateAppProg3.Presentation.WebApp.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly ITypePropertyService _typePropertyService;
        private readonly ITypeSaleService _typeSaleService;
        private readonly IUpgradeService _upgradeService;
        private readonly IPropertyService _propertyService;
        //importacion del servicio de usuario
        private readonly IUserService _userService;

        public AdminController(ITypePropertyService typePropertyService,
            ITypeSaleService typeSaleService, IUpgradeService upgradeService, IUserService userService, IPropertyService propertyService)
        {
            _typeSaleService = typeSaleService;
            _typePropertyService = typePropertyService;
            _upgradeService = upgradeService;
            _userService = userService;
            _propertyService = propertyService;
        }
        #region Home Admin
        public async Task<IActionResult> Dashboard()
        {
            //tomando las propiedades
            var properties = await _propertyService.GetAllAsync();
            ViewBag.propertiesActive = properties.Count();
            //agentes
            var agents = await _userService.GetUsersByRole("Agent");
            ViewBag.AgentsActive = agents.Where(d => d.IsActive).Count();
            ViewBag.AgentsDisabled = agents.Where(d => !d.IsActive).Count();
            //tomando los agentes
            var devs = await _userService.GetUsersByRole("Developer");
            ViewBag.devsActive = devs.Where(c => c.IsActive).Count();
            ViewBag.devsDisabled = devs.Where(c => !c.IsActive).Count();
            //tomando los clientes 
            var clients= await _userService.GetUsersByRole("Client");
            ViewBag.clientsActive = clients.Where(c => c.IsActive).Count();
            ViewBag.clientsDisabled = clients.Where(c => !c.IsActive).Count();

            //clients.Where(c => c.IsActive).Count();
            return View("DashboardAdmin");
        }
        #endregion
        #region Mant. Admin
        //obtener todos los usuarios con nivel adminsitrador
        public async Task<IActionResult> Index()
        {
            var users = await _userService.GetUsersAdmin();
            ViewBag.IsFor = "admin";
            return View("IndexAdmin",users);
        }
        //guardar administradores
        public IActionResult SaveAdminUser()
        {
            var userVm = new SaveUserViewModel{ IsActive = true };
            ViewBag.IsFor = "admin";
            return View("RegisterAdmin", userVm);
        }
        [HttpPost]
        public async Task<IActionResult> SaveAdminUser(SaveUserViewModel vm)
        {
            if (ModelState["Name"].Errors.Any() || ModelState["LastName"].Errors.Any() || ModelState["Identification"].Errors.Any()
                || ModelState["PhoneNumber"].Errors.Any() || ModelState["PhoneNumber"].Errors.Any()
                || ModelState["Username"].Errors.Any() || ModelState["Email"].Errors.Any() || ModelState["Password"].Errors.Any()
                || ModelState["ConfirmPassword"].Errors.Any())
            {
                return View("RegisterAdmin", vm);
            }
            var origin = Request.Headers["origin"];
            var response = await _userService.RegisterAsync(vm, origin);
            //-------------------------
            if (response != null && response.HasError != true)
            {
                return RedirectToRoute(new { controller = "Admin", action = "Index" });
            }
            else
            {
                vm.HasError = response.HasError;
                vm.Error = response.Error;
                return View(vm);

            }
        }

        //actualizar administradores
        public async Task<IActionResult> UpdateAdminUser(string Id)
        {
            var user = await _userService.GetByIdWithoutRol(Id);
            ViewBag.IsFor = "admin";
            if(user != null)
            {
                return View("RegisterAdmin", user);
            }
            return RedirectToRoute(new { controller = "Admin", action = "Index" });
        }
        [HttpPost]
        public async Task<IActionResult> UpdateAdminUser(SaveUserViewModel vm)
        {
            var usr = await _userService.GetByIdWithoutRol(vm.Id);
            if (vm.Password == null)
            {
                vm.Password = usr.Password;
                vm.ConfirmPassword = usr.Password;
            }
            if (usr.PhotoProfileUrl == null)
            {
                vm.PhotoProfileUrl = "url";
            }
            else
            {
                vm.PhotoProfileUrl = usr.PhotoProfileUrl;
            }

            if (ModelState["Name"].Errors.Any() || ModelState["LastName"].Errors.Any() || ModelState["Identification"].Errors.Any()
                || ModelState["PhoneNumber"].Errors.Any() || ModelState["PhoneNumber"].Errors.Any()
                || ModelState["Username"].Errors.Any() || ModelState["Email"].Errors.Any()|| ModelState["TypeUser"].Errors.Any())
            {
                return View("RegisterAdmin", vm);
            }

            await _userService.UpdateAsync(vm);
            return RedirectToRoute(new { controller = "Admin", action = "Index" });
        }
        //actualizar el estado del usuario IsActive
        public async Task<IActionResult> UpdateStatusUser(string Id, string Message)
        {
            ViewBag.Id = Id;
            ViewBag.Message = Message;  
            return View("ConfirmStatus");
        }
        //confirmando que desea cambiar el estado 
        public async Task<IActionResult> ConfirmUpdateStatus(string Id)
        {
            var user = await _userService.GetByIdWithoutRol(Id);
            //invertimos su valor, en caso de que este activo lo pondra falso y viceversa
            user.IsActive = !user.IsActive;
            //actualizamos
            await _userService.UpdateAsync(user);
            return RedirectToRoute(new { controller = "Admin", action = "Index" });
        }
        #endregion
        #region Mant. de desarrolladores
        //obtener todos los usuarios con nivel adminsitrador
        public async Task<IActionResult> IndexDev()
        {
            var users = await _userService.GetUsersByRole("Developer");
            ViewBag.IsFor = "dev";
            return View("IndexAdmin", users);
        }
        //guardar administradores
        public IActionResult SaveDevUser()
        {
            var userVm = new SaveUserViewModel { IsActive = true };
            ViewBag.IsFor = "dev";
            return View("RegistrAdmin", userVm);
        }

        [HttpPost]
        public async Task<IActionResult> SaveDevUser(SaveUserViewModel vm)
        {
            var usr = await _userService.GetByIdWithoutRol(vm.Id);
            if (vm.Password == "")
            {
                vm.Password = usr.Password;
                vm.ConfirmPassword = usr.Password;
            }
            vm.PhotoProfileUrl = usr.PhotoProfileUrl;
            if (ModelState["Name"].Errors.Any() || ModelState["LastName"].Errors.Any() || ModelState["Identification"].Errors.Any()
                || ModelState["PhoneNumber"].Errors.Any() || ModelState["PhoneNumber"].Errors.Any()
                || ModelState["Username"].Errors.Any() || ModelState["Email"].Errors.Any() || ModelState["Password"].Errors.Any()
                || ModelState["ConfirmPassword"].Errors.Any())
            {
                return View("RegisterAdmin", vm);
            }
            var origin = Request.Headers["origin"];
            var response = await _userService.RegisterAsync(vm, origin);
            //-------------------------
            if (response != null && response.HasError != true)
            {
                return RedirectToRoute(new { controller = "Admin", action = "Index" });
            }
            else
            {
                vm.HasError = response.HasError;
                vm.Error = response.Error;
                return View(vm);

            }
        }

        //actualizar administradores
        public async Task<IActionResult> UpdateDevUser(string Id)
        {
            var user = await _userService.GetByIdWithoutRol(Id);
            if (user != null)
            {
                return View("RegisterAdmin", user);
            }
            return RedirectToRoute(new { controller = "Admin", action = "IndexDev" });
        }
        [HttpPost]
        public async Task<IActionResult> UpdateDevUser(SaveUserViewModel vm)
        {
            var usr = await _userService.GetByIdWithoutRol(vm.Id);
            if (vm.Password == null)
            {
                vm.Password = usr.Password;
                vm.ConfirmPassword = usr.Password;
            }
            if (usr.PhotoProfileUrl == null)
            {
                vm.PhotoProfileUrl = "url";
            }
            else
            {
                vm.PhotoProfileUrl = usr.PhotoProfileUrl;
            }

            if (ModelState["Name"].Errors.Any() || ModelState["LastName"].Errors.Any() || ModelState["Identification"].Errors.Any()
                || ModelState["PhoneNumber"].Errors.Any() || ModelState["PhoneNumber"].Errors.Any()
                || ModelState["Username"].Errors.Any() || ModelState["Email"].Errors.Any() || ModelState["TypeUser"].Errors.Any())
            {
                return View("UpdateAdminUser", vm);
            }

            await _userService.UpdateAsync(vm);
            return RedirectToRoute(new { controller = "Admin", action = "IndexDev" });
        }
     
        #endregion
        #region Mant. de propiedades
        //Mantenimiento de propiedades
        public async Task<IActionResult> MantTpro()
        {
            var vm = await _typePropertyService.GetAllAsync();
            ViewBag.Cantidad = await _propertyService.GetallWithInclude();
            return View(vm);
        }
        //guarda tipo de propiedad
        public IActionResult SaveTPro()
        {
            return View("SaveTPro", new SaveTypePropertyViewModel());
        }

        
        [HttpPost]
        public async Task<IActionResult> SaveTPro(SaveTypePropertyViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                return View("SaveTPro", vm);
            }
            await _typePropertyService.SaveAsync(vm);
            return RedirectToRoute(new { controller = "Admin", action = "MantTpro" });
        }
        //actualizar tipo de propiedades
        public async Task<IActionResult> UpdateTPro(int Id)
        {
            var typeProperty = await _typePropertyService.GetByIdAsync(Id);
            return View("SaveTPro", typeProperty);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateTPro(SaveTypePropertyViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                return View("SaveTPro", vm);
            }
            await _typePropertyService.UpdateAsync(vm, vm.Id);
            return RedirectToRoute(new { controller = "Admin", action = "MantTpro" });

        }

        public IActionResult RemoveTPro(int Id)
        {
            ViewBag.Id = Id;
            return View();
        }

        public async Task<IActionResult> ConfirmRemoveTPro(int id)
        {
            await _typePropertyService.RemoveAsync(id);
            return RedirectToRoute(new { controller = "Admin", action = "MantTpro" });
        }
        #endregion

        #region Mant. Tipo de venta
        public async Task<IActionResult> MantTSale()
        {
            var vm = await _typeSaleService.GetAllAsync();
            ViewBag.Cantidad = await _propertyService.GetallWithInclude();
            return View(vm);
        }

        public IActionResult SaveTSale()
        {
            return View("SaveTSale", new SaveTypeSaleViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> SaveTSale(SaveTypeSaleViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                return View("SaveTSale", vm);
            }
            await _typeSaleService.SaveAsync(vm);
            return RedirectToRoute(new { controller = "Admin", action = "MantTSale" });
        }

        public async Task<IActionResult> UpdateTSale(int Id)
        {
            var typeSale = await _typeSaleService.GetByIdAsync(Id);
            return View("SaveTSale", typeSale);

        }
        [HttpPost]
        public async Task<IActionResult> UpdateTSale(SaveTypeSaleViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                return View("SaveTSale", vm);
            }
            await _typeSaleService.UpdateAsync(vm, vm.Id);
            return RedirectToRoute(new { controller = "Admin", action = "MantTSale" });
        }

        public IActionResult RemoveTSale(int Id)
        {
            ViewBag.Id = Id;
            return View();
        }
        public async Task<IActionResult> ConfirmRemoveTSale(int id)
        {
            await _typeSaleService.RemoveAsync(id);
            return RedirectToRoute(new { controller = "Admin", action = "MantTSale" });
        }
        #endregion

        #region Mant. de Mejoras
        public async Task<IActionResult> MantUpgrade()
        {
            var vm = await _upgradeService.GetAllAsync();
            return View(vm);
        }

        public IActionResult SaveUpgrade()
        {
            return View("SaveUpgrade", new SaveUpgradesViewModel());
        }
        [HttpPost]
        public async Task<IActionResult> SaveUpgrade(SaveUpgradesViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                return View("SaveUpgrade", vm);
            }
            await _upgradeService.SaveAsync(vm);
            return RedirectToRoute(new { controller = "Admin", action = "MantUpgrade" });
        }
        public async Task<IActionResult> UpdateUpgrade(int Id)
        {
            
            var Upgrade = await _upgradeService.GetByIdAsync(Id);
            return View("SaveUpgrade", Upgrade);

        }

        [HttpPost]
        public async Task<IActionResult> UpdateUpgrade(SaveUpgradesViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                return View("SaveUpgrade", vm);
            }
            await _upgradeService.UpdateAsync(vm, vm.Id);
            return RedirectToRoute(new { controller = "Admin", action = "MantUpgrade" });
        }

        public IActionResult RemoveUpgrade(int Id)
        {
            ViewBag.Id = Id;
            return View();
        }
        public async Task<IActionResult> ConfirmRemoveUpgrade(int id)
        {
            await _upgradeService.RemoveAsync(id);
            return RedirectToRoute(new { controller = "Admin", action = "MantUpgrade" });
        }

        #endregion

        public async Task<IActionResult> AgentList()
        {
            var usersAgent = await _userService.GetUsersByRole("Agent");
            ViewBag.Cantidad = await _propertyService.GetallWithInclude();
            return View("AgentList", usersAgent);
        }
        public async Task<IActionResult> UpdateStatusUserAgent(string Id, string Message)
        {
            ViewBag.Id = Id;
            ViewBag.Message = Message;
            return View("ConfirmStatusAgent");
        }
        //confirmando que desea cambiar el estado 
        public async Task<IActionResult> ConfirmUpdateStatusAgent(string Id)
        {
            var user = await _userService.GetByIdWithoutRol(Id);
            //invertimos su valor, en caso de que este activo lo pondra falso y viceversa
            user.IsActive = !user.IsActive;
            //actualizamos
            await _userService.UpdateAsync(user);
            return RedirectToRoute(new { controller = "Admin", action = "AgentList" });
        }
        public IActionResult RemoveAgent(string Id)
        {
            ViewBag.Id = Id;
            return View("RemoveAgent");
        }
        public async Task<IActionResult> ConfirmRemoveAgent(string Id)
        {
            await _userService.DeleteByIdAsync(Id.ToString());
            return RedirectToRoute(new { controller = "Admin", action = "AgentList" });
        }
    }
}
