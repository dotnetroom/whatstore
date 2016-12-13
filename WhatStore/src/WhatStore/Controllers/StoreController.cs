using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WhatStore.Infrastructure.ViewModels.Admin;
using WhatStore.Domain.Infrastructure.Repository.Interfaces;
using Microsoft.AspNetCore.Identity;
using WhatStore.Domain.Infrastructure.Models.Identity;
using WhatStore.Infrastructure.ViewModels.Store;


// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace WhatStore.Controllers
{
    [Route("store")]
    public class StoreController : Controller
    {
        private IStoreRepository _storeRepository;
        private ILocalizationRepository _localizationRepository;
        private UserManager<ApplicationUser> _userManager;
        public StoreController(IStoreRepository storeRepository, ILocalizationRepository localizationRepository, UserManager<ApplicationUser> userManager)
        {
            _storeRepository = storeRepository;
            _localizationRepository = localizationRepository;
            _userManager = userManager;
        }

        [Route("information")]
        public async Task<IActionResult> Information()
        {
            try
            {

                var states = await _localizationRepository.GetStates();

                var viewModel = new RegisterStoreDataViewModel()
                {
                    States = states
                };

                return View(viewModel);
            }

            catch (Exception ex)
            {
                return BadRequest();
            }
        }

        [HttpPost("register/information")]
        public async Task<IActionResult> RegisterInformation (RegisterStoreDataViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            var phone = model.PhoneDDD + model.PhoneNumber;
            if(await _storeRepository.UpdateStoreInformation(user.Id, model.StoreName, model.StoreDescription, phone,
                                                    model.Email, model.URL, model.Terms, model.HasAdress, model.Address,
                                                    model.Number, model.CEP, model.Complemento, model.City))
            {
                model.ReturnMessage = "Alterações salvas com sucesso";
            }
            else
            {
                model.ReturnMessage = "Erro ao salvar alterações";
            }

            return View("Information",model);
        }


        [HttpGet("type")]
        public async Task<IActionResult> Type()
        {
            try

            {
                var viewModel = new RegisterStoreTypeViewModel();
                return View(viewModel);
            }

            catch (Exception ex)
            {
                return BadRequest();
            }
        }

        [HttpPost("register/type")]
         public async Task<IActionResult> RegisterType (RegisterStoreTypeViewModel model)
         {
             if (!ModelState.IsValid)
             {
                 return BadRequest();
             }

             if (await _storeRepository.RegisterStoreType(model.StoreType))
             {
                 model.ReturnMessage = "Alterações salvas com sucesso";
             }
             else
             {
                 model.ReturnMessage = "Erro ao salvar alterações";
             }

             return View("Type", model);
         }


    }
}
