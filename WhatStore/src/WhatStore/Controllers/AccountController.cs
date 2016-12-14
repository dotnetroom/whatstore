using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WhatStore.Infrastructure.ViewModels.Admin;
using Microsoft.AspNetCore.Identity;
<<<<<<< HEAD
using WhatStore.Domain.Infrastructure.Repository.Interfaces;
using WhatStore.Domain.Infrastructure.Models.Store;
=======
using WhatStore.Domain.Infrastructure.Models.Identity;
>>>>>>> origin/1.0

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace WhatStore.Controllers
{
    [Route("account")]
    public class AccountController : Controller
    {
<<<<<<< HEAD

        private IStoreRepository _storeRepository;
        public AccountController(IStoreRepository storeRepository)
        {
            _storeRepository = storeRepository;
=======
        private UserManager<ApplicationUser> _userManager;
        public AccountController(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
>>>>>>> origin/1.0
        }

        // GET: /<controller>/

        [Route("login")]
        public IActionResult Login()
        {
            return View();
        }

        [Route("register")]
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
        public async Task<IActionResult> Login (LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            return Ok();
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterUserViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

<<<<<<< HEAD
            var storeID = await _storeRepository.RegisterStore(new Store() {
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

            return View("Login", model);
=======
            var storeID = _store.RegisterStore();

            var newUser = new ApplicationUser()
            {
                StoreId = storeID
            };
            _userManager.CreateAsync(_userManager);

            return Ok();
>>>>>>> origin/1.0
        }
    }
}
