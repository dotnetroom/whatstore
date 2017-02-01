using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using WhatStore.Domain.Infrastructure.ViewModels.Admin;
using WhatStore.Domain.Infrastructure.Repository.Interfaces;
using Microsoft.AspNetCore.Identity;
using WhatStore.Domain.Infrastructure.Models.Identity;
using System.Globalization;

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

            if (await _productRepository.InsertProduct(user.StoreId, model.ProductName, model.Description, model.Price, model.Picture,
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

        [HttpGet("edit/{id}")]
        public async Task<IActionResult> EditProduct(string id)
        {
            try
            {
                var dataProduct = await _productRepository.GetProduct(id);

                var dataTag = await _productRepository.GetTag(dataProduct.Id);

                string resultDataTag = string.Join(",", dataTag);

                var viewModel = new RegisterProductViewModel()
                {
                    Id = dataProduct.Id,
                    Description = dataProduct.Description,
                    IsFreeShip = dataProduct.IsFreeShipping,                    
                    Length = (dataProduct.IsFreeShipping != false) ? double.Parse(dataProduct.Length) : 0,
                    ProductName = dataProduct.Name,
                    Price = dataProduct.Price,
                    Weigth = (dataProduct.IsFreeShipping != false) ? double.Parse(dataProduct.Weigth) : 0,
                    Widith = (dataProduct.IsFreeShipping != false) ? double.Parse(dataProduct.Widith) : 0,
                    Tags = resultDataTag
                };
                return View(viewModel);
            }

            catch(Exception ex)
            {
                return BadRequest();
            }
        }

        [HttpPost("edit")]
        public async Task<IActionResult>EditProductSave(RegisterProductViewModel model)
        {
            if (await _productRepository.UpdateProduct(model.ProductName, model.Description,
                model.Price, model.HasVariety, model.Colors, model.Sizes, model.IsFreeShip, model.Length,
                model.Weigth, model.Widith, model.Tags, model.Id))
            {
                model.ReturnMessage = "Alterações salvas com sucesso";
            }
            else
            {
                model.ReturnMessage = "Erro ao salvar alterações";
            }

            return RedirectToAction("Product",model);
        }

        [HttpPost("delete")]
        public async Task<IActionResult>DeleteProduct(string id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            if(await _productRepository.DeleteProduct(id))
            {

            }

            return RedirectToAction("Product");
        }


    }
}
