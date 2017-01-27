using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WhatStore.Domain.Infrastructure.ViewModels.Admin;
using WhatStore.Domain.Infrastructure.Repository.Interfaces;
using Microsoft.AspNetCore.Identity;
using WhatStore.Domain.Infrastructure.Models.Identity;
using WhatStore.Domain.Infrastructure.ViewModels.Store;
using WhatStore.Domain.Infrastructure.Models.Store;



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

                var states = await _localizationRepository.GetStates();

                var dataStore = await _storeRepository.GetStore(user.StoreId);

                var viewModel = new RegisterStoreDataViewModel()
                {
                    StoreName = dataStore.StoreName,
                    StoreDescription = dataStore.StoreDescription,
                    PhoneDDD = dataStore.PhoneDDD,
                    PhoneNumber = dataStore.PhoneNumber,
                    Email = dataStore.Email,
                    URL = dataStore.URL,
                    Terms = dataStore.Terms,
                    Address = dataStore.Address,
                    Number = dataStore.Number,
                    CEP = dataStore.CEP,
                    Complemento = dataStore.Complemento,
                    States = states,
                    State = dataStore.State,
                    City = dataStore.City,
                    CityName = dataStore.CityName,
                    HasAdress = (dataStore.State > 0) ? true : false  
                    
                };

                return View(viewModel);
            }

            catch (Exception ex)
            {
                return BadRequest();
            }
        }

        [HttpPost("register/information")]
        public async Task<IActionResult> RegisterInformation(RegisterStoreDataViewModel model)
        {


            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            var states = await _localizationRepository.GetStates();
            var user = await _userManager.FindByNameAsync(User.Identity.Name);

            var phone = model.PhoneDDD + model.PhoneNumber;

            if (await _storeRepository.UpdateStoreInformation(user.StoreId, model.StoreName, model.StoreDescription, phone,
                                                    model.Email, model.URL, model.Terms, model.HasAdress, model.Address,
                                                    model.Number, model.CEP, model.Complemento, model.City))
            {
                    model.ReturnMessage = "Alterações salvas com sucesso";
            }
            else
            {
                model.ReturnMessage = "Erro ao salvar alterações";
            }

            model.States = states;

            return View("Information", model);
        }


        [HttpGet("type")]
        public async Task<IActionResult> Type()

        {
            try

            {
                var storeType = await _storeRepository.GetStoreType();
                var viewModel = new RegisterStoreTypeViewModel();
                viewModel.StoreTypes = storeType;
                return View(viewModel);
            }

            catch (Exception ex)
            {
                return BadRequest();
            }
        }

        [HttpPost("register/type")]
        public async Task<IActionResult> RegisterType(RegisterStoreTypeViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var storeType = await _storeRepository.GetStoreType();

            if (await _storeRepository.RegisterStoreType(model.StoreType))
            {
                model.StoreTypes = storeType;
                model.ReturnMessage = "Alterações salvas com sucesso";
            }
            else
            {
                model.ReturnMessage = "Erro ao salvar alterações";
            }

            return RedirectToAction("Type");
        }

        [HttpPost("delete/type")]
        public async Task<IActionResult> DeleteType(int typeID)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            if(await _storeRepository.DeleteStoreType(typeID))
            {
                
            }
            else
            {
                
            }

            return RedirectToAction("Type");
        }

        [HttpGet("financial")]
        public async Task<IActionResult> Financial()
        {
            var states = await _localizationRepository.GetStates();

            var model = new RegisterFinancialViewModel()
            {
                States = states
            };

            return View(model);
        }

        [HttpPost("financial/register")]
        public async Task<IActionResult> RegisterFinancial(RegisterFinancialViewModel model)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var phone = model.DDD + model.Phone;

            var user = await _userManager.FindByNameAsync(User.Identity.Name);

            if (await _storeRepository.InsertFinancial(model.CEP, model.CityID, model.Complement, model.Number, model.Street, user.StoreId,
                                             model.About, model.BirthDay, model.CPF, model.FirstName, model.LastName, model.IsPessoaJuridica,
                                             phone, model.Rg, model.Gender, model.CNPJ, model.RazaoSocial, model.InscricaoEstadual, model.InscricaoMunicipal))
            {
                model.ReturnMessage = "Alterações salvas com sucesso";
            }
            else
            {
                model.ReturnMessage = "Erro ao salvar alterações";
            }

            return RedirectToAction("Information");
        }      
    }
}
