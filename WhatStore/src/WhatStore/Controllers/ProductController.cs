using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WhatStore.Domain.Infrastructure.ViewModels.Admin;
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
        private IStoreRepository _storeRepository;
        public ProductController(IProductRepository productRepository, UserManager<ApplicationUser> userManager, IStoreRepository storeRepository)
        {
            _productRepository = productRepository;
            _userManager = userManager;
            _storeRepository = storeRepository;
        }


        [HttpGet("register")]
        public async Task<IActionResult> RegisterProduct()
        {
            try
            {
                var viewModel = new RegisterProductViewModel();

                return View(viewModel);
            }

            catch (Exception ex)
            {
                return BadRequest();
            }
        }

        [HttpPost("register")]
        public async Task<IActionResult> RegisterProduct(RegisterProductViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var user = await _userManager.FindByNameAsync(User.Identity.Name);

            if (await _productRepository.UpdateProduct(user.StoreId, model.ProductName, model.Description, model.Price, model.Picture,
                                                       model.HasVariety, model.Colors, model.Sizes, model.IsFreeShip, model.Length,
                                                       model.Weigth, model.Widith, model.Tags, model.Id))
            {
                model.ReturnMessage = "Alterações salvas com sucesso";
            }
            else
            {
                ModelState.AddModelError("Id", "Código já existente");
                model.ReturnMessage = "Erro ao salvar alterações";
            }

            return RedirectToAction("RegisterProduct", model);
        }


        [HttpGet("information")]
        public async Task<IActionResult> Product(RegisterProductViewModel model)
        {
            try
            {
                var user = await _userManager.FindByNameAsync(User.Identity.Name);

                var products = _productRepository.GetProducts(user.StoreId);

                var viewModel = new ProductViewModel();
                viewModel.Products = await products;

                return View(viewModel);

            }
            catch (Exception ex)
            {
                return BadRequest();

            }
        }

        [Route("Edit")]
        public async Task<IActionResult> EditProduct(RegisterProductViewModel model)
        {
            try
            {
                var dataProduct = await _productRepository.GetProduct(model.Id);

                var viewModel = new RegisterProductViewModel()
                {
                    Description = dataProduct.Description,
                    IsFreeShip = dataProduct.IsFreeShipping,
                    Length = Double.Parse(dataProduct.Length),
                    ProductName = dataProduct.Name,
                    Price = dataProduct.Price,
                    Weigth = Double.Parse(dataProduct.Weigth),
                    Widith = Double.Parse(dataProduct.Widith)                  
                }
                return view(viewModel);
            }
        }

    }
}
