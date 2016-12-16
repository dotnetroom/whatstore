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
                var user = await _userManager.FindByNameAsync(User.Identity.Name);

                string subDDD = string.Empty;
                string subPhoneNumber = string.Empty;
                var states = await _localizationRepository.GetStates();
                var dataStore = await _storeRepository.GetStore(user.StoreId);
                if (dataStore.Phone.Length > 0)
                {
                    subDDD = dataStore.Phone.Substring(0, 2);
                    subPhoneNumber = dataStore.Phone.Substring(2);
                }
                var viewModel = new RegisterStoreDataViewModel()
                {
                    StoreName = dataStore.Name,    
                    StoreDescription = dataStore.Description,
                    PhoneDDD = subDDD,
                    PhoneNumber = subPhoneNumber,
                    Email = dataStore.Email,                             
                    URL = dataStore.URL,
                    Terms = dataStore.Term,
                    //Address = dataStore.Adress.Street,
                    //Number = dataStore.Adress.Number,
                    //CEP = dataStore.Adress.CEP,
                    //Complemento = dataStore.Adress.Complement,
                    
                    States = states,
                    
                                
                    
                    
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
            var states = await _localizationRepository.GetStates();
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
         
            var phone = model.PhoneDDD + model.PhoneNumber;
            
            if(await _storeRepository.UpdateStoreInformation(user.StoreId, model.StoreName, model.StoreDescription, phone,
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
