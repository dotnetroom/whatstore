using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WhatStore.Infrastructure.ViewModels.Admin;
using WhatStore.Domain.Infrastructure.Repository.Interfaces;
using Microsoft.AspNetCore.Identity;
using WhatStore.Domain.Infrastructure.Models.Identity;

namespace WhatStore.Controllers
{
    [Route("product")]
    public class ProductController : Controller
    {
        private IProductRepository _productRepository;
        private UserManager<ApplicationUser> _userManager;
        public ProductController(IProductRepository productRepository,  UserManager<ApplicationUser> userManager)
        {
            _productRepository = productRepository;
            _userManager = userManager;
        }

        [Route("register")]
        public async Task<IActionResult> Register()
        {
            try
            {

                var viewModel = new RegisterProductViewModel()
                {

                };

                return View(viewModel);
            }

            catch (Exception ex)
            {
                return BadRequest();
            }
        }

        [HttpPost("register/information")]
        public async Task<IActionResult> RegisterProduct(RegisterProductViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            if (await _productRepository.UpdateProduct(user.Id, model.ProductName, model.Description, model.Price, model.Picture,
                                                       model.HasVariety, model.Colors, model.Sizes, model.IsFreeShip, model.Length,
                                                       model.Weight, model.Height, model.Tags))
            {
                model.ReturnMessage = "Alterações salvas com sucesso";
            }
            else
            {
                model.ReturnMessage = "Erro ao salvar alterações";
            }

            return View("Information", model);
        }
    }
}
