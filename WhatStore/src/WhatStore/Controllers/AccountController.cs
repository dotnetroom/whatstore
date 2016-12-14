using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WhatStore.Infrastructure.ViewModels.Admin;
using Microsoft.AspNetCore.Identity;
using WhatStore.Domain.Infrastructure.Repository.Interfaces;
using WhatStore.Domain.Infrastructure.Models.Store;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace WhatStore.Controllers
{
    [Route("account")]
    public class AccountController : Controller
    {

        private IStoreRepository _storeRepository;
        public AccountController(IStoreRepository storeRepository)
        {
            _storeRepository = storeRepository;
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
        }
    }
}
