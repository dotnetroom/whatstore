using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WhatStore.Infrastructure.ViewModels.Admin;
using Microsoft.AspNetCore.Identity;
using WhatStore.Domain.Infrastructure.Repository.Interfaces;
using WhatStore.Domain.Infrastructure.Models.Store;
using Microsoft.AspNetCore.Authorization;
using WhatStore.Domain.Infrastructure.Models.Identity;
using Microsoft.Extensions.Logging;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace WhatStore.Controllers
{
    [Route("account")]
    public class AccountController : Controller
    {

        private IStoreRepository _storeRepository;
        private SignInManager<ApplicationUser> _signInManager;
        private ILogger _logger;

        public AccountController(IStoreRepository storeRepository, SignInManager<ApplicationUser> signInManager, ILogger logger)
        {
            _storeRepository = storeRepository;
            _signInManager = signInManager;
            _logger = logger;
        }

        // GET: /<controller>/

        [Route("login")]
        [AllowAnonymous]
        public IActionResult Login(string returnURL = null)
        {
            ViewData["ReturnURL"] = returnURL;
            return View();
        }

        [Route("register")]
        [AllowAnonymous]
        public async Task<IActionResult> Register()
        {
            var storeTypes = await _storeRepository.GetStoreType();

            var model = new RegisterUserViewModel()
            {
                StoreTypes = storeTypes
            };

            return View(model);
        }

        // POST: /<controller>/
        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginViewModel model, string returnURL = null)
        {
            ViewData["ReturnURL"] = returnURL;

            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe ,lockoutOnFailure: false);

            return Ok();
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterUserViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var storeID = await _storeRepository.RegisterStore(new Store()
            {
                StoreTypeId = model.StoreTypeID,
                Name = model.StoreName.Trim(),
                URL = model.StoreName.Trim().Replace(" ", "").ToLower()
            });

            if (storeID > 0)
            {
                model.ReturnMessage = "Alterações salvas com sucesso";
            }
            else
            {
                model.ReturnMessage = "Erro ao salvar alterações";
            }

            var modelLogin = new LoginViewModel();

            return View("Login", modelLogin);
        }
    }
}
