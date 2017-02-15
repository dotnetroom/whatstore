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
        private IProductRepository _productRepository;
        private UserManager<ApplicationUser> _userManager;
        public AdminController(IProductRepository productRepository, UserManager<ApplicationUser> userManager)
        {
            _productRepository = productRepository;
            _userManager = userManager;
        }

        [HttpGet("open")]
        public IActionResult Open()

        {
            return View();
        }

        [HttpGet("category")]
        public async Task<IActionResult> Category()
        {
            try

            {
                var category = await _productRepository.GetCategory();
                var viewModel = new RegisterProductCategoryViewModel();
                viewModel.Categories = category;
                return View(viewModel);
            }

            catch (Exception ex)
            {
                return BadRequest();
            }
        }

        [HttpPost("register/category")]
        public async Task<IActionResult> RegisterCategoryProduct(RegisterProductCategoryViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            //var category = await _productRepository.GetCategory();

            if (await _productRepository.RegisterCategory(model.CategoryName))
            {
                model.ReturnMessage = "Alterações salvas com sucesso";
            }
            else
            {
                model.ReturnMessage = "Erro ao salvar alterações";
            }

            return RedirectToAction("category");
        }

        [HttpPost("update/category")]
        public async Task<IActionResult> UpdateCategoryProduct(RegisterProductCategoryViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            if (await _productRepository.UpdateCategory(model.CategoryId, model.CategoryName))
            {
                model.ReturnMessage = "Alterações salvas com sucesso";
            }
            else
            {
                model.ReturnMessage = "Erro ao salvar alterações";
            }

            return RedirectToAction("category");
        }

        [HttpGet("subcategory")]
        public async Task<IActionResult> Subcategory(RegisterProductCategoryViewModel model)
        {
            try

            {
                var category = await _productRepository.GetCategory();
                var subcategory = await _productRepository.GetSubCategory(model.CategoryId);                
                model.Subcategorys = subcategory;
                return View(model);
            }

            catch (Exception ex)
            {
                return BadRequest();
            }
        }

        [HttpPost("register/subcategory")]
        public async Task<IActionResult> RegisterSubCategoryProduct(RegisterProductCategoryViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            //var category = await _productRepository.GetCategory();

            if (await _productRepository.RegisterSubCategory(model.SubcategoryName, model.SubcategoryId))
            {
                model.ReturnMessage = "Alterações salvas com sucesso";
            }
            else
            {
                model.ReturnMessage = "Erro ao salvar alterações";
            }

            return RedirectToAction("subcategory");
        }
    }
}
