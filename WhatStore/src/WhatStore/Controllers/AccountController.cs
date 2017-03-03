using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WhatStore.Domain.Infrastructure.ViewModels.Admin;
using Microsoft.AspNetCore.Identity;
using WhatStore.Domain.Infrastructure.Repository.Interfaces;
using WhatStore.Domain.Infrastructure.Models.Store;
using Microsoft.AspNetCore.Authorization;
using WhatStore.Domain.Infrastructure.Models.Identity;
using Microsoft.Extensions.Logging;
using WhatStore.Domain.Infrastructure.ViewModels.Account;
using WhatStore.Domain.Infrastructure.Helpers;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace WhatStore.Controllers
{
    [Route("account")]
    public class AccountController : Controller
    {

        private IStoreRepository _storeRepository;
        private SignInManager<ApplicationUser> _signInManager;
        private ILogger _logger;
        private UserManager<ApplicationUser> _userManager;

        public AccountController(UserManager<ApplicationUser> userManager, IStoreRepository storeRepository, SignInManager<ApplicationUser> signInManager, ILoggerFactory loggerFactory)
        {
            _storeRepository = storeRepository;
            _signInManager = signInManager;
            _logger = loggerFactory.CreateLogger<AccountController>();
            _userManager = userManager;

        }

        [HttpGet("login")]
        [AllowAnonymous]
        public IActionResult Login(string returnURL = null)
        {
            ViewData["ReturnURL"] = returnURL;
            return View();
        }




        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, lockoutOnFailure: false);

            if (result.Succeeded)
            {
                _logger.LogInformation(1, "Usuario logado.");

            }

            else
            {
                ModelState.AddModelError("Email", "Usuário ou senha incorreta");
                return View(model);
            }

            var modelAdmin = new RegisterStoreDataViewModel();

            return RedirectToAction("Information", "Store", modelAdmin);
        }


        [HttpGet("register")]
        [AllowAnonymous]
        public async Task<IActionResult> Register()
        {
            RegisterUserViewModel model = new RegisterUserViewModel();

            var storeTypes = await _storeRepository.GetStoreType();


            model.StoreTypes = storeTypes;


            return View(model);
        }


        [HttpPost("register/user")]
        public async Task<IActionResult> RegisterUser(RegisterUserViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            long storeID = 0;
            var urlStore = model.StoreName.Trim().Replace(" ", "").ToLower();

            if (await _storeRepository.SelectStoreName(urlStore) == null)
            {
                storeID = await _storeRepository.RegisterStore(new Store()
                {
                    StoreTypeId = model.StoreTypeID,
                    Name = model.StoreName.Trim(),
                    URL = urlStore,
                });
            }
            else
            {
                var storeTypes = await _storeRepository.GetStoreType();
                model.StoreTypes = storeTypes;
                ModelState.AddModelError("StoreName", "Nome de loja já utilizada");
                model.ReturnMessage = "Erro ao salvar alterações";
                return View("Register", model);
            }

            if (storeID > 0)
            {
                var user = new ApplicationUser
                {
                    FirstName = model.Name,
                    LastName = model.LastName,
                    RG = model.RG,
                    CPF = model.CPF,
                    Birthday = model.Birthday,
                    Genero = model.Genero,
                    Email = model.Email,
                    NormalizedEmail = model.Email.ToUpper(),
                    NormalizedUserName = model.Email.ToUpper(),
                    UserName = model.Email,
                    StoreId = storeID
                };

                var result = await _userManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(user, "Store");
                    model.ReturnMessage = "Alterações salvas com sucesso";
                    return View("Login", new LoginViewModel());
                }

                else
                {
                    ViewBag.Errors = result.ConvertToHTML();
                    var storeTypes = await _storeRepository.GetStoreType();
                    model.StoreTypes = storeTypes;
                    await _storeRepository.DeleteStore(storeID);
                    return View("Register", model);
                }

            }

            return Ok();
        }

        [HttpPost("Logoff")]
        public async Task<IActionResult> Logoff()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Login", "Account");

        }
        #region helper
        private IActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
                return RedirectToAction(nameof(HomeController.Index), "Home");
            }
        }
        #endregion

        [HttpGet("~/{store}/register/user")]
        public IActionResult RegisterUserStore()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            return View();
        }

        [HttpGet("~/{store}/register/user/comp")]
        [AllowAnonymous]
        public async Task<IActionResult> RegisterUserStoreComp()
        {
            RegisterUserStoreCompViewModel model = new RegisterUserStoreCompViewModel();

            return View(model);
        }

        [HttpPost("~/{store}/register/user/complement")]
        public async Task<IActionResult> RegisterUserStoreComplement(RegisterUserStoreCompViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            var user = new ApplicationUser
            {
                FirstName = model.Name,
                LastName = model.LastName,
                RG = model.RG,
                CPF = model.CPF,
                Birthday = model.Birthday,
                Genero = model.Genero,
                Email = model.Email,
                NormalizedEmail = model.Email.ToUpper(),
                NormalizedUserName = model.Email.ToUpper(),
                UserName = model.Email
            };

            var result = await _userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
                return View("Login", new LoginViewModel());
            }
            else
            {
                return View("RegisterUserStoreComp", model);
            }


            return View();
        }

        [HttpGet("~/{store}/confirm/user")]
        public IActionResult ConfirmUserStore()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            return View();
        }
    }
}
