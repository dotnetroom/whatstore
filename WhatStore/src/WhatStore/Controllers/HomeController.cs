using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WhatStore.Domain.Infrastructure.Repository.Interfaces;
using WhatStore.Domain.Infrastructure.Repository;
using WhatStore.Domain.Infrastructure.Models.Product;
using WhatStore.Domain.Infrastructure.ViewModels.Store;

namespace WhatStore.Controllers
{
    public class HomeController : Controller
    {
        private IStoreRepository _storeRepository;
        private IProductRepository _productRepository;

        public HomeController(IStoreRepository storeRepository, IProductRepository productRepository)
        {
            _storeRepository = storeRepository;
            _productRepository = productRepository;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet("~/{store}")]
        public async Task<IActionResult> StoreIndex(string store)
        {
            var storeId = await _storeRepository.GetStoreId(store);
            var logo = await _storeRepository.GetLogo(storeId);
            List<CategoryViewModel> subCategoriesList = new List<CategoryViewModel> { };
            if (logo != null)
            {
                logo = Url.Action(logo, "image");
            }
            
            var categories = await _productRepository.GetCategories(storeId);
            foreach (var item in categories)
            {
                var subCategories = await _productRepository.GetSubCategories(item.Id);

                var categoryObject = new CategoryViewModel
                {
                    category = item,
                    subcategories = subCategories
                };
                subCategoriesList.Add(categoryObject);
            }
            ViewBag.Categories = subCategoriesList;
            ViewBag.Logo = logo;
            ViewBag.StoreName = store;
            ViewBag.StoreId = storeId;
            
            return View();
        }


    }
}
