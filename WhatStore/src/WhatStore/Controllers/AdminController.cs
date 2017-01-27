using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WhatStore.Domain.Infrastructure.ViewModels.Admin;
using WhatStore.Domain.Infrastructure.Repository.Interfaces;
using WhatStore.Domain.Infrastructure.Models.Identity;
using Microsoft.AspNetCore.Identity;
using WhatStore.Domain.Infrastructure;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace WhatStore.Controllers
{
    [Route("admin")]
    public class AdminController : Controller
    {
        #region Constructors and Injections

        private ILocalizationRepository _localization;
        private IStoreRepository _store;
        private UserManager<ApplicationUser> _userManager;

        public AdminController(UserManager<ApplicationUser> userManager, ILocalizationRepository localization, IStoreRepository store)
        {
            _localization = localization;
            _store = store;
            _userManager = userManager;
        }

        #endregion 

        [Route("financial")]
        // GET: /<controller>/
        public async Task<IActionResult> Financial()
        {
            var states = await _localization.GetStates();

            var viewModel = new RegisterFinancialViewModel()
            {
                States = states
            };

            return View(viewModel);
        }

        [HttpPost("financial")]
        public  async Task<IActionResult> Financial(RegisterFinancialViewModel model)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var phone = model.DDD + model.Phone;

            var user = await _userManager.FindByNameAsync(User.Identity.Name);

            if (await _store.InsertFinancial(model.CEP, model.CityID, model.Complement, model.Number, model.Street, user.StoreId,
                                             model.About, model.BirthDay, model.CPF, model.FirstName, model.LastName, model.IsPessoaJuridica,
                                             phone, model.Rg, model.Gender, model.CNPJ, model.RazaoSocial, model.InscricaoEstadual, model.InscricaoMunicipal))
            {
                model.ReturnMessage = "Alterações salvas com sucesso";
            } 
            else
            {  
                model.ReturnMessage = "Erro ao salvar alterações";
            }

            return RedirectToAction("FinancialViewModel", model);
        }
        
            
        

        [Route("open")]
        public IActionResult Open()

        {
            return View();
        }

        [Route("StoreType")]
         public IActionResult StoreType()
        {
            return View();

        }

        
    }
}
